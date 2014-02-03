using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.Project;
using DigitalRune.Windows.Docking;

namespace Animat.Studio.ToolWindows
{
    public partial class PreviewWindow : DockableForm
    {
        public const String UPDATE_TARGET = "PREVIEW";

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
            if (instance != null)
                throw new DuplicateInstanceException(typeof(PreviewWindow));

            // Restrict Docking
            DockAreas = ~(DockAreas.Document | DockAreas.Top | DockAreas.Bottom);

            // Initialize and such
            InitializeComponent();
            AttachEventHandlers();

            // Hook up update logic
            StudioCore.Instance.OnUpdateRequest += (@s, e) =>
            {
                if (e.Scope.HasFlag(UpdateScope.Preview))
                {
                    if (e.UpdateMessage is Object[])
                    {
                        var asset = ((Object[]) e.UpdateMessage)[0] as AssetBase;
                        var index = (Int32) ((Object[])e.UpdateMessage)[1];

                        if (asset == null || index < 0 || index >= asset.FrameCount)
                            imageBox.Image = null;
                        else
                            imageBox.Image = asset.GetFrameThumbnail(index);
                    }
                    else
                    {
                        imageBox.Image = null;
                    }
                    
                }
            };
        }

        private void AttachEventHandlers()
        {
        }

    }
}
