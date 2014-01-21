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
using libWyvernzora.Nightingale;
using Animat.UI.Project;
using libWyvernzora.Utilities;

namespace Animat.UI.ToolWindows
{
    public partial class ResourceExplorer : DockableForm
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
            AttachTreeViewEventHandlers();
            Shown += (@s, e) => UpdateUi();
            StudioCore.Instance.OnUpdateRequest += (@s, e) =>
                { if (e.Scope.HasFlag(UpdateScope.Explorer)) UpdateUi(); };
        }

        #region Updating

        private void UpdateUi()
        {
            // Update UI state of the window
            stateManager.SetCurrentState(AnimatProject.Instance == null ?
                                                                    noProjectState.Name : browseState.Name);

            // Stop further loading if there is no project loaded
            if (AnimatProject.Instance == null) return;

            using (new ActionLock(treeView.BeginUpdate, treeView.EndUpdate))
            {
                // Get root nodes
                var resNode = treeView.Nodes["Assets"];
                var frameNode = treeView.Nodes["Frames"];
                var seqNode = treeView.Nodes["Sequences"];
                var evNode = treeView.Nodes["Events"];

                // Delete all stuff
                foreach (TreeNode n in treeView.Nodes)
                    n.Nodes.Clear();

                // Fill up resources
                foreach (var node in AnimatProject.Instance.Model.Resources
                                                  .Select(Path.GetFileName)
                                                  .Select(
                                                      name =>
                                                      new TreeNode(name) {ImageKey = "file", SelectedImageKey = "file"})
                    )
                    resNode.Nodes.Add(node);
            }
        }

        #endregion

        #region Event Handlers

        private void AttachTreeViewEventHandlers()
        {
            treeView.BeforeLabelEdit += (@s, e) =>
                {
                    if (e.Node.Parent == null)
                        e.CancelEdit = true;
                };
        }

        #endregion
    }
}

