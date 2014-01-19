using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.UI.Project;
using Animat.UI.Properties;
using Animat.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;

namespace Animat.UI
{
    public partial class MainForm : Form
    {
        #region Global Access to this class

        /// <summary>
        /// Gets the last instance of the form.
        /// </summary>
        public static MainForm Instance { get; private set; }

        #endregion

        // List of UI elements that need updating with state changes
        readonly List<IUpdateState> updateable = new List<IUpdateState>(); 


        public MainForm()
        {
            // Detect multiple instances
            if (Instance != null)
                throw new Exception("Attempt to create a second instance of a singleton.");
            Instance = this;

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

            // Hook up Update Logic
            updateable.Add(ResourceExplorer.Instance);
            updateable.Add(StartPage.Instance);
            updateable.Add(PreviewWindow.Instance);

        }

        #endregion

        #region UI Utilities

        void UpdateUiState()
        {
            foreach (var i in updateable)
                i.UpdateState();
        }

        void NewProject()
        {
            var dialog = new NewProjectWizard();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    YuaiProject.Create(YuaiProject.ProjectFolder, dialog.ProjectName);
                    UpdateUiState();
                    StartPage.Instance.Hide();
                }
                catch (Exception x)
                {
                    (new ErrorWindow(x)).ShowDialog(this);
                }
            }
        }

        void CloseProject()
        {
            
        }

        void LoadProject()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "BarloX Project File (*.bxproj)|*.bxproj";
            dialog.InitialDirectory = Settings.Default.ProjectFolder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    YuaiProject.Load(dialog.FileName);
                    UpdateUiState();
                }
                catch (Exception x)
                {
                    (new ErrorWindow(x)).ShowDialog(this);
                }}
            }

        #endregion

        #region Cross-Window Interop

        public void StartPageNavigate(String projectId)
        {
            switch (projectId)
            {
                case "new":
                    NewProject();
                    break;
                case "open":
                    LoadProject();
                    break;
                default:
                    MessageBox.Show(String.Format("Project navigation not implemented yet. ID: {0}", projectId));
                    break;
            }
        }

        #endregion

    }
}
