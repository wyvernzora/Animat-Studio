using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.Studio.Properties;
using Animat.Studio.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.Utilities;

namespace Animat.Studio
{
    // Partial Class for MainForm for everything related to graphics and painting
    public partial class MainForm : Form
    {
        

        /// <summary>
        /// Attaches graphics event handlers
        /// </summary>
        private void AttachGraphicsEvents()
        {
            /* Since DigitalRune did not provide a better way to paint watermarks,
             * here is a somewhat dirty hack to actually make it center in the document pane.
             * There is a known issue that the pane does not redraw itself if a window is
             * moved to another docking area without undocking.
             * 
             */


            // Watermark
            dockPanel.Paint += (@s, e) =>
                {
                    // Disable painting if there is not enough space
                    if (Width < 256 || Height < 320) return;

                    // Calculate coordinates for painting
                    var leftOffset = (from p in dockPanel.Panes where p.DockState == DockState.DockLeft select p.Width).FirstOrDefault();
                    var rightOffset = (from p in dockPanel.Panes where p.DockState == DockState.DockRight select p.Width).FirstOrDefault();
                    var topOffset = (from p in dockPanel.Panes where p.DockState == DockState.DockTop select p.Height).FirstOrDefault();
                    var bottOffset = (from p in dockPanel.Panes where p.DockState == DockState.DockBottom select p.Height).FirstOrDefault();

                    // Calculate offsets
                    var effectiveWidth = dockPanel.Width - rightOffset - leftOffset;
                    if (effectiveWidth < 256) return;
                    var x = (effectiveWidth) / 2 + leftOffset - 128;

                    var effectiveHeight = dockPanel.Height - topOffset - bottOffset;
                    if (effectiveHeight < 320) return;
                    var y = (effectiveHeight) / 2 + topOffset - 160;

                    // Paint the watermark
                    e.Graphics.DrawImage(Resources.watermark, x, y);
                };
            dockPanel.ContentRemoved += (@s, e) =>
                                        dockPanel.Invalidate();

            foreach (var zone in dockPanel.DockZones)
                zone.Resize += (@s, e) => dockPanel.Invalidate();

        }

    }
}