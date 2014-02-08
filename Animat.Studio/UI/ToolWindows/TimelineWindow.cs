using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace Animat.Studio.UI.ToolWindows
{
    public partial class TimelineWindow : DockableForm
    {
        public TimelineWindow()
        {
            InitializeComponent();


            // Restrict docking
            DockAreas = DockAreas.Top | DockAreas.Bottom;

        }

        #region Properties



        #endregion

        #region Graphics related stuff

        /// <summary>
        /// Forces the window to use double buffering.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        
        #endregion
    }
}
