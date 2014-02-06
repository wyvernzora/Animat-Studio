using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Animat.Studio;
using libWyvernzora.Core;
using NLog;

namespace Animat.Project
{
    /// <summary>
    /// Base class for all asset types.
    /// </summary>
    /// <remarks>
    /// All types of assets are treated as frame arrays.
    /// It is up to concrete asset implementations to utilize caching via CacheManager.
    /// </remarks>
    public abstract class AssetBase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor.
        /// </summary>
        protected AssetBase()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">The project that this asset is associated with.</param>
        /// <param name="name">Name of the asset.</param>
        /// <param name="filename">Name of the asset file in the asset folder.</param>
        protected AssetBase(StudioProject project, String name, String filename)
        {
            try
            {
                Project = project;
                Name = name;
                Filename = filename;

                var strID = Path.GetFileNameWithoutExtension(filename);
                ID = Guid.Parse(strID);

                if (!File.Exists(Path.Combine(Project.GetAssetDirectory(), Filename)))
                    throw new FileNotFoundException("Asset file not found!");
            }
            catch (Exception x)
            {
                Error = x;
            }
        }

        /// <summary>
        /// Gets the ID of the asset.
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// Gets the name of the asset.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets the filename of the asset.
        /// </summary>
        public String Filename { get; private set; }

        /// <summary>
        /// Gets or sets the project that this asset is associated with.
        /// </summary>
        public StudioProject Project { get; set; }

        /// <summary>
        /// Gets the error that occured while loading this asset.
        /// Null if nothing went wrong.
        /// </summary>
        public Exception Error { get; protected set; }

        /// <summary>
        /// Ggets the full path to the asset file.
        /// </summary>
        public String FullPath
        {
            get
            {
                return Path.Combine(Project.GetAssetDirectory(), Filename);
            }
        }

        /// <summary>
        /// Gets or sets the name of the factory that produced the implementation of the AssetBase.
        /// </summary>
        public String FactoryName { get; set; }

        /// <summary>
        /// Number of frames contained in this asset.
        /// </summary>
        public abstract Int32 FrameCount { get; }
        

        /// <summary>
        /// Clears and rebuilds the cache for this asset.
        /// May be time consuming!
        /// </summary>
        public abstract void BuildCache();

        /// <summary>
        /// Gets the original asset image (if available).
        /// </summary>
        /// <returns></returns>
        public abstract Image GetOriginalImage();

        /// <summary>
        /// Gets the specified full-size frame image.
        /// </summary>
        /// <param name="index">Index of the frame to get.</param>
        /// <returns></returns>
        public abstract Image GetFrameImage(Int32 index);

        /// <summary>
        /// Gets the scaled-down frame thumbnail.
        /// </summary>
        /// <param name="index">Index of the frame to get.</param>
        /// <returns></returns>
        public abstract Image GetFrameThumbnail(Int32 index);
        
        #region Static Utilities

        /// <summary>
        /// Wrapper around Image.GetThumbnailImage() method.
        /// </summary>
        /// <param name="img">Image to get the thumbnail of.</param>
        /// <param name="width">Width of the resulting thumbnail image.</param>
        /// <param name="height">Height of the resulting thumbnail image.</param>
        /// <returns></returns>
        public static Image GetThumbnailEx(Image img, int width, int height)
        {
            var thumb = new Bitmap(width, height);
            using (var gfx = Graphics.FromImage(thumb))
            {
                gfx.DrawImage(img, new Rectangle(0, 0, width, height));
            }

            return thumb;

            //return img.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero);
        }

        /// <summary>
        /// Wrapper around Image.GetThumbnailImage().
        /// </summary>
        /// <param name="img">Image to get the thumbnail of.</param>
        /// <param name="boxSize">Size of the rectangulat box the image has to fit in.</param>
        /// <returns></returns>
        public static Image GetThumbnailEx(Image img, int boxSize)
        {
            double scale = 1.0;
            if (img.Width > img.Height)
                scale = boxSize / (double)img.Width;
            else
                scale = boxSize / (double)img.Height;
            if (scale > 1) scale = 1.0;

            int nWidth = (int)(img.Width * scale);
            int nHeight = (int)(img.Height * scale);

            return GetThumbnailEx(img, nWidth, nHeight);
        }

        #endregion
    }
}
