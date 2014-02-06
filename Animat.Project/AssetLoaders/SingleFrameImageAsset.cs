using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animat.Project.Moduality;
using libWyvernzora.Core;
using libWyvernzora.IO;

namespace Animat.Project.AssetLoaders
{
    /// <summary>
    /// Asset implementation for single frame images.
    /// </summary>
    public class SingleFrameImageAsset : AssetBase
    {
        private Image thumbnail = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">Project that this asset is associated with.</param>
        /// <param name="name">Display name of the asset file.</param>
        /// <param name="filename">Name of the asset file inside the asset directory.</param>
        public SingleFrameImageAsset(StudioProject project, String name, String filename)
            : base(project, name, filename)
        {
            
        }

        #region AssetBase Members

        /// <summary>
        /// Gets the number of frames in this asset.
        /// Since this is a single frame asset, this value is always 1.
        /// </summary>
        public override int FrameCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Builds or rebuilds the cache associated with the asset.
        /// </summary>
        public override void BuildCache()
        {
            // Get the cache manager of current project
            var cacheMgr = Project.CacheManager;

            // Cache the thumbnail
            var name = String.Format("{0}/C/0", ID);

            // Get the thumbnail
            var original = GetOriginalImage();
            thumbnail = GetThumbnailEx(original, Project.ThumbnailSize);

            // Write cache
            using (var temporary = new StreamEx(new MemoryStream()))
            {
                thumbnail.Save(temporary, ImageFormat.Png);
                temporary.Position = 0;
                cacheMgr.AddEntry(name, temporary);
            }

            // Release memory
            original.Dispose();
        }

        public override Image GetOriginalImage()
        {
            try
            {
                // Load original image and return it
                var path = Path.Combine(Project.GetAssetDirectory(), Filename);
                return Image.FromFile(path);
            }
            catch (Exception x)
            {
                // In case if there is an exception, set error flag
                Error = x;
                return null;
            }
        }

        public override Image GetFrameImage(int index)
        {
            // Check index
            if (index < 0 || index >= FrameCount)
                throw new ArgumentOutOfRangeException("index");

            // The only frame is the original image here
            return GetOriginalImage();
        }

        public override Image GetFrameThumbnail(int index)
        {
            // Check index
            if (index < 0 || index >= FrameCount)
                throw new ArgumentOutOfRangeException("index");

            // Get thumbnail if it is not cached
            if (thumbnail == null)
            {
                var name = String.Format("{0}/C/0", ID);

                // Rebuild cache if the thumbnail is not cached.
                if (!Project.CacheManager.ContainsEntry(name)) BuildCache();

                thumbnail = Image.FromStream(Project.CacheManager.GetEntry(name));
            }

            return thumbnail;
        }

        #endregion
    }
}
