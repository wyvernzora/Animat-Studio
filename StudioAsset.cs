using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Animat.UI.Utilities;
using libWyvernzora.Core;

namespace Animat.UI
{
    /// <summary>
    /// Enum indicating the format of the asset.
    /// </summary>
    public enum AssetType
    {
// ReSharper disable InconsistentNaming
        /// <summary>
        /// PNG Image
        /// </summary>
        PNG,
        /// <summary>
        /// Bitmap Image
        /// </summary>
        BMP,
        /// <summary>
        /// JPG Image
        /// </summary>
        JPG,
        /// <summary>
        /// GIF Image
        /// </summary>
        GIF,
        /// <summary>
        /// Amimat Resource
        /// </summary>
        ARES,
        /// <summary>
        /// BarloX Animation
        /// </summary>
        BXA,
        /// <summary>
        /// Interactive Barlox Animation
        /// </summary>
        IBXA
// ReSharper restore InconsistentNaming
    }

    /// <summary>
    /// Asset descriptor file.
    /// </summary>
    /// <remarks>
    /// Every asset is treated as an image array, where single-frame images
    /// such as PNG are arrays with 1 element. 
    /// </remarks>
    [DataContract(Name = "asset")]
    public class StudioAsset
    {
        #region Constructor and Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filename"></param>
        /// <param name="name"></param>
        public StudioAsset(StudioProject project, String filename, String name)
        {
            // Initialize Fields
            Filename = filename;
            Name = name;

            Initialize(project);
        }

        /// <summary>
        /// Initializes derived project variables.
        /// </summary>
        /// <param name="project"></param>
        public void Initialize(StudioProject project)
        {
            Project = project;

            // Load up the image
            var original = Image.FromFile(FullPath);
            var dimension = new FrameDimension(original.FrameDimensionsList[0]);
            FrameCount = original.GetFrameCount(dimension);
            original.Dispose();

            cachedThumbnails = new Image[FrameCount];

        }

        #endregion
        
        #region Non-serializable Fields

        // Thumbnail cache.
        [IgnoreDataMember] 
        private Image[] cachedThumbnails;

        #endregion

        #region Serializable Properties

        /// <summary>
        /// Gets the name of the asset.
        /// </summary>
        [DataMember(Name = "name")]
        public String Name
        { get; internal set; }

        /// <summary>
        /// Gets the filename of the asset, excluding path
        /// but including the extension.
        /// </summary>
        [DataMember(Name = "file")]
        public String Filename
        { get; internal set; }

        #endregion

        #region Non-serializable (Derived) Properties

        /// <summary>
        /// Gets the project that this asset is associated with.
        /// </summary>
        [IgnoreDataMember]
        public StudioProject Project
        { get; private set; }

        /// <summary>
        /// Gets the type of the asset.
        /// </summary>
        [IgnoreDataMember]
        public AssetType Type
        {
            get
            {
                AssetType t;
                if (!AssetType.TryParse(Path.GetExtension(Filename).TrimStart('.'), out t))
                    throw new InvalidDataException(String.Format("Unknown Asset Type: Name = {0}", Filename));
                return t;
            }
        }

        /// <summary>
        /// Gets the full path to the asset file.
        /// The path is to the file ITSELF and not any of its
        /// cached copies.
        /// </summary>
        [IgnoreDataMember]
        public String FullPath
        { get { return Path.Combine(Project.GetAssetDirectory(), Filename); } }

        /// <summary>
        /// Gets the number 
        /// </summary>
        [IgnoreDataMember]
        public Int32 FrameCount
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets a value containing details on the error
        /// occured while loading this asset.
        /// </summary>
        [IgnoreDataMember]
        public Exception Error
        { get; set; }

        #endregion
        
        #region Methods

        /* Note:
         * Calling GetFrame() and GetFrameThumbnail() when there is no
         * cache present is extremely inefficient. Please consider
         * rebuilding cache for an asset if necessary. 
         */

