using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Animat.UI.Properties;

namespace Animat.UI.Project
{
    /// <summary>
    /// Singleton Project Class.
    /// Holds references to all data used within the project.
    /// </summary>
    public class YuaiProject
    {
        #region Constants

        private const String PROJECT_FILE = "project.bxproj";
        private const String RESOURCE_DIR = "resources";
        private const String FRAMES_DIR = "frames";
        private const String SEQUENCES_DIR = "sequences";
        private const String EVENTS_DIR = "events";

        private const String DEFAULT_PROJECT_DIR = "Animat Studio Projects";

        #endregion

        #region Singleton

        private static YuaiProject instance = null;

        public static YuaiProject Instance
        {
            get { return instance; }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// Prevents creation of the class instances from outside the class.
        /// </summary>
        protected YuaiProject(String path, String name)
        {
            Name = name;
            ProjectDirectory = Path.GetDirectoryName(path);
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
            File.WriteAllText(Path.Combine(dir, PROJECT_FILE), String.Format(Resources.DefaultProjectDescriptor, name));

            // Create Default Files
            File.WriteAllText(Path.Combine(dir, FRAMES_DIR, "default.bxframe"), "{ }");
            File.WriteAllText(Path.Combine(dir, SEQUENCES_DIR, "default.bxseq"), "{ }");
            File.WriteAllText(Path.Combine(dir, EVENTS_DIR, "default.bxevent"), "{ }");

            // Load Default Project
            instance = new YuaiProject(Path.Combine(dir, PROJECT_FILE), name);
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
