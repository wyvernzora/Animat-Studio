using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;
using libWyvernzora.Nightingale;
using Animat.UI.Project;

namespace Animat.UI.ToolWindows
{
    public partial class ResourceExplorer : DockableForm, IUpdateState
    {
        // Window State Manager
        readonly WizardStateManager stateManager
            = new WizardStateManager();

        #region Singleton Window

        private static ResourceExplorer instance = null;
        
        /// <summary>
        /// Gets the global instance of Resource Explorer
        /// </summary>
        public static ResourceExplorer Instance
        {
            get { return instance ?? (instance = new ResourceExplorer()); }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResourceExplorer()
        {
            InitializeComponent();

            // Initializa State Manager
            stateManager.AddState(noProjectState);
            stateManager.AddState(browseState);
            stateManager.SetCurrentState(browseState.Name);

            // Disable docking to document area and top
            DockAreas = ~(DockAreas.Document | DockAreas.Top);

            // Events
            Shown += (@s, e) => stateManager.SetCurrentState(YuaiProject.Instance == null ? 
                                                                 noProjectState.Name : browseState.Name);
        }

        public void UpdateState()
        {
            stateManager.SetCurrentState(YuaiProject.Instance == null ?
                                                                    noProjectState.Name : browseState.Name);
        }
    }
}
