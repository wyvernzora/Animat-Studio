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
    /// <summary>
    /// Start Page tool window.
    /// Displays an embedded HTML page with options on how to start using the YUAI.
    /// </summary>
    public partial class StartPage : DockableForm
    {
        #region Singleton

        private static StartPage instance;

        /// <summary>
        /// Gets the global instance of StartPage.
        /// </summary>
        public static StartPage Instance
        { get { return instance ?? (instance = new StartPage()); } }

        #endregion

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
                    MainForm.Instance.StartPageNavigate(projectId);

                    e.Cancel = true;
                };
        }
    }
}
