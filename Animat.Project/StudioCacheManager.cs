// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/StudioCacheManager.cs
// --------------------------------------------------------------------------------
// Copyright (c) 2014, Jieni Luchijinzhou a.k.a Aragorn Wyvernzora
// 
// This file is a part of Animat-Studio.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
// of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Animat.UI;
using libWyvernzora.Core;
using libWyvernzora.IO;
using NLog;

namespace Animat.Project
{
    /// <summary>
    ///     Animat Studio Cache Manager
    /// </summary>
    public class StudioCacheManager : IDisposable
    {
        // NLog Logger
        // ReSharper disable once InconsistentNaming
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        // Constants
        protected const String MagicNumber = "ANIMAT.UI.CACHE.";
        protected const String IndexFile = "cache-index.bin";
        protected const String DataFile = "cache-data.bin";
        protected const Int32 DataAlignment = 0x800;

        #region Nested Types

        /// <summary>
        ///     Single cache entry
        /// </summary>
        protected class CacheEntry : IComparable<CacheEntry>
        {
            /// <summary>
            ///     Address of the entry in the index file.
            /// </summary>
            public Int64 MetadataAddress { get; set; }

            /// <summary>
            ///     ID of the entry.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public Int64 ID { get; set; }

            /// <summary>
            ///     Address of the entry in the data file.
            /// </summary>
            public Int64 Address { get; set; }

            /// <summary>
            ///     Length of the entry data.
            /// </summary>
            public Int64 Length { get; set; }

            /// <summary>
            ///     Gets the unaligned address of the end of the entry.
            /// </summary>
            public Int64 EndAddress
            {
                get { return Address + Length; }
            }

            #region IComparable Members

            public int CompareTo(CacheEntry other)
            {
                if (Address > other.Address) return 1;
                if (Address < other.Address) return -1;
                if (Length > other.Length) return 1;
                if (Length < other.Length) return -1;
                return 0;
            }

            #endregion
        }

        #endregion

        // Data Fields
        protected readonly SortedDictionary<Int64, CacheEntry> entries; // Map of filenames to entries
        protected readonly SortedList<Int64, Int64> holes; // Sorted list of holes (unused data sections)
        protected readonly Queue<Int64> emptyIndex; // Addresses of empty slots in the index file
        protected StreamEx dataStream; // Data file stream
        protected StreamEx indexStream; // Index file stream

        #region Constructor

        /// <summary>
        ///     Constructor.
        ///     Manually assign indexStream and dataStream.
        /// </summary>
        protected StudioCacheManager()
        {
            logger.Trace("Initializing StudioCacheManager data fields.");

            entries = new SortedDictionary<Int64, CacheEntry>();
            holes = new SortedList<Int64, Int64>();
            emptyIndex = new Queue<Int64>();
        }

