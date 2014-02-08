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
using Animat.Studio.ToolWindows;
using Animat.Studio.Properties;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;

namespace Animat.Studio
{
    // Partial Class for MainForm for the Event Handling code
    public partial class MainForm : Form
    {
        /// <summary>
        /// Programmatically attaches event handlers.
        /// </summary>
        /// <remarks>
        /// How to attach event handlers is entirely up to the developer,
        /// doing it programmatically using labda expression is my personal
        /// preference because I believe that the code is neater without so many
        /// subroutines.
        /// </remarks>
        private void AttachEventHandlers()
        {
            Closed += (@s, e) =>
            {
                if(StudioCore.Instance.HasProject)
                    StudioCore.Instance.Project.CacheManager.Dispose();
            };

            AttachMenuStripEventHandlers();
        }

        /// <summary>
        /// Attaches event handlers related to 
        /// </summary>
        private void AttachMenuStripEventHandlers()
        {
            // File
            tsmNewProject.Click += (@s, e) => CreateProject();
            tsmOpenProject.Click += (@s, e) => LoadProject();
            tsmClose.Click += (@s, e) => { };
            tsmCloseProject.Click += (@s, e) => CloseProject(true);

            // Edit


            // View
            tsmShowStartPage.Click += (@s, e) => StartPage.Instance.Show(dockPanel);
            tsmShowProjectExplorer.Click += (@s, e) => ResourceExplorer.Instance.Show(dockPanel);
            tsmShowPreview.Click += (@s, e) => PreviewWindow.Instance.Show(dockPanel);

            // Project
            tsmImportResource.Click += (@s, e) => ImportAsset();

            // Help
            tsmAbout.Click += (@s, e) =>
                AboutWindow.Instance.Show(dockPanel, DockState.Document);

            // Debug
            tsmReloadStartPage.Click += (@s, e) => StartPage.Instance.LoadPage();
            tsmUpdateAll.Click += (@s, e) => StudioCore.Instance.RequestUpdate();

        }
    }
}