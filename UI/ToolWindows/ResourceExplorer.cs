using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.UI.UI.ToolWindows;
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
                    var imgKey = node.Error != null ? "error" : "file";
                    var treeNode = new TreeNode(node.Name)
                    {
                        ImageKey = imgKey,
                        SelectedImageKey = imgKey,
                        Tag = node
                    };

                    if (node.FrameCount > 1)
                    {
                        for (int i = 0; i < node.FrameCount; i++)
                        {
                            treeNode.Nodes.Add(new TreeNode(String.Format("Frame#{0}", i.ToString().PadLeft(5, '0')))
                            {
                                ImageKey = imgKey,
                                SelectedImageKey = imgKey,
                                Tag = i
                            });
                        }
                    }

                    resNode.Nodes.Add(treeNode);
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
                    if (e.Node.Tag is int)
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
                    var asset = e.Node.Tag as StudioAsset;

                    StudioCore.Instance.RequestUpdate(UpdateScope.Preview, PreviewWindow.UPDATE_TARGET, 
                        new [] { e.Node.Tag, 0 });

                    if (AssetViewer.GetInstance(asset) != null)
                    {
                        //StudioCore.Instance.RequestUpdate(UpdateScope.AssetViewer, asset.Name, -1);
                    }
                }
                if (e.Node.Tag is int && e.Node.Parent.Tag is StudioAsset)
                {
                    var asset = e.Node.Parent.Tag as StudioAsset;
                    var index = (Int32) e.Node.Tag;
                    
                    StudioCore.Instance.RequestUpdate(UpdateScope.Preview, PreviewWindow.UPDATE_TARGET, new[] { e.Node.Parent.Tag, index });

                    if (AssetViewer.GetInstance(asset) != null)
                    {
                        //StudioCore.Instance.RequestUpdate(UpdateScope.AssetViewer, asset.Name, index);
                    }
                }
            };

            treeView.DoubleClick += (@s, e) =>
            {
                if (treeView.SelectedNode == null)
                    return;

                var asset = treeView.SelectedNode.Tag as StudioAsset;
                if (asset != null && asset.Error == null) StudioCore.Instance.ViewAsset(asset);
            };

        }

        #endregion
    }
}

