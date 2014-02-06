using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Animat.Studio.Properties;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;
using Mustache;

namespace Animat.Studio.ToolWindows
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
        
        private readonly StartPageInterface startPageInterface = new StartPageInterface();

        public StartPage()
        {
            // Prevent duplicate instances
            if (instance != null)
                throw new DuplicateInstanceException(typeof(StartPage));

            InitializeComponent();

            // Set StartPage only dockable to document area
            DockAreas = DockAreas.Document;

            // Set up browser for interfacing
            startPageBrowser.ObjectForScripting = startPageInterface;

            // Load Resource Data
            var compiler = new FormatCompiler();
            template = compiler.Compile(Resources.StartPageTemplate);
            LayoutCSS = Resources.StartPageLayout;
            themeDark = Resources.StartPageDark;
            themeLight = Resources.StartPageLight;

            ColorThemeCSS = themeDark;


            // Load Start Page
            LoadPage();

            AttachEventHandlers(); // This one messes with the browser, so do it last.
        }

        public void LoadPage()
        {
            var result = template.Render(this);

            File.WriteAllText("start-test.html", result);
                startPageBrowser.DocumentText = result;
        }

        private void AttachEventHandlers()
        {
            Closed += (@s, e) =>
            {
                instance = null;
            };
            startPageBrowser.Navigating += (@s, e) =>
            {
                if (e.Url.Scheme.ToLower() == "http" || e.Url.Scheme.ToLower() == "https")
                    Process.Start(e.Url.ToString());
                e.Cancel = true;
            };
        }

        #region Start Page Data Support

        private Generator template;
        private String themeLight;
        private String themeDark;

        public String LayoutCSS
        { get; private set; }
        
        public String ColorThemeCSS
        { get; private set; }
        
        public List<StudioSettings.RecentProjectInfo> RecentProjects
        { get { return StudioSettings.Instance.RecentProjects; } }

        #endregion

        #region Command Processing

        /// <summary>
        /// Processes a <c>animat://</c> navigation url as a command.
        /// </summary>
        /// <param name="nav"></param>
        private void ProcessCommand(Uri nav)
        {
            var scheme = nav.Scheme.ToLower();
            if (scheme == "http" || scheme == "https")
                Process.Start(nav.ToString());  // HTTP navigation is handled by default browser
            else if (scheme == "animat") {
                var commandScope = nav.Host.Split('.')[0].ToLower();
                var command = nav.Host.Split('.')[1].ToLower();
                var target = nav.LocalPath.Trim('/');

                if (commandScope == "project")
                    StudioCore.Instance.ProcessProjectScopedCommand(command, target);

            }
        }

        #endregion


    }

    #region Script Interfacing

    /// <summary>
    /// Interface class between studio code and start page HTML.
    /// </summary>
    [ComVisible(true)]
    public class StartPageInterface
    {

        public void CreateProject()
        {
            MainForm.Instance.CreateProject();
        }

        public void OpenProject(String target)
        {
            if (target.Length == 0)
                MainForm.Instance.LoadProject();
            else
                MainForm.Instance.LoadProject(StudioSettings.Instance.FindProjectById(target).Path);
        }
        
        public void TogglePin(String target, Boolean pin)
        {
            var p1 = StudioSettings.Instance.FindProjectById(target);
            p1.IsPinned = pin;
            StudioSettings.Instance.Save();
        }

    }

    #endregion
}
