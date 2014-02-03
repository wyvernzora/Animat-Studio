using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Animat.Foundation;
using Animat.Project;
using libWyvernzora.IO;
using NLog;

namespace Animat.Studio
{
    /// <summary>
    /// Singleton.
    /// Animat Studio Settings.
    /// </summary>
    [DataContract(Name = "animat-settings")]
    public class StudioSettings : INotifyPropertyChanged
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public const String ConfigFilename = "Animat.Studio.cfg";

        #region Singleton

        private static StudioSettings instance;

        /// <summary>
        /// Gets the global instance.
        /// </summary>
        public static StudioSettings Instance
        {
            get
            {
                if (instance == null) InitializeInstance();
                return instance;
            } 
        }

        /// <summary>
        /// Loads the config file.
        /// </summary>
        public static void InitializeInstance()
        {
            logger.Debug("Starting to load configuration file.");
            if (instance == null)
            {
                var path = Path.Combine(Application.StartupPath, ConfigFilename);
                // Attemp to load the existing file first
                if (File.Exists(path))
                {
                    logger.Trace("Configuration file found, attempting to load.");
                    try
                    {
                        using (var stream = new StreamEx(path))
                        {
                            instance = JsonUtils.Deserialize<StudioSettings>(stream);
                        }
                        logger.Info("Successfully loaded the configuration file.");
                    }
                    catch (Exception x)
                    {
                        logger.Error("Failed to load existing configuration file. Exception Type: {0}; Message: {1}", x.GetType().Name, x.Message);
                        MessageBox.Show(String.Format("Failed to load the configuration file!\nYour settings will be restored to defaults.\n\nError info: {0}", x.Message),
                            "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // If instance is still null, either no file exists or load failed.
                if (instance == null)
                {
                    instance = new StudioSettings();
                    instance.Save();
                    logger.Info("Created new configuration file with default values.");
                }
            }
        }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static StudioSettings()
        {
            // Initialize default project dir.
            DefaultProjectDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Animat Studio Projects");

        }

        #endregion

        /// <summary>
        /// Constructors.
        /// Creates a default settings file.
        /// </summary>
        protected StudioSettings()
        {
            RecentProjects = new List<RecentProjectInfo>();
        }

        [OnDeserialized]
        protected void OnDeserialized(StreamingContext context)
        {
            PropertyChanged += (@s, e) => Save();
        }

        /// <summary>
        /// Saves all data to the config file and overwrites all previous data.
        /// </summary>
        public void Save()
        {
            logger.Info("Saving configuration file.");
            var path = Path.Combine(Application.StartupPath, ConfigFilename);
            using (var stream = new StreamEx(path, FileMode.Create, FileAccess.Write)) 
            {
                this.Serialize(stream);
            }
        }

        #region Internal workings of this class

        // When true, PropertyChanged event will be suppressed
        private Boolean suppressChangedEvent = true;

        /// <summary>
        /// Fired when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Fires the PropertyChanged event.
        /// </summary>
        /// <param name="name">Name of the property that changes.</param>
        protected void FirePropertyChanged(String name)
        {
            if (!suppressChangedEvent && PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        
        #endregion
        
        #region Directory Management

        /// <summary>
        /// Represents the default name for the directory storing studio projects.
        /// </summary>
        public static readonly String DefaultProjectDirectory;

        protected String projectStore;

        /// <summary>
        /// Gets or sets the path to the directory where projects are stored.
        /// </summary>
        [DataMember(Name = "project-store")]
        public String ProjectStore
        {
            get {
                return String.IsNullOrWhiteSpace(projectStore) 
                    ? DefaultProjectDirectory : projectStore;
            }
            set
            {
                if (projectStore != value)
                {
                    projectStore = value;
                    FirePropertyChanged("ProjectStore");
                }
            }
        }

        /// <summary>
        ///     Processes the project name to comply with filename requirements.
        /// </summary>
        /// <param name="projectName">Raw project name.</param>
        /// <returns></returns>
        public String ProcessProjectName(String projectName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                projectName = projectName.Replace(c, '_');
            foreach (char c in Path.GetInvalidPathChars())
                projectName = projectName.Replace(c, '_');
            projectName = projectName.Replace(' ', '-');

            return projectName;
        }

        /// <summary>
        ///     Gets the path to a project by name.
        /// </summary>
        /// <param name="name">Unprocessed project name.</param>
        /// <returns></returns>
        public String GetProjectDirectory(String name)
        {
            return Path.Combine(ProjectStore, ProcessProjectName(name));
        }



        #endregion

        #region Thumbnail Generation

        protected UInt32 thumbnailSize = 300;

        /// <summary>
        /// Gets or sets the size of the generated thumbnails.
        /// </summary>
        [DataMember(Name = "thumbnail-size")]
        public UInt32 ThumbnailSize
        {
            get { return thumbnailSize; }
            set
            {
                if (thumbnailSize != value)
                {
                    thumbnailSize = value;
                    FirePropertyChanged("ThumbnailSize");
                }
            }
        }

        #endregion

        #region Recent Projects

        public const Int32 RecentProjectsListCapacity = 10;

        [DataContract(Name = "recent-project-info")] public sealed class RecentProjectInfo
        {
            [DataMember(Name = "name")]
            public String Name { get; set; }
            [DataMember(Name = "path")]
            public String Path { get; set; }
            [DataMember(Name = "id")]
            public Guid ID { get; set; }
            [DataMember(Name = "is-pinned")]
            public Boolean IsPinned { get; set; }
        }

        /// <summary>
        /// Gets or sets the list of recent projects.
        /// </summary>
        [DataMember(Name = "recent-projects")]
        public List<RecentProjectInfo> RecentProjects
        { get; set; }

        /// <summary>
        /// Pushes a project into the recent projects list.
        /// Pinned projects will not be removed.
        /// </summary>
        /// <param name="project"></param>
        public void PushRecentProject(StudioProject project)
        {
            var existing =
                RecentProjects.FindIndex(i => i.ID.Equals(project.Guid));

            if (existing == -1)
            {
                logger.Info("Adding {0} to the recent project list.", project.Name);
                var info = new RecentProjectInfo
                {
                    ID = project.Guid,
                    Name = project.Name,
                    Path = Path.Combine(project.ProjectDirectory, StudioProject.PROJECT_FILE),
                    IsPinned = false
                };

                // If the list if at capacity
                if (RecentProjects.Count >= RecentProjectsListCapacity)
                {
                    // Remove the last unpinned entry
                    var unpinned = RecentProjects.FindLastIndex(i => !i.IsPinned);
                    if (unpinned != -1)
                    {
                        RecentProjects.RemoveAt(unpinned);
                        RecentProjects.Insert(0, info);
                        FirePropertyChanged("RecentProjects");
                        logger.Trace("Recent projects list at capacity, removed the last unpinned.");
                    }
                    else logger.Trace("Recent projects list at capacity, no unpinned entries to remove.");
                }
                else
                {
                    RecentProjects.Insert(0, info);
                    FirePropertyChanged("RecentProjects");
                }
            }
            else
            {
                logger.Info("Moving {0} to the top of the RecentProjects list.", project.Name);
                var info = RecentProjects[existing];
                RecentProjects.RemoveAt(existing);
                RecentProjects.Insert(0, info);
            }
        }

        /// <summary>
        /// Gets a recent project by ID.
        /// Throws exception if no such project found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RecentProjectInfo FindProjectById(String id)
        {
            var guid = Guid.Parse(id);
            var project = RecentProjects.Find(t => t.ID.Equals(guid));
            if (project == null) throw new KeyNotFoundException();
            return project;
        }

        #endregion

    }
}
