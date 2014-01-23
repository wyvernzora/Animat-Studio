using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalRune.Windows.Docking;

namespace Animat.UI.ToolWindows
{
    public partial class PreviewWindow : DockableForm
    {
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
                    UpdateState();
            };
        }

        private void AttachEventHandlers()
        {
            pictureBox1.Resize += (@s, e) => UpdateSizeMode();
        }

        public void UpdateState()
        {
            pictureBox1.Image = StudioCore.Instance.PreviewAsset;
            UpdateSizeMode();
        }

        private void UpdateSizeMode()
        {
            if (pictureBox1.Image == null) return;

            if (pictureBox1.Width > pictureBox1.Image.Width && pictureBox1.Height > pictureBox1.Image.Height)
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
