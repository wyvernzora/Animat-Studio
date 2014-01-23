﻿using System;
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
            stateManager.SetCurrentState(StudioCore.Instance.HasProject ? browseState.Name : noProjectState.Name);

            // Stop further loading if there is no project loaded
            if (!StudioCore.Instance.HasProject) return;

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
                foreach (var node in StudioCore.Instance.Project.Assets.OrderBy((a) => { return a.Filename; }))
                {
                    var imgKey = node.Error ? "error" : "file";
                    resNode.Nodes.Add(new TreeNode(node.Name)
                    {
                        ImageKey = imgKey,
                        SelectedImageKey = imgKey,
                        Tag = node
                    });
                }
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
            treeView.AfterLabelEdit += (@s, e) =>
            {
                if (e.Node.Tag is StudioAsset)
                {
                    if (e.Label == null) return;
                    if (String.IsNullOrWhiteSpace(e.Label))
                        e.Node.EndEdit(false);
                     
                    StudioCore.Instance.Project.RenameAsset(e.Node.Text, e.Label);
                }
            };
            
            treeView.AfterSelect += (@s, e) =>
            {
                if (e.Node.Tag is StudioAsset)
                {
                    StudioCore.Instance.PreviewAsset = ((StudioAsset) e.Node.Tag).Thumbnail;
                    StudioCore.Instance.RequestUpdate(UpdateScope.Preview);
                }
            };

            treeView.DoubleClick += (@s, e) =>
            {
                if (treeView.SelectedNode == null)
                    return;

                var asset = treeView.SelectedNode.Tag as StudioAsset;
                if (asset != null && !asset.Error) StudioCore.Instance.ViewAsset(asset);
            };

        }

        #endregion
    }
}

