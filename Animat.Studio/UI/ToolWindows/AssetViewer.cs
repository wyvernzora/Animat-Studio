using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using DigitalRune.Windows.Docking;
using libWyvernzora.Nightingale;
using libWyvernzora.Utilities;

namespace Animat.UI.UI.ToolWindows
{
    /// <summary>
    /// Multiton.
    /// Asset Viewer & Frame Editor
    /// </summary>
    public partial class AssetViewer : DockableForm
    {
        #region Multiton

        private static Dictionary<String, AssetViewer> instances
            = new Dictionary<string, AssetViewer>(StringComparer.CurrentCultureIgnoreCase);

        

        /// <summary>
        /// Gets or creates an Asset Viewer instance for an asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static AssetViewer GetInstance(StudioAsset asset)
        {
            if (!instances.ContainsKey(asset.Name))
                instances[asset.Name] = new AssetViewer(asset);
                
            return instances[asset.Name];
        }

        #endregion
        
        protected AssetViewer(StudioAsset asset)
        {
            // Initialize Components
            InitializeComponent();
            AttachEventHandlers();

            // Restrict Docking
            DockAreas = DockAreas.Bottom | DockAreas.Top | DockAreas.Document | DockAreas.Float;

            // Set up asset
            Asset = asset;
            var assetImage = Image.FromFile(asset.FullPath);
            imageBox.Image = assetImage;

            Text = asset.Name;
            TabText = asset.Name;

            // Hook up update logic
            StudioCore.Instance.OnUpdateRequest += UpdateState;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the asset displayed in the viewer.
        /// </summary>
        public StudioAsset Asset
        { get; private set; }

        #endregion

        #region Event Handling

        private void AttachEventHandlers()
        {
            // Dispose assets once closing
            Closing += (@e, s) =>
            {
                imageBox.Image.Dispose();
                imageBox.Image = null;
            };

            // Remove the instance once it's closed
            Closed += (@s, e) =>
            {
                instances.Remove(Asset.Name);
                StudioCore.Instance.OnUpdateRequest -= UpdateState;
            };
            
            // Zooming
            imageBox.ZoomChanged += (@s, e) => { tslZoomFactor.Text = String.Format("{0}%", imageBox.Zoom); };
            tsbZoomIn.Click += (@s, e) => imageBox.ZoomIn();
            tsbZoomOut.Click += (@s, e) => imageBox.ZoomOut();
            tsbActualSize.Click += (@s, e) => { imageBox.Zoom = 100; };
            tsbZoomFit.Click += (@s, e) => imageBox.ZoomToFit();

            // Selection
            tsbDeselect.Click += (@s, e) => imageBox.SelectNone();
            tsbSelectAll.Click += (@s, e) => imageBox.SelectAll();

            // Tool
            tsbPan.Click += (@s, e) =>
            {
                tsbPan.Checked = true;
                tsbSelection.Checked = false;
                imageBox.SelectionMode = ImageBoxSelectionMode.None;
            };
            tsbSelection.Click += (@s, e) =>
            {
                tsbSelection.Checked = true;
                tsbPan.Checked = false;
                imageBox.SelectionMode = ImageBoxSelectionMode.Rectangle;
            };

        }

        /// <summary>
        /// Handles update requests from StudioCore.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="args"></param>
        private void UpdateState(Object project, UpdateEventArgs args)
        {
            if (!args.Scope.HasFlag(UpdateScope.AssetViewer)) return;

            if (Asset.Name.Equals(args.Target))
            {
                var index = (int) args.UpdateMessage;
                if (index >= 0 && index < Asset.FrameCount)
                {
                    var oldImg = imageBox.Image;
                    imageBox.Image = Asset.GetFrame(index);
                    oldImg.Dispose();
                }
                else
                {
                    var oldImg = imageBox.Image;
                    imageBox.Image = Image.FromFile(Asset.FullPath);
                    oldImg.Dispose();
                }
            }
        }

        #endregion
        
    }
}
