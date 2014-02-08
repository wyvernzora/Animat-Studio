using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Animat.Foundation;
using Animat.Project.Moduality;
using Animat.Studio;
using libWyvernzora.Core;
using libWyvernzora.IO;
using libWyvernzora.Utilities;

namespace Animat.Project
{

    /// <summary>
    /// Animat Studio Project instance.
    /// </summary>
    [DataContract(Name = "AnimatProject")]
    public sealed class StudioProject
    {
        #region Nested Types

        [DataContract]
        internal class AssetInfo
        {
            [DataMember(Name = "name")]
            public String Name { get; set; }

            [DataMember(Name = "filename")]
            public String Filename { get; set; }

            [DataMember(Name = "loader")]
            public String AssetLoader { get; set; }
        }

        #endregion

        #region Life Cycle

        static StudioProject()
        {
            AssetLoaders = new ComponentFactoryLoader<AssetBase>();
        }

        private StudioProject()
        {
            this.Guid = Guid.NewGuid();
            RawAssets = new List<AssetInfo>();
            ThumbnailSize = 300;
        }

        public static StudioProject CreateProject(String path, String name)
        {
            // Check argument
            if (path == null) throw new ArgumentNullException("path");
            
            // Make sure root directory is there
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Set up the project
            var project = new StudioProject { Name = name, Fps = 10 };
            project.ProjectDirectory = path;

            // Set up directories
            if (!Directory.Exists(project.GetAssetDirectory()))
                Directory.CreateDirectory(project.GetAssetDirectory());

            project.SaveProject();
            
            // Create cache
            project.CacheManager = new CacheManager(project);

            return project;
        }

        public static StudioProject OpenProject(String path)
        {
            // Deserialize project from file
            StudioProject project = null;
            using (var stream = new StreamEx(path))
            {
                project = JsonUtils.Deserialize<StudioProject>(stream);
            }
            project.ProjectDirectory = Path.GetDirectoryName(path);

            // Sync raw lists to models
            project.assets = new Dictionary<string, AssetBase>();
            foreach (var p in project.RawAssets)
            {
                var factory = AssetLoaders[p.AssetLoader];
                project.assets.Add(p.Name, factory.Create(project, p.Name, p.Filename));
            }

            // Set up cache manager
            project.CacheManager = new CacheManager(project);

            // TODO Load up other stuff


            return project;
        }

        public void SaveProject()
        {
            // Sync models to raw lists
            RawAssets.Clear();
            RawAssets.AddRange(
                from a in Assets select
                    new AssetInfo
                    {
                        AssetLoader = a.FactoryName,
                        Filename = a.Filename,
                        Name = a.Name
                    }
                );

            // Serialize projec to project file
            using (var stream = new StreamEx(GetProjectFile(), FileMode.Create))
            {
                this.Serialize(stream);
            }
            
            // TODO Save other stuff
        }

        #endregion

        #region Basic Properties

        [DataMember(Name = "guid")]
        public Guid Guid
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        [DataMember(Name = "name")]
        public String Name
        { get; set; }

        /// <summary>
        /// Gets or sets the fps of the animation.
        /// </summary>
        [DataMember(Name = "fps")]
        public UInt32 Fps
        { get; set; }

        #endregion

        #region Directory Management

        // ReSharper disable InconsistentNaming
        [IgnoreDataMember] public const String PROJECT_FILE = "project.bxp";
        [IgnoreDataMember] public const String ASSET_DIR = "assets";
        [IgnoreDataMember] public const String CACHE_DIR = "cache";
        // ReSharper restore InconsistentNaming

        /* Disable Serialization */
        /// <summary>
        /// Path to the folder containing the project.
        /// </summary>
        [IgnoreDataMember]
        public String ProjectDirectory
        { get; set; }

        /// <summary>
        ///     Gets the project file path.
        /// </summary>
        /// <returns></returns>
        public String GetProjectFile()
        {
            return Path.Combine(ProjectDirectory, PROJECT_FILE);
        }

        /// <summary>
        ///     Gets the asset directory of the project.
        /// </summary>
        /// <returns></returns>
        public String GetAssetDirectory()
        {
            return Path.Combine(ProjectDirectory, ASSET_DIR);
        }

