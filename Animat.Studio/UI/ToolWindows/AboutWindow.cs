using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace Animat.Studio.ToolWindows
{
    public partial class AboutWindow : DockableForm
    {
        #region Singleton Window

        private static AboutWindow instance = null;

        /// <summary>
        /// Gets the global instance of Resource Explorer
        /// </summary>
        public static AboutWindow Instance
        {
            get { return instance ?? (instance = new AboutWindow()); }
        }

        #endregion

        public AboutWindow()
        {
            InitializeComponent();
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {

        }


    }
}
