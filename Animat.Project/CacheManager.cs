// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/CacheManager.cs
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
using libWyvernzora.Core;
using libWyvernzora.IO;
using NLog;

namespace Animat.Project
{
    /// <summary>
    ///     Animat Studio Cache Manager
    /// </summary>
    public class CacheManager : IDisposable
    {
        // NLog Logger
        // ReSharper disable once InconsistentNaming
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        // Constants
        protected const String MagicNumber = "ANIMAT.UI.CACHE.";
        protected const String IndexFile = "cache-index.bin";
        protected const String DataFile = "cache-data.bin";
        protected const Int32 DataAlignment = 0x800;
        protected const Int32 IndexAlignment = 0x100;

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
            public String Name { get; set; }

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

        /// <summary>
        ///     A collection of data holes.
        /// </summary>
        public class HoleCollection : SortedList<Int64, Int64>
        {
            /// <summary>
            ///     Adds a new hole to the collection without merging.
            /// </summary>
            /// <param name="address">Address of the new hole.</param>
            /// <param name="length">Length of the now hole.</param>
            public new void Add(Int64 address, Int64 length)
            {
                // Add the hole first, so that we can search the list much faster
                base.Add(address, length);

                // Get holes before and after this one
                int index = IndexOfKey(address); // Index of the hole we are adding
                long previous = index > 0 ? Keys[index - 1] : -1;
                long next = index < Count - 1 ? Keys[index + 1] : -1;

                // Remove the new hole
                Remove(address);

                // See if the hole before this can be merged
                if (previous >= 0)
                {
                    long previousLength = this[previous];
                    long previousEnds = previous + previousLength; // end address of the previous hole
                    if (previousEnds == address)
                    {
                        address -= previousLength; // move start of current hole to include the previous one
                        length += previousLength; // adjust the length to include the previous hole
                        Remove(previous); // remove the previous hole from list
                    }
                }

                // See if the next hole can be merged
                if (next >= 0)
                {
                    long currentEnds = address + length;
                    if (currentEnds == next)
                    {
                        length += this[next]; // adjust length to include the next hole
                        Remove(next); // remove the next hole from list
                    }
                }

                // Add the new hole to the list
                // Recursion ? No need actually.
                base.Add(address, length);
            }
        }

        #endregion

        // Data Fields
        protected readonly Dictionary<String, Int64> entriesMap;    // Map of filenames to entry addresses 
        protected readonly SortedDictionary<Int64, CacheEntry> entries; // Map of entry addresses to entries
        protected readonly HoleCollection dataHoles; // Unused data sections in the data file
        protected readonly Queue<Int64> indexHoles; // Unused data sections in the index file
        protected StreamEx dataStream; // Data file stream
        protected StreamEx indexStream; // Index file stream

        #region Constructor

        /// <summary>
        ///     Constructor.
        ///     Manually assign indexStream and dataStream.
        /// </summary>
        protected CacheManager()
        {
            logger.Trace("Initializing StudioCacheManager data fields.");

            entriesMap = new Dictionary<string, long>();
            entries = new SortedDictionary<Int64, CacheEntry>();
            indexHoles = new Queue<Int64>();
            dataHoles = new HoleCollection();
        }

        /// <summary>
        ///     Constructor.
        ///     Creates a cache manager for the specified StudioProject.
        /// </summary>
        /// <param name="project"></param>
        public CacheManager(StudioProject project) : this()
        {
            logger.Info("Creating new StudioCacheManager instance...");

            Project = project;

            // Construct paths
            string indexPath = Path.Combine(project.ProjectDirectory, IndexFile);
            string dataPath = Path.Combine(project.ProjectDirectory, DataFile);

            Initialize(indexPath, dataPath);
        }

        /// <summary>
        ///     Constructor.
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
                        // Calculate the entry count
                        long entryCount = (indexStream.Length - MagicNumber.Length) / IndexAlignment;

                        // Load Data
                        for (int i = 0; i < entryCount; i++)
                        {
                            // Load Address and length
                            long address = indexStream.ReadInt64();
                            long length = indexStream.ReadInt64();

                            // Entry is valid only if address is greater or equal than 0
                            if (address >= 0)
                            {
                                // Read Name
                                string name =
                                    new String(Encoding.UTF8.GetChars(indexStream.ReadBytes(IndexAlignment - 0x10)));
                                name = name.TrimEnd('\0');

                                // Add Entry
                                CacheEntry entry = new CacheEntry
                                {
                                    Address = address,
                                    Length = length,
                                    MetadataAddress = i * IndexAlignment + MagicNumber.Length,
                                    Name = name
                                };
                                entriesMap.Add(entry.Name, entry.Address);
                                entries.Add(entry.Address, entry);
                            }
                            else
                            {
                                // Add the unused entry to the holes
                                indexHoles.Enqueue(i * IndexAlignment + MagicNumber.Length);
                            }
                        }

                        // Scan for data holes
                        //var sortedEntries = entries.Values.OrderBy(t => t.Address).ToArray();
                        CacheEntry[] sortedEntries = entries.Values.ToArray(); // <-- this should be sorted correctly
                        for (int i = 0; i < entries.Count - 1; i++)
                        {
                            long lastEntryEnd = Align(sortedEntries[i].EndAddress, DataAlignment);
                            long nextEntryStart = sortedEntries[i + 1].Address;
                            long holeSize = nextEntryStart - lastEntryEnd;

                            // Positive hole size means a data hole, negative means corrupted data
                            if (holeSize > 0)
                                dataHoles.Add(lastEntryEnd, holeSize);
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
                    indexStream = null;
                    dataStream = null;
                }
            }
            else
                logger.Info("Cache index or data file not found, proceeding to creating empty cache.");

