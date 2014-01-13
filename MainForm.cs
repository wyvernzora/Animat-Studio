using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;

namespace Animat.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Initialize Layout
            InitializeLayout();

            // Attach Event Handlers
            AttachMenuStripEventHandlers();
        }

        #region Dock and Layout Management

        public void InitializeLayout()
        {
            using (var lk = new ActionLock(
                () => dockPanel1.SuspendLayout(true),
                () => dockPanel1.ResumeLayout(true, true)))
            {
                var explorer = ResourceExplorer.Instance;
                explorer.Show(dockPanel1, DockState.DockLeft);
                StartPage.Instance.Show(dockPanel1, DockState.Document);
            }
        }

        #endregion

        #region Tool Strip/Menu Strip eventr handlers

        private void AttachMenuStripEventHandlers()
        {
            // File

            // Edit

            // View
            tsmShowStartPage.Click += (@s, e) => StartPage.Instance.Show(dockPanel1);


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
