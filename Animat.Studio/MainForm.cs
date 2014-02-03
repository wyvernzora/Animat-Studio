using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.Project;
using Animat.Studio.Properties;
using Animat.Studio.ToolWindows;
using Animat.Studio.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;
using NLog;

namespace Animat.Studio
{
    public partial class MainForm : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Global Access to this class

        /// <summary>
        /// Gets the last instance of the form.
        /// </summary>
        public static MainForm Instance { get; private set; }

        #endregion
        
        public MainForm()
        {
            logger.Info("Starting to initialize the main window.");

            // Detect multiple instances
            if (Instance != null)
                throw new DuplicateInstanceException(typeof(MainForm));
            Instance = this;
            
            // Load Settings
            StudioSettings.InitializeInstance();

            // Attach update handler
            StudioCore.Instance.OnUpdateRequest += (@s, e) =>
            {
            };

            // Default exception handling
            Application.ThreadException += (@s, e) => (new ErrorWindow(e.Exception)).ShowDialog(this);

            // Initialize Components
            InitializeComponent();

            // Change Docking Renderer
            DockPanelManager.RenderMode = DockPanelRenderMode.Office2007Silver;

            // Initialize Layout
            InitializeLayout();

            // Attach Event Handlers
            AttachGraphicsEvents();
            AttachEventHandlers();

            logger.Info("Completed initialization of the main window.");
        }   
        
        #region Dock and Layout Management

        public void InitializeLayout()
        {
            // Layout
            using (var lk = new ActionLock(
                () => dockPanel.SuspendLayout(true),
                () => dockPanel.ResumeLayout(true, true)))
            {
                ResourceExplorer.Instance.Show(dockPanel, DockState.DockLeft);
                PreviewWindow.Instance.Show(ResourceExplorer.Instance.Pane, DockPaneAlignment.Bottom, 0.4);
                StartPage.Instance.Show(dockPanel, DockState.Document);

            }
        }

        #endregion

        #region UI Utilities

        public void CreateProject()
        {
            var newProjDialog = new NewProjectWizard();
            if (newProjDialog.ShowDialog(this) == DialogResult.OK)
            {
                StudioCore.Instance.Project = 
                    StudioProject.CreateProject(newProjDialog.ProjectPath, newProjDialog.ProjectName);
                StudioSettings.Instance.PushRecentProject(StudioCore.Instance.Project);
            }
        }

        public void LoadProject()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Animat Studio Project (*.bxp)|*.bxp";
            dialog.Multiselect = false;
            dialog.InitialDirectory = StudioSettings.Instance.ProjectStore;
            if (dialog.ShowDialog(this) == DialogResult.OK)
                LoadProject(dialog.FileName);
        }

        public void LoadProject(String path)
        {
            if (StudioCore.Instance.Project != null)
                StudioCore.Instance.Project.CacheManager.Dispose();
            StudioCore.Instance.Project = StudioProject.OpenProject(path);
            StudioCore.Instance.Project.ThumbnailSize = (Int32) StudioSettings.Instance.ThumbnailSize;
            StudioSettings.Instance.PushRecentProject(StudioCore.Instance.Project);
            StartPage.Instance.Close();
        }

        public void CloseProject(Boolean showStartPage = false)
        {
            if (StudioCore.Instance.HasProject)
                StudioCore.Instance.Project.CacheManager.Dispose();
            
            // Close all windows
            foreach (var a in StudioCore.Instance.Project.Assets) {
                var viewer = AssetViewer.GetInstance(a);
                if (viewer != null) viewer.Close();
            }
            
            StudioCore.Instance.Project = null;

            if (showStartPage) StartPage.Instance.Show(dockPanel);

        }

        private void ImportAsset()
        {
            if (StudioCore.Instance.Project == null) return;

            var dialog = new OpenFileDialog();
            var factories = StudioProject.AssetLoaders.Factories.ToArray();
            dialog.Filter = String.Join("|", from f in factories select f.Filter);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var asset = StudioCore.Instance.Project.AddAsset(dialog.FileName, factories[dialog.FilterIndex - 1].Name);
                StudioCore.Instance.Project.SaveProject();

                // Initialize cache immideately.
                asset.BuildCache();

                // Send out update request
                StudioCore.Instance.RequestUpdate(UpdateScope.Explorer);
            }
        }

        #endregion

        #region Cross-Window Interop

        public void PushDockableWindow(DockableForm form, DockState state)
        {
            form.Show(dockPanel, state);
        }

        #endregion

    }
}
