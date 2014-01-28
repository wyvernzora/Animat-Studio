using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animat.UI;
using libWyvernzora.Core;
using libWyvernzora.IO;

namespace Animat.Project
{
    /// <summary>
    /// Asset Loader for plain, single-frame images.
    /// </summary>
    [CustomAsset(Name = "Plain Image Asset Loader", Author = "Aragorn Wyvernzora", Version = "1.0",
        Filter = "Image File (*.png; *.jpg; *.png)|*.png;*.jpg;*.bmp")]
    public class PlainImageAsset : AssetBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">The project that this asset is associated with.</param>
        /// <param name="name">Name of the asset.</param>
        /// <param name="id">ID of the asset.</param>
        /// <param name="ext">File extension of the asset.</param>
        public PlainImageAsset(StudioProject project, string name, int id, string ext) 
            : base(project, name, id, ext)
        {
        }

        /// <summary>
        /// Number of frames contained in this asset.
        /// </summary>
        public override int FrameCount
        {
            get { return 1; }
        }


        public override void BuildCache()
        {
            // Get the cache manager
            var cache = Project.CacheManager;

            // Cache the thumbnail, and do not (!) cache the original
                // since there is only 1 frame
            Int64 entryId = (ID << 32) | 0x1 << 31;

            // Generate the thumbnail
            var original = GetOriginalImage();
            var thumbnail = GetThumbnailEx(original, Project.ThumbnailSize);

            // Write cache
            using (var temp = new StreamEx(new MemoryStream()))
            {
                thumbnail.Save(temp, ImageFormat.Jpeg);
                temp.Position = 0;
                cache.AddEntry(entryId, temp);
            }
        }


        public override Image GetOriginalImage()
        {
            try
            {
                var path = Path.Combine(Project.GetAssetDirectory(), Filename);
                return Image.FromFile(path);
            }
            catch (Exception x)
            {
                Error = x;
                return null;
            }
        }


        public override Image GetFrameImage(int index)
        {
            // Check if index is within range
            if (index < 0 || index >= FrameCount)
                throw new ArgumentOutOfRangeException("index");

            return GetOriginalImage();
        }


        public override Image GetFrameThumbnail(int index)
        {
            // Check if index is within range
            if (index < 0 || index >= FrameCount)
                throw new ArgumentOutOfRangeException("index");
            
            // Get the cache manager
            var cache = Project.CacheManager;
            
            // If the image is not cached for whatever reason, do that now.
            Int64 id = (ID << 32) | (0x1 << 31);
            if (!cache.ContainsEntry(id)) BuildCache();

            // Read cached image
            return Image.FromStream(cache.GetEntry(id));
        }
    }


}