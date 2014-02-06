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
using Animat.Project;
using Cyotek.Windows.Forms;
using DigitalRune.Windows.Docking;
using libWyvernzora.Nightingale;
using libWyvernzora.Utilities;
using NLog;

namespace Animat.Studio.UI.ToolWindows
{
    /// <summary>
    /// Multiton.
    /// Asset Viewer & Frame Editor
    /// </summary>
    public partial class AssetViewer : DockableForm
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Multiton

        private static Dictionary<Guid, AssetViewer> instances
            = new Dictionary<Guid, AssetViewer>();
        
        /// <summary>
        /// Gets or creates an Asset Viewer instance for an asset.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public static AssetViewer GetInstance(AssetBase asset)
        {
            logger.Debug("Requesting asset viewer for the asset {{{0}}}", asset.ID);

            if (!instances.ContainsKey(asset.ID)) {
                logger.Trace("Found an asset viewer instance for the asset {{{0}}}", asset.ID);
                instances[asset.ID] = new AssetViewer(asset);
            }

            return instances[asset.ID];
        }

        #endregion
        
        protected AssetViewer(AssetBase asset)
        {
            logger.Trace("Creating an instance of AssetViewer for the asset {{{0}}}", asset.ID);

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
        public AssetBase Asset
        { get; private set; }

        #endregion

        #region Event Handling

        private void AttachEventHandlers()
        {
            // Dispose assets once closing
            Closing += (@e, s) =>
            {
                logger.Trace("AssetViewer {{{0}}} is closing, disposing image resources.", Asset.ID);
                imageBox.Image.Dispose();
                imageBox.Image = null;
            };

            // Remove the instance once it's closed
            Closed += (@s, e) =>
            {
                logger.Trace("AssetViewer {{{0}}} is closed, removing the instance from the multiton.", Asset.ID);
                instances.Remove(Asset.ID);
                StudioCore.Instance.OnUpdateRequest -= UpdateState;
            };

            // Adjust zoom level when shown
            Shown += (@s, e) => imageBox.ZoomToFit();
        
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
                    imageBox.Image = Asset.GetFrameImage(index);
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
