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
        List<IUpdateState> updateable = new List<IUpdateState>(); 


        public MainForm()
        {
            Instance = this;

            InitializeComponent();

            // Initialize Layout
            InitializeLayout();

            // Attach Event Handlers
            AttachMenuStripEventHandlers();
        }

        #region Dock and Layout Management

        public void InitializeLayout()
        {
            // Layout
            using (var lk = new ActionLock(
                () => dockPanel1.SuspendLayout(true),
                () => dockPanel1.ResumeLayout(true, true)))
            {
                ResourceExplorer.Instance.Show(dockPanel1, DockState.DockLeft);
                PreviewWindow.Instance.Show(ResourceExplorer.Instance.Pane, DockPaneAlignment.Bottom, 0.4);
                StartPage.Instance.Show(dockPanel1, DockState.Document);

            }

            // Hook up Update Logic
            updateable.Add(ResourceExplorer.Instance);

        }

        #endregion

        #region Tool Strip/Menu Strip eventr handlers

        private void AttachMenuStripEventHandlers()
        {
            // File
            tsmNewProject.Click += (@s, e) => NewProject();

            // Edit


            // View
            tsmShowStartPage.Click += (@s, e) => StartPage.Instance.Show(dockPanel1);


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
                    
                }
            }
        }

        #endregion

        #region Cross-Window Interop

        public void StartPageNavigate(String projectId)
        {
            if ((projectId == "new"))
                NewProject();
            else
            {
                MessageBox.Show(String.Format("Project navigation not implemented yet. ID: {0}", projectId));
            }
        }

        #endregion

        private void addDockableFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var window = new ResourceExplorer();


            if (dockPanel1.DocumentStyle == DocumentStyle.SystemMdi)
            {
                window.MdiParent = this;
                window.Show();
            }
            else
            {
                window.Show(dockPanel1);
                window.DockState = DockState.DockLeft;
                window.DockAreas = ~(DockAreas.Document | DockAreas.Top | DockAreas.Bottom);
            }
        }
    }
}
