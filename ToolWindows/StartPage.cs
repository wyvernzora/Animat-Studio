using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace Animat.UI.ToolWindows
{
    public partial class StartPage : DockableForm
    {
        public StartPage()
        {
            InitializeComponent();

            // Set StartPage only dockable to document area
            DockAreas = DockAreas.Document;

            // Load Start Page
            String html = File.ReadAllText("Assets\\StartPage\\index.html");
            startPageBrowser.DocumentText = html;

            startPageBrowser.Navigating += (@s, e) =>
                {
                    String projectId = e.Url.LocalPath.Trim('/');
                    MessageBox.Show(String.Format("Navigation to Project with id: {0}", projectId));

                    e.Cancel = true;
                };
        }
    }
}