        /// <summary>
        /// Rebuilds the asset cache on a background thread.
        /// </summary>
        public void RebuildCacheAsync(Action callback)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (@s, e) => RebuildCache();
            bw.RunWorkerCompleted += (@s, e) => callback();
            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Rebuilds the asset cache on the caller thread.
        /// </summary>
        /// <param name="allowMemCache">Specifies whether in-memory cache of thumbnails is allowed.</param>
        public void RebuildCache(Boolean allowMemCache = true)
        {
            // Load up the original image
            var original = Image.FromFile(FullPath);
            var dimension = new FrameDimension(original.FrameDimensionsList[0]);

            // Get path components
            var cacheDir = Project.GetCacheDirectory();
            var fname = Path.GetFileNameWithoutExtension(Filename);
            var fext = Path.GetExtension(Filename);

            // Remove all existing cache
            for (int i = 0; i < FrameCount; i++)
            {
                // Select active frame
                original.SelectActiveFrame(dimension, i);

                // Construct frame cache path
                var fpath = Path.Combine(cacheDir,
                    String.Format("{0}.F{1}.png", fname, DirectIntConv.ToHexString(i, 8)));
                var cpath = Path.Combine(cacheDir,
                    String.Format("{0}.C{1}.png", fname, DirectIntConv.ToHexString(i, 8)));

                // Delete cache files if they exist
                if (File.Exists(fpath)) File.Delete(fpath);
                if (File.Exists(cpath)) File.Delete(cpath);

                // Rebuild Cache
                original.Save(fpath, ImageFormat.Png);
                var thumb = original.GetThumbnailEx(Properties.Settings.Default.ThumbnailSize);
                thumb.Save(cpath, ImageFormat.Png);
                if (allowMemCache)
                {
                    if (cachedThumbnails[i] != null) cachedThumbnails[i].Dispose();
                    cachedThumbnails[i] = thumb;
                }
            }

            // Dispose original image
            original.Dispose();
        }

        /// <summary>
        /// Gets a frame of the asset, preferring the cached version.
        /// </summary>
        /// <param name="frameIndex"></param>
        /// <returns></returns>
        public Image GetFrame(Int32 frameIndex)
        {
            // Check param
            if (frameIndex < 0 || frameIndex >= FrameCount)
                throw new ArgumentOutOfRangeException("frameIndex");

            // Construct frame cache path
            var path = Path.Combine(
                    Project.GetCacheDirectory(),
                    String.Format("{0}.F{1}.png", 
                        Path.GetFileNameWithoutExtension(Filename), 
                        DirectIntConv.ToHexString(frameIndex, 8)));

            // Create cache if there is none
            if (!File.Exists(path))
            {
                // Load image from file
                var original = Image.FromFile(FullPath);
                
                // Select current frame
                var dimension = new FrameDimension(original.FrameDimensionsList[0]);
                original.SelectActiveFrame(dimension, frameIndex);

                // Write the cache
                original.Save(path, ImageFormat.Png);

                // Dispose the original image
                original.Dispose(); 
            }
            
            // Load up the cache
            return Image.FromFile(path);
        }

        /// <summary>
        /// Gets the thumbnail of a frame, preferrably from cache.
        /// </summary>
        /// <param name="frameIndex"></param>
        /// <returns></returns>
        public Image GetFrameThumbnail(Int32 frameIndex)
        {
            // Check param
            if (frameIndex < 0 || frameIndex >= FrameCount)
                throw new ArgumentOutOfRangeException("frameIndex");

            // Return cached copy if there is one
            if (cachedThumbnails != null && cachedThumbnails[frameIndex] != null)
                return cachedThumbnails[frameIndex];

            // Construct frame cache path
            var path = Path.Combine(
                    Project.GetCacheDirectory(),
                    String.Format("{0}.C{1}.png",
                        Path.GetFileNameWithoutExtension(Filename),
                        DirectIntConv.ToHexString(frameIndex, 8)));

            // Get the frame and generate the thumbnail
            if (!File.Exists(path))
            {
                var frame = GetFrame(frameIndex);
                frame.GetThumbnailEx(Properties.Settings.Default.ThumbnailSize).Save(path);
                frame.Dispose();
            }

            // Load the frame thumbnail
            var thumb = Image.FromFile(path);
            return thumb;
        }
        
        #endregion
    }

    
}
