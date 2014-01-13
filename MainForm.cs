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

namespace Animat.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var startpage = new StartPage();
            startpage.Show(dockPanel1);
        }

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