        /// <summary>
        /// Gets the cache directory of the project.
        /// </summary>
        /// <returns></returns>
        public String GetCacheDirectory()
        {
            // Get the path
            var path = Path.Combine(ProjectDirectory, CACHE_DIR);
            IOUtilities.EnsureDirectoryExists(path);    // Make sure that there is such a directory.
            return path;
        }

        #endregion

        #region Asset Management

        // Asset store
        [IgnoreDataMember]
        private Dictionary<String, AssetBase> assets
            = new Dictionary<string, AssetBase>();

        /// <summary>
        /// Raw representation of the asset list.
        /// In order to retrieve assets, please use Assets property.
        /// </summary>
        [DataMember(Name = "assets")]
        internal List<AssetInfo> RawAssets { get; set; }

        /// <summary>
        /// Gets the enumerable collection of assets associated with this project.
        /// </summary>
        [IgnoreDataMember]
        public IEnumerable<AssetBase> Assets
        { get { return assets.Values; } }

        /// <summary>
        /// Gets an asset from the asset store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AssetBase GetAsset(String name)
        {
            return assets.ContainsKey(name) ? assets[name] : null;
        }

        /// <summary>
        /// Adds an image to the asset store.
        /// </summary>
        /// <param name="filepath"></param>
        public AssetBase AddAsset(String filepath, String loader)
        {
            // Check arguments
            if (filepath == null) throw new ArgumentNullException("filepath");
            if (!File.Exists(filepath)) throw new FileNotFoundException("Cannot find the asset file to add.");

            // Generate a unique filename
            var assetId = Guid.NewGuid();
            var filename = assetId + Path.GetExtension(filepath);

            // Copy the file to the asset folder
            var newPath = Path.Combine(GetAssetDirectory(), filename);
            File.Copy(filepath, newPath);

            // Figure out the best name
            var name = Path.GetFileNameWithoutExtension(filepath);
            if (assets.ContainsKey(name))
            {
                int i = 1;
                while (assets.ContainsKey(String.Format("{0}({1})", name, i))) i++;
                name = String.Format("{0}({1})", name, i);
            }

            // Create the asset 
            var factory = AssetLoaders[loader];
            var asset = factory.Create(this, name, filename);
            assets.Add(asset.Name, asset);
            

            // Request Update
            //StudioCore.Instance.RequestUpdate(UpdateScope.Explorer);

            return asset;
        }

        /// <summary>
        /// Removes an asset from the asset store.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void RemoveAsset(String name)
        {
            // Get the asset and make sure it exists
            var asset = GetAsset(name);
            if (asset == null) return;

            // Remove it from asset store and thumbnail store
            assets.Remove(name);

            // Delete the asset file
            if (File.Exists(asset.FullPath))
                File.Delete(asset.FullPath);
        }

        /// <summary>
        /// Renames an asset and moves associated files.
        /// </summary>
        /// <param name="name">Name of the asset to rename.</param>
        /// <param name="newName">New name for the asset.</param>
        public void RenameAsset(String name, String newName)
        {
            var asset = GetAsset(name);
            if (asset == null) return;

            // Figure out the best name (in case of duplicates)
            if (assets.ContainsKey(newName))
            {
                int i = 1;
                while (!assets.ContainsKey(String.Format("{0}({1})", newName, i))) i++;
                newName = String.Format("{0}({1})", newName, i);
            }

            asset.Name = newName;
            assets.Remove(name);
            assets.Add(newName, asset);

            SaveProject();
        }
    
        #endregion

        #region Cache Management

        /// <summary>
        /// Gets the cache manager for the project.
        /// </summary>
        [IgnoreDataMember]
        public CacheManager CacheManager
        { get; private set; }  // TODO Properly dispose of the cache manager

        /// <summary>
        /// Gets or sets the thumbnail size.
        /// </summary>
        public Int32 ThumbnailSize
        { get; set; }

        #endregion

        #region  Modular Components

        /// <summary>
        /// Gets the component loader for the AssetBase factories.
        /// </summary>
        public static ComponentFactoryLoader<AssetBase> AssetLoaders
        { get; private set; }

        #endregion
    }

}
