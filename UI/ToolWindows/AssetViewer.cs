using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using DigitalRune.Windows.Docking;

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
            imageBox.ZoomToFit();

            Text = asset.Name;
            TabText = asset.Name;
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
            Closed += (@s, e) => instances.Remove(Asset.Name);

            // Zooming
            imageBox.ZoomChanged += (@s, e) => { tslZoomFactor.Text = String.Format("{0}%", imageBox.Zoom); };
        }

        #endregion
    }
}
