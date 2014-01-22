using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Animat.UI
{

    /// <summary>
    /// Animat Studio Project instance.
    /// </summary>
    [DataContract(Name = "AnimatProject")]
    public sealed class StudioProject
    {
        #region Directory Management

        // ReSharper disable InconsistentNaming
        [IgnoreDataMember] private const String PROJECT_FILE = "project.bxp";
        [IgnoreDataMember] private const String ASSET_DIR = "assets";
        [IgnoreDataMember] private const String FRAME_DIR = "frames";
        [IgnoreDataMember] private const String SEQ_DIR = "sequences";
        [IgnoreDataMember] private const String EVENT_DIR = "events";
        // ReSharper restore InconsistentNaming

        /* Disable Serialization */
        /// <summary>
        /// Path to the folder containing the project.
        /// </summary>
        [IgnoreDataMember]
        public String ProjectDirectory
        { get; set; }

        /// <summary>
        ///     Gets the project file by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetProjectFile(String name)
        {
            return Path.Combine(ProjectDirectory, PROJECT_FILE);
        }

        /// <summary>
        ///     Gets the asset directory of a project by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetAssetDirectory(String name)
        {
            return Path.Combine(ProjectDirectory, ASSET_DIR);
        }

        /// <summary>
        ///     Gets the frame directory of a project by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetFrameDirectory(String name)
        {
            return Path.Combine(ProjectDirectory, FRAME_DIR);
        }

        /// <summary>
        ///     Gets the sequence directory of a project by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetSequenceDirectory(String name)
        {
            return Path.Combine(ProjectDirectory, SEQ_DIR);
        }

        /// <summary>
        ///     Gets the event directory of a project by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public String GetEventDirectory(String name)
        {
            return Path.Combine(ProjectDirectory, EVENT_DIR);
        }

        #endregion

        #region Asset Management

        // Asset store
        [IgnoreDataMember]
        private Dictionary<String, StudioAsset> assets;

        /// <summary>
        /// Gets the thumbnail store for the project.
        /// </summary>
        [IgnoreDataMember]
        public StudioThumbnailStore ThumbnailStore
        { get; private set; }

        /// <summary>
        /// Raw representation of the asset list.
        /// In order to retrieve assets, please use Assets property.
        /// </summary>
        [DataMember(Name = "assets")]
        internal List<String> RawAssets { get; set; }

        /// <summary>
        /// Gets the enumerable collection of assets associated with this project.
        /// </summary>
        public IEnumerable<StudioAsset> Assets
        { get { return assets.Values; } }

        /// <summary>
        /// Gets an asset from the asset store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StudioAsset GetAsset(String name)
        {
            return assets.ContainsKey(name) ? assets[name] : null;
        }

        /// <summary>
        /// Removes an asset from the asset store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void removeAsset(String name)
        {
            // Get the asset and make sure it exists
            var asset = GetAsset(name);
            if (asset == null) return;

            // Remove it from asset store and thumbnail store
            assets.Remove(name);
            ThumbnailStore.RemoveThumbnail(asset);
        }

        
        

        #endregion
    }


    #region Assets
    
    /// <summary>
    /// Not Serializable.
    /// Contains basic information about an asset file.
    /// </summary>
    public sealed class StudioAsset
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filename"></param>
        public StudioAsset(StudioProject project, String filename)
        {
            // Check for nulls
            if (project == null) throw new ArgumentNullException("project");
            if (filename == null) throw new ArgumentNullException("filename");
            
            // Set up properties
            Project = project;
            Filename = filename;

            // Add to thumbnail store
            
        }

        /// <summary>
        /// Gets the project that this asset is associated with.
        /// </summary>
        public StudioProject Project
        { get; private set; }

        /// <summary>
        /// Name of the file, including the extension.
        /// </summary>
        public String Filename
        { get; private set; }

        /// <summary>
        /// Gets the full path of the asset file.
        /// </summary>
        public String FullPath
        {
            get
            {
                return Path.Combine(Project.ProjectDirectory, Filename);
            } 
        }

        /// <summary>
        /// Thumbnail of the asset file.
        /// </summary>
        public Image Thumbnail
        { get; set; }
    }



    #endregion


}