        /// <summary>
        ///     Constructor.
        ///     Creates a cache manager for the specified StudioProject.
        /// </summary>
        /// <param name="project"></param>
        public StudioCacheManager(StudioProject project) : this()
        {
            logger.Info("Creating new StudioCacheManager instance...");

            Project = project;

            // Construct paths
            string indexPath = Path.Combine(project.ProjectDirectory, IndexFile);
            string dataPath = Path.Combine(project.ProjectDirectory, DataFile);
            
            Initialize(indexPath, dataPath);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="indexFile"></param>
        /// <param name="dataFile"></param>
        protected void Initialize(String indexFile, String dataFile)
        {
            logger.Trace("Cache index file: {0}", indexFile);
            logger.Trace("Cache data file: {0}", dataFile);

            // Open cache files if BOTH index and data files exist
            if (File.Exists(indexFile) && File.Exists(dataFile))
            {
                try
                {
                    logger.Info("Cache files found, attempting to load.");

                    // Open cache files
                    indexStream = new StreamEx(indexFile);
                    dataStream = new StreamEx(dataFile);

                    // Load index file
                    string magicNumber = new String(Encoding.ASCII.GetChars(indexStream.ReadBytes(0x10)));
                    if (magicNumber.Equals(MagicNumber))
                    {
                        // Calculate entry count
                        long count = (indexStream.Length - indexStream.Position) / 0x10;

                        // Load Data
                        for (int i = 0; i < count; i++)
                        {
                            // Data fields
                            long slotPosition = indexStream.Position;
                            long entryId = indexStream.ReadInt64();
                            int entryAddress = indexStream.ReadInt32();
                            int entryLength = indexStream.ReadInt32();

                            // If entry is invalid, mark it as empty;
                            // otherwise, add it to the index.
                            if (entryId == -1)
                                emptyIndex.Enqueue(slotPosition);
                            else
                            {
                                CacheEntry entry = new CacheEntry
                                {
                                    ID = entryId,
                                    Length = entryLength,
                                    Address = entryAddress,
                                    MetadataAddress = slotPosition
                                };
                                entries.Add(entry.ID, entry);
                            }
                        }

                        // Scan for holes
                        //var sortedEntries = entries.Values.OrderBy(t => t.Address).ToArray();
                        CacheEntry[] sortedEntries = entries.Values.ToArray(); // <-- this should be sorted correctly
                        for (int i = 0; i < entries.Count - 1; i++)
                        {
                            long lastEntryEnd = Align(sortedEntries[i].EndAddress, DataAlignment);
                            long nextEntryStart = sortedEntries[i + 1].Address;
                            long holeSize = nextEntryStart - lastEntryEnd;

                            // Positive hole size means a data hole, negative means corrupted data
                            if (holeSize > 0)
                                holes.Add(lastEntryEnd, holeSize);
                            else if (holeSize < 0)
                                throw new InvalidDataException("Negative hole (overlapping data) detected.");
                        }
                    }
                    else
                        throw new InvalidDataException("Invalid magic number!");
                }
                catch (Exception x)
                {
                    logger.Warn("Cache file corrupted: [{0}] {1}", x.GetType().FullName, x.Message);
                    indexStream.Dispose();
                    dataStream.Dispose();
                }
            }
            else
                logger.Info("Cache index or data file not found, proceeding to creating empty cache.");

            // Overwrite BOTH index and data files if one of them is missing
            // This is also the fallback route if cache files are corrupted
            indexStream = new StreamEx(indexFile, FileMode.Create);
            indexStream.WriteBytes(Encoding.ASCII.GetBytes(MagicNumber));
            dataStream = new StreamEx(dataFile, FileMode.Create);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds data into cache.
        ///     In case of a duplicate entry, the old entry will be overwritten.
        /// </summary>
        /// <param name="id">ID of the entry.</param>
        /// <param name="data">Stream containing data to be cached.</param>
        public void AddEntry(Int64 id, StreamEx data)
        {
            logger.Debug("Adding cache entry: ID = 0x{0}; Length = {1}", id.ToHexString(16), data.Length);

            // If the entry already exists, delete it
            if (entries.ContainsKey(id))
            {
                RemoveEntry(id);
                logger.Trace("Attempting to add a duplicate entry, removing old data.");
            }

            // Find a hole to write the data
            Int64 address;
            KeyValuePair<long, long> hole = holes.FirstOrDefault(t => t.Value >= data.Length);
            if (!hole.Equals(default(KeyValuePair<Int64, Int64>)))
            {
                logger.Trace("Hole found: Address = 0x{0}; Length = 0x{1}", hole.Key.ToHexString(8),
                    hole.Value.ToHexString(8));

                // Appropriate hole found, write data first
                address = hole.Key;
                // Use partial stream to avoid overflows
                PartialStreamEx partialStream = new PartialStreamEx(dataStream, address, hole.Value);
                data.CopyTo(partialStream);

                // Reduce hole size
                holes.Remove(hole.Key);
                long newHoleAddress = Align(hole.Key + data.Length, DataAlignment);
                long newHoleLength = hole.Value - data.Length;

                Debug.Assert(newHoleLength > 0, "Hole size should be greater or equal than zero after resize.");

                // Add it back if there is still hole after the write
                if (newHoleLength > DataAlignment)
                {
                    logger.Trace("Hole reduced to: Address = 0x{0}; Length = 0x{1}", newHoleAddress.ToHexString(8),
                        newHoleLength.ToHexString(8));
                    holes.Add(newHoleAddress, newHoleLength);
                }
            }
            else
            {
                logger.Trace("No holes large enough, writing to the end of data file.");
                address = dataStream.Seek(0, SeekOrigin.End);

                // Write Data
                data.CopyTo(dataStream);

                // Pad for alignment
                long fill = Align(dataStream.Position, DataAlignment) - dataStream.Position;
                dataStream.WriteBytes(new byte[fill]);

                Debug.Assert(dataStream.Position % DataAlignment == 0,
                    "Data stream position should be aligned after write.");
            }

            // Find an empty slot in the index file
            long indexAddress = indexStream.Seek(0, SeekOrigin.End); // Set it to end of file first
            if (emptyIndex.Count > 0)
            {
                indexAddress = emptyIndex.Dequeue(); // If there are empty index slots, use those instead
                logger.Trace("Found an empty index slot at 0x{0}", indexAddress.ToHexString(8));
            }

            Debug.Assert(indexAddress % 0x10 == 0, "Index address should be aligned to 0x10.");

            // Write the index
            indexStream.Position = indexAddress;
            indexStream.WriteInt64(id);
            indexStream.WriteInt32((Int32) address);
            indexStream.WriteInt32((Int32) data.Length);

            // Save the index
            entries.Add(id, new CacheEntry
            {
                ID = id,
                Address = address,
                Length = data.Length,
                MetadataAddress = indexAddress
            });
        }

        /// <summary>
        ///     Removes the entry from cache.
        ///     Please note that this method
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEntry(Int64 id)
        {
            logger.Debug("Removing an entry: ID = 0x{0}", id.ToHexString(16));

            // Check if there is an entry to remove
            if (!entries.ContainsKey(id))
            {
                logger.Trace("No such entry to remove: ID = 0x{0}", id.ToHexString(16));
                return;
            }

            // There is such entry, overwrite its ID in index
            CacheEntry entry = entries[id];
            indexStream.Position = entry.MetadataAddress;
            indexStream.WriteInt64(-1);

            // Remove the entry from index
            entries.Remove(id);

            // Create a hole
            long holeAddress = entry.Address;
            long holeSize = Align(entry.Length, DataAlignment);

            // Try to merge other holes into this one if possible
            KeyValuePair<long, long> holeBefore = holes.FirstOrDefault(t => (t.Key + t.Value) == holeAddress);
            if (!holeBefore.Equals(default(KeyValuePair<Int64, Int64>)))
            {
                // There is a hole before this one, remove and merge it
                holes.Remove(holeBefore.Key);
                holeAddress = holeBefore.Key;
                holeSize += holeBefore.Value;
            }

            if (holes.ContainsKey(holeAddress + holeSize))
            {
                // There is a hole after this one, remove and merge it
                holes.Remove(holeAddress + holeSize);
                holeSize += holes[holeAddress + holeSize];
            }

            // Add the hole
            holes.Add(holeAddress, holeSize);
        }

        /// <summary>
        ///     Returns a value indicating whether the cache contains the specified entry.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean ContainsEntry(Int64 id)
        {
            return entries.ContainsKey(id);
        }

        /// <summary>
        ///     Gets the stream of a cache entry.
        ///     DO NOT CLOSE THE STREAM RETURNED BY THIS METHOD.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StreamEx GetEntry(Int64 id)
        {
            logger.Debug("Getting cache entry: ID = 0x{0}", id.ToHexString(16));

            // Check if there is such an entry to get
            if (!entries.ContainsKey(id))
            {
                logger.Error("Attempting to get an entry that does not exist: ID = 0x{0}", id.ToHexString(16));
                throw new KeyNotFoundException();
            }

            // Get the entry
            CacheEntry metadata = entries[id];
            PartialStreamEx stream = new PartialStreamEx(dataStream, metadata.Address, metadata.Length);

            return stream;
        }

        /// <summary>
        ///     Aligns data address.
        ///     Please note that there is no magic number in data file.
        /// </summary>
        /// <param name="length">Address/Length to align.</param>
        /// <param name="alignment">Alignment value.</param>
        /// <returns></returns>
        protected static Int64 Align(Int64 length, Int32 alignment)
        {
            if (alignment < 1) return length;
            return ((length + alignment - 1) / alignment) * alignment;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the project this cache manager is associated with.
        /// </summary>
        public StudioProject Project
        { get; private set; }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // Dispose of data streams
            if (indexStream != null)
                indexStream.Dispose();
            if (dataStream != null)
                dataStream.Dispose();

            // Dispose of index data
            entries.Clear();
            holes.Clear();
            emptyIndex.Clear();
        }

        #endregion
    }
}