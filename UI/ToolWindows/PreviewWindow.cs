using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace Animat.UI.ToolWindows
{
    public partial class PreviewWindow : DockableForm
    {
        #region Singleton Window

        private static PreviewWindow instance = null;

        /// <summary>
        /// Gets the global instance of Resource Explorer
        /// </summary>
        public static PreviewWindow Instance
        {
            get { return instance ?? (instance = new PreviewWindow()); }
        }

        #endregion

        public PreviewWindow()
        {
            // Prevent creation of redundant instances


            InitializeComponent();
        }

        public void UpdateState()
        {

        }
    }
}
