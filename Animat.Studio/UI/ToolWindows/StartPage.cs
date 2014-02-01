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
            // Prevent duplicate instances
            if (instance != null)
                throw new DuplicateInstanceException(typeof(StartPage));

            InitializeComponent();

            // Set StartPage only dockable to document area
            DockAreas = DockAreas.Document;

            // Load Start Page
            //String html = File.ReadAllText("Assets\\StartPage\\HTML\\index1.html");
            //startPageBrowser.DocumentText = html;
            startPageBrowser.Navigate(new Uri(String.Format("file://{0}/Assets/StartPage/index.html", Application.StartupPath.Replace('\\', '/')), UriKind.Absolute));

            startPageBrowser.Navigating += (@s, e) =>
                {
                    if (e.Url.Scheme.ToLower() == "animat")
                    {
                        String command = e.Url.Host;
                        String args = e.Url.LocalPath.Trim('/');
                        StudioCore.Instance.StartPageCommand(command, args);
                    }
                    else
                    {
                        
                    }

                    e.Cancel = true;
                };

            // Attach update request events
            StudioCore.Instance.OnUpdateRequest += (@s, e) =>
                { if (e.Scope.HasFlag(UpdateScope.StartPage)) UpdateUi(); };
        }

        public void UpdateUi()
        {
            
        }
    }
}
