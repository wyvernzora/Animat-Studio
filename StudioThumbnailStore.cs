using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Animat.UI.Utilities;
using libWyvernzora.IO;
using libWyvernzora.Owl.Markup;

namespace Animat.UI
{
    /* OBML:
     * 
     * ThumbDB - 0x1CC9 - Root Node
     *      Thumbnail - 0x1CCA - Thumbnail Wrapper
     *          Name - 0x1CCB - Thumbnail Name in UTF-8
     *          [Data] - Thumbnail Stream
     */

    /// <summary>
    /// Thumbnail database for the StudioProject file.
    /// </summary>
    public class StudioThumbnailStore
    {
// ReSharper disable InconsistentNaming
        private const string STORE_FILE = "thumbstore.obml";
        private const UInt16 STORE_ROOT_ID = 0x1CC9;
        private const UInt16 THUMB_NODE_ID = 0x1CCA;
        private const UInt16 THUMB_NAME_ID = 0x1CCB;
// ReSharper restore InconsistentNaming

        // Thumbnail store.
        private Dictionary<String, Image> store;

        /// <summary>
        /// Constructor.
        /// </summary>
        public StudioThumbnailStore()
        {
            // Set up store to ignore case
            store = new Dictionary<string, Image>(StringComparer.CurrentCultureIgnoreCase);
        }


        #region Thumbnail Management

        /// <summary>
        /// Gets the thumbnail image by StudioAsset.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Image this[StudioAsset asset]
        {
            get
            {
                if (store.ContainsKey(asset.Filename))
                    return store[asset.Filename];
                else
                    return null;
            }
        }

        /// <summary>
        /// Adds an asset's thumbnail to the thumbnail store.
        /// </summary>
        /// <param name="asset">StudioAsset to add.</param>
        /// <param name="size">Size of the thumbnail box; -1 for default from settings.</param>
        /// <param name="forceUpdate">Determines whether to force thumbnail update.</param>
        public void AddThumbnail(StudioAsset asset, Int32 size = -1, Boolean forceUpdate = false)
        {
            // Check if it's already there
            if (store.ContainsKey(asset.Filename) && !forceUpdate)
                return;

            // Get size
            if (size < 0)
                size = Properties.Settings.Default.ThumbnailSize;

            // Generate the thumbnail
            var thumbnail = Image.FromFile(asset.FullPath).GetThumbnailEx(size);
            store[asset.Filename] = thumbnail;
            asset.Thumbnail = thumbnail;
        }

        /// <summary>
        /// Removes an asset's thumbnail from the thumbnail store.
        /// </summary>
        /// <param name="asset"></param>
        public void RemoveThumbnail(StudioAsset asset)
        {
            store.Remove(asset.Filename);
        }


        /// <summary>
        /// Writes all thumbnails to the disk.
        /// </summary>
        /// <param name="project"></param>
        public void Save(StudioProject project)
        {
            List<StreamEx> streamHolder = new List<StreamEx>(); // holds memory streams for closure.
            var storeRoot = new OwlNode(STORE_ROOT_ID); // Thumbnail Store Root

            foreach (var thumb in store)
            {
                // Create Thumbnail Root
                var thumbNode = new OwlNode(THUMB_NODE_ID);
                
                // Create Name Node
                var nameNode = new OwlNode(THUMB_NAME_ID);
                var nameStream = new StreamEx(new MemoryStream());    // Temporary stream for name
                streamHolder.Add(nameStream);
                nameStream.WriteBytes(Encoding.UTF8.GetBytes(thumb.Key));
                nameNode.SetData(nameStream);
                thumbNode.Children.Add(nameNode);

                // Create Data
                var data = new StreamEx(new MemoryStream());
                streamHolder.Add(data);
                thumb.Value.Save(data, ImageFormat.Png);
                thumbNode.SetData(data);

                // Add Node to the root
                storeRoot.Children.Add(thumbNode);
            }
            
            // Open the store file
            var storePath = Path.Combine(project.ProjectDirectory, STORE_FILE);
            using (var stream = new StreamEx(storePath, FileMode.Create, FileAccess.Write))
            {
                storeRoot.WriteToStream(stream);
            }

            // Close all temporary streams
            foreach (var s in streamHolder)
                s.Dispose();
        }

        /// <summary>
        /// Loads/reloads the thumbnail store from disk.
        /// </summary>
        /// <param name="project"></param>
        public void Load(StudioProject project)
        {
            // Clear all store items before reloading
            store.Clear();

            // Start reading data
            var storePath = Path.Combine(project.ProjectDirectory, STORE_FILE);
            using (var stream = new StreamEx(storePath, FileMode.Open, FileAccess.Read))
            {
                // Read the Store Root
                var storeRoot = new OwlNode(stream);

                // Get all Thumbnail Nodes
                var thumbNodes = storeRoot.FindChildren(THUMB_NODE_ID);
                foreach (var thumb in thumbNodes)
                {
                    // Get Name
                    var nameNode = thumb.FindChild(THUMB_NAME_ID);
                    if (nameNode == null)
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                            System.Diagnostics.Debugger.Break();
                        else
                            continue; // If not debugging, skip nodes with undefined name
                    }
                    var name = new string(Encoding.UTF8.GetChars(nameNode.Data.ReadBytes((int)nameNode.Data.Length)));

                    // Get Thumbnail Image
                    var thumbnail = Image.FromStream(thumb.Data);

                    // Add to store
                    store.Add(name, thumbnail);
                }
            }
        }
    
        #endregion

    }
}
