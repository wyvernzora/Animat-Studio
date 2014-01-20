using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Animat.UI.Properties;
using libWyvernzora.IO;

namespace Animat.UI.Project
{
    /// <summary>
    /// Singleton Project Class.
    /// Holds references to all data used within the project.
    /// </summary>
    public class AnimatProject
    {
        #region Nested Types

        /// <summary>
        /// Indicates the scope of requested update.
        /// </summary>
        [Flags]
        public enum UpdateScope
        {
            None = 0,
            Resources = 1,
            Frames = 2,
            Sequences = 4,
            Events = 8,
            All = 0x7FFFFFFF
        }

        /// <summary>
        /// OnRequestUpdate event argument object.
        /// </summary>
        public sealed class UpdatedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets or sets the scope of the requested update.
            /// </summary>
            public UpdateScope Scope { get; set; }
        }


        #endregion

        #region Constants

        private const String PROJECT_FILE = "project.bxproj";
        private const String RESOURCE_DIR = "resources";
        private const String FRAMES_DIR = "frames";
        private const String SEQUENCES_DIR = "sequences";
        private const String EVENTS_DIR = "events";

        private const String DEFAULT_PROJECT_DIR = "Animat Studio Projects";

        #endregion

        #region Singleton

        private static AnimatProject instance = null;

        public static AnimatProject Instance
        {
            get { return instance; }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// Prevents creation of the class instances from outside the class.
        /// </summary>
        protected AnimatProject(String path)
        {
            // Set up variables
            ProjectDirectory = Path.GetDirectoryName(path);

            // Load project descriptor file
            if (!File.Exists(path))
                throw new FileNotFoundException("Cannot find the project descriptor file!", path);
            try
            {
                Model = ProjectModel.Deserialize(path);
            }
            catch (Exception x)
            {
                throw new InvalidDataException("Error loading the project descriptor file!");
            }
        }

        #region Properties

        /// <summary>
        /// Gets the name of the project
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets the path to the project directory.
        /// </summary>
        public String ProjectDirectory { get; protected set; }

        /// <summary>
        /// Gets the serializable project model.
        /// </summary>
        public ProjectModel Model { get; protected set; }
    


        #endregion

        #region Methods

        /// <summary>
        /// Saves the project and writes all changes to files.
        /// </summary>
        public void Save()
        {
            // Save project descriptor file
            ProjectModel.Serialize(Path.Combine(ProjectDirectory, PROJECT_FILE), Model);

            // TODO Save Everything Else
        }

        /// <summary>
        /// Imports the resource and adds the file to the resource list.
        /// </summary>
        /// <param name="path"></param>
        public void ImportResource(String path)
        {
            // Get the filename
            var filename = Path.GetFileName(path);
            if (filename == null)
                throw new Exception(String.Format("YuaiProject.ImportResource(string): Null Filename; Path = {0}", path));
            
            // Copy the resource to the resource directory
            File.Copy(path, Path.Combine(ProjectDirectory, RESOURCE_DIR, filename));

            // Add the resource to the project descriptor
            var relPath = Path.Combine(RESOURCE_DIR, filename);
            Model.Resources.Add(relPath);

            // Save Stuff
            Save();

            // Request Update
            RequestUiUpdate(UpdateScope.Resources);
        }

        #endregion

        #region Events

        // Static event handler for 
        private static EventHandler<UpdatedEventArgs> requestUiUpdate;

        /// <summary>
        /// Raised when YuaiProject changes and needs UI to be updated.
        /// </summary>
        public static event EventHandler<UpdatedEventArgs> OnRequestUiUpdate
        {
            add { requestUiUpdate += value; }
            remove { requestUiUpdate -= value; }
        }
        /// <summary>
        /// Request UI to be updated.
        /// </summary>
        public static void RequestUiUpdate(UpdateScope scope)
        {
            if (requestUiUpdate != null)
                requestUiUpdate(instance, new UpdatedEventArgs { Scope = scope });
        }

        #endregion

        #region Static Utilities

        /// <summary>
        /// Factory Method.
        /// Creates a project in the specified empty directory and initializes it.
        /// </summary>
        /// <param name="dir">Directory, where to create the new project.</param>
        /// <returns></returns>
        public static void Create(String dir, String name)
        {
            // Create directory name from project name
            String dirName = name;
            foreach (var c in Path.GetInvalidPathChars())
                dirName = dirName.Replace(c, '_');
            foreach (var c in Path.GetInvalidFileNameChars())
                dirName = dirName.Replace(c, '_');
            dirName = dirName.Replace(' ', '-');

            // Combine both names
            dir = Path.Combine(dir, dirName);

            // Create dir if it does not exist
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Check if it is empty
            if (Directory.GetFiles(dir).Length != 0 || Directory.GetDirectories(dir).Length != 0)
                throw new Exception("New projects can only be created in an empty directory!");

            // Initialize Subdirectories
            Directory.CreateDirectory(Path.Combine(dir, RESOURCE_DIR));  // Create Resources Folder
            Directory.CreateDirectory(Path.Combine(dir, FRAMES_DIR));  // Create Frames Folder
            Directory.CreateDirectory(Path.Combine(dir, SEQUENCES_DIR));  // Create Sequences Folder
            Directory.CreateDirectory(Path.Combine(dir, EVENTS_DIR));  // Create Events Folder

            // Create Project Descriptor File
                // TODO Serialize instead of writing
            //File.WriteAllText(Path.Combine(dir, PROJECT_FILE), String.Format(Resources.DefaultProjectDescriptor, name));
            var model = new ProjectModel()
                {
                    FPS = 10,
                    Name = name,
                    Resources = { },
                    FrameFiles = {"\\frames\\default.bxframe"},
                    SequenceFiles = {"\\sequences\\default.bxseq"},
                    EventFiles = {"\\events\\default.bxevent"}
                };
            ProjectModel.Serialize(Path.Combine(dir, PROJECT_FILE), model);

            // Create Default Files
            File.WriteAllText(Path.Combine(dir, FRAMES_DIR, "default.bxframe"), "{ }");
            File.WriteAllText(Path.Combine(dir, SEQUENCES_DIR, "default.bxseq"), "{ }");
            File.WriteAllText(Path.Combine(dir, EVENTS_DIR, "default.bxevent"), "{ }");

            // Load Default Project
            instance = new AnimatProject(Path.Combine(dir, PROJECT_FILE));
        }

        /// <summary>
        /// Loads a project from descriptor file.
        /// </summary>
        /// <param name="path"></param>
        public static void Load(String path)
        {
            instance = new AnimatProject(path);
        }

        #endregion

        #region Static Global Stuff

        /// <summary>
        /// Gets the default project folder
        /// </summary>
        public static String DefaultProjectFolder
        { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DEFAULT_PROJECT_DIR); } }

        /// <summary>
        /// Gets the current project folder.
        /// </summary>
        public static String ProjectFolder
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Settings.Default.ProjectFolder))
                {
                    Settings.Default.ProjectFolder = DefaultProjectFolder;
                    Settings.Default.Save();
                }

                return Settings.Default.ProjectFolder;
            }
            set
            {
                Settings.Default.ProjectFolder = value;
                Settings.Default.Save();
            }
        }

        #endregion
    }
}