            if (indexStream == null && dataStream == null)
            {
                // Overwrite BOTH index and data files if one of them is missing
                // This is also the fallback route if cache files are corrupted
                indexStream = new StreamEx(indexFile, FileMode.Create);
                indexStream.WriteBytes(Encoding.ASCII.GetBytes(MagicNumber));
                dataStream = new StreamEx(dataFile, FileMode.Create);
            }

        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds data into cache.
        ///     In case of a duplicate entry, the old entry will be overwritten.
        /// </summary>
        /// <param name="name">Name of the entry.</param>
        /// <param name="data">Stream containing data to be cached.</param>
        public void AddEntry(String name, StreamEx data)
        {
            logger.Debug("Adding cache entry: ID = {0}; Length = {1}", name, data.Length);

            // If the entry name is too long, throw error
            if (Encoding.UTF8.GetByteCount(name) > (IndexAlignment - 0x10))
            {
                throw new ArgumentOutOfRangeException("name",
                    String.Format("Name too long! Name must be less than {0} bytes long when encoded.",
                        IndexAlignment - 0x10));
            }

            // If the entry already exists, delete it 
            if (entriesMap.ContainsKey(name))
            {
                RemoveEntry(name);
                logger.Trace("Attempting to add a duplicate entry, removing old data.");
            }

            // Find a hole to write the data
            Int64 address;
            KeyValuePair<long, long> hole = dataHoles.FirstOrDefault(t => t.Value >= data.Length);
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
                dataHoles.Remove(hole.Key);
                long newHoleAddress = Align(hole.Key + data.Length, DataAlignment);
                long newHoleLength = hole.Value - Align(data.Length, DataAlignment);

                Debug.Assert(newHoleLength > 0, "Hole size should be greater or equal than zero after resize.");
                Debug.Assert(newHoleAddress % DataAlignment == 0, "New hole address should be aligned.");
                Debug.Assert(newHoleLength % DataAlignment == 0, "New hole length should be aligned.");

                // Add it back if there is still hole after the write
                if (newHoleLength > DataAlignment)
                {
                    logger.Trace("Hole reduced to: Address = 0x{0}; Length = 0x{1}", newHoleAddress.ToHexString(8),
                        newHoleLength.ToHexString(8));
                    dataHoles.Add(newHoleAddress, newHoleLength);
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
            if (indexHoles.Count > 0)
            {
                indexAddress = indexHoles.Dequeue(); // If there are empty index slots, use those instead
                logger.Trace("Found an empty index slot at 0x{0}", indexAddress.ToHexString(8));
            }

            Debug.Assert(indexAddress % 0x10 == 0, "Index address should be aligned to 0x10.");

            // Write the index
            indexStream.Position = indexAddress;
            indexStream.WriteInt64(address);
            indexStream.WriteInt64(data.Length);
            byte[] encodedName = Encoding.UTF8.GetBytes(name);
            indexStream.WriteBytes(encodedName);
            indexStream.WriteBytes(new Byte[IndexAlignment - 0x10 - encodedName.Length]); // Padding

            // Save the index
            entries.Add(address, new CacheEntry
            {
                Name = name,
                Address = address,
                Length = data.Length,
                MetadataAddress = indexAddress
            });
            entriesMap.Add(name, address);

            // Force write data to stream
            indexStream.Flush();
            dataStream.Flush();
        }

        /// <summary>
        ///     Removes the entry from cache.
        ///     Please note that this method
        /// </summary>
        /// <param name="name"></param>
        public void RemoveEntry(String name)
        {
            logger.Debug("Removing an entry: ID = 0x{0}", name);

            // Check if there is an entry to remove
            if (!entriesMap.ContainsKey(name))
            {
                logger.Trace("No such entry to remove: ID = 0x{0}", name);
                return;
            }

            // There is such entry, overwrite its ID in index
            CacheEntry entry = entries[entriesMap[name]];
            indexStream.Position = entry.MetadataAddress;
            indexStream.WriteInt64(-1);

            // Remove the entry from index and mark is as empty
            entries.Remove(entry.Address);
            entriesMap.Remove(entry.Name);
            indexHoles.Enqueue(entry.MetadataAddress);

            // Create a hole
            long holeAddress = entry.Address;
            long holeSize = Align(entry.Length, DataAlignment);

            // Add the hole
            dataHoles.Add(holeAddress, holeSize);

            // Force write
            indexStream.Flush();
            dataStream.Flush();
        }

        /// <summary>
        ///     Returns a value indicating whether the cache contains the specified entry.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Boolean ContainsEntry(String name)
        {
            return entriesMap.ContainsKey(name);
        }

        /// <summary>
        ///     Gets the stream of a cache entry.
        ///     DO NOT CLOSE THE STREAM RETURNED BY THIS METHOD.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StreamEx GetEntry(String name)
        {
            logger.Debug("Getting cache entry: ID = 0x{0}", name);

            // Check if there is such an entry to get
            if (!entriesMap.ContainsKey(name))
            {
                logger.Error("Attempting to get an entry that does not exist: ID = 0x{0}", name);
                throw new KeyNotFoundException();
            }

            // Get the entry
            CacheEntry metadata = entries[entriesMap[name]];
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
        public StudioProject Project { get; private set; }

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
            dataHoles.Clear();
            indexHoles.Clear();
        }

        #endregion
    }
}