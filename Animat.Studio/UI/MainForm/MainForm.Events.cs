﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.UI.Properties;
using Animat.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;

namespace Animat.UI
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

            // Edit


            // View
            tsmShowStartPage.Click += (@s, e) => StartPage.Instance.Show(dockPanel);
            tsmShowProjectExplorer.Click += (@s, e) => ResourceExplorer.Instance.Show(dockPanel);
            tsmShowPreview.Click += (@s, e) => PreviewWindow.Instance.Show(dockPanel);

            // Project
            tsmImportResource.Click += (@s, e) =>
                {
                    var dialog = new OpenFileDialog();
                    dialog.Filter = "Image File (*.jpg;*.png)|*.jpg;*.png|Animated Image File (*.gif)|*.gif|Animat Resource (*.amt)|*.amt|BarloX Animation (*.bxa;*.ibxa)|*.bxa;*.ibxa";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var asset = StudioCore.Instance.Project.AddAsset(dialog.FileName);
                        StudioCore.Instance.Project.SaveProject();

                        // Rebuild Cache
                        var dlg = new BackgrounWorkDialog();
                        dlg.Show(this);
                        Enabled = false;
                        asset.RebuildCacheAsync(
                            () =>
                            {
                                dlg.Close();
                                Enabled = true;
                                Show();
                            }
                            );
                    }
                };

            // Help
            tsmAbout.Click += (@s, e) =>
                    AboutWindow.Instance.Show(dockPanel, DockState.Document);

        }
    }
}