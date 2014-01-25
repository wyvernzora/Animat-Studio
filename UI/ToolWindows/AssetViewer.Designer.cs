namespace Animat.UI.UI.ToolWindows
{
    partial class AssetViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.tsbSelection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbActualSize = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomFit = new System.Windows.Forms.ToolStripButton();
            this.tslZoomFactor = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDeselect = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectAll = new System.Windows.Forms.ToolStripButton();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.frameImgList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPan,
            this.tsbSelection,
            this.toolStripSeparator2,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tsbActualSize,
            this.tsbZoomFit,
            this.tslZoomFactor,
            this.toolStripSeparator1,
            this.tsbDeselect,
            this.tsbSelectAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(624, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPan
            // 
            this.tsbPan.Checked = true;
            this.tsbPan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPan.Image = ((System.Drawing.Image)(resources.GetObject("tsbPan.Image")));
            this.tsbPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPan.Name = "tsbPan";
            this.tsbPan.Size = new System.Drawing.Size(23, 22);
            this.tsbPan.Text = "Pan Image";
            // 
            // tsbSelection
            // 
            this.tsbSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelection.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelection.Image")));
            this.tsbSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelection.Name = "tsbSelection";
            this.tsbSelection.Size = new System.Drawing.Size(23, 22);
            this.tsbSelection.Text = "Selection Tool";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomIn.Text = "Zoom In";
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomOut.Text = "Zoom Out";
            // 
            // tsbActualSize
            // 
            this.tsbActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbActualSize.Image = ((System.Drawing.Image)(resources.GetObject("tsbActualSize.Image")));
            this.tsbActualSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActualSize.Name = "tsbActualSize";
            this.tsbActualSize.Size = new System.Drawing.Size(23, 22);
            this.tsbActualSize.Text = "Actual Size";
            // 
            // tsbZoomFit
            // 
            this.tsbZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomFit.Image")));
            this.tsbZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomFit.Name = "tsbZoomFit";
            this.tsbZoomFit.Size = new System.Drawing.Size(23, 22);
            this.tsbZoomFit.Text = "Fit the Window";
            // 
            // tslZoomFactor
            // 
            this.tslZoomFactor.Name = "tslZoomFactor";
            this.tslZoomFactor.Size = new System.Drawing.Size(35, 22);
            this.tslZoomFactor.Text = "100%";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDeselect
            // 
            this.tsbDeselect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeselect.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeselect.Image")));
            this.tsbDeselect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeselect.Name = "tsbDeselect";
            this.tsbDeselect.Size = new System.Drawing.Size(23, 22);
            this.tsbDeselect.Text = "Deselect";
            // 
            // tsbSelectAll
            // 
            this.tsbSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectAll.Image")));
            this.tsbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectAll.Name = "tsbSelectAll";
            this.tsbSelectAll.Size = new System.Drawing.Size(23, 22);
            this.tsbSelectAll.Text = "Select All";
            // 
            // imageBox
            // 
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.GridColor = System.Drawing.Color.WhiteSmoke;
            this.imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.Medium;
            this.imageBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.imageBox.Location = new System.Drawing.Point(0, 25);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(624, 584);
            this.imageBox.TabIndex = 1;
            // 
            // frameImgList
            // 
            this.frameImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.frameImgList.ImageSize = new System.Drawing.Size(64, 64);
            this.frameImgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AssetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 609);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.toolStrip1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssetViewer";
            this.ShowHint = DigitalRune.Windows.Docking.DockState.Document;
            this.TabText = "Asset Viewer";
            this.Text = "Asset Viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Cyotek.Windows.Forms.ImageBox imageBox;
        private System.Windows.Forms.ToolStripLabel tslZoomFactor;
        private System.Windows.Forms.ToolStripButton tsbActualSize;
        private System.Windows.Forms.ToolStripButton tsbZoomFit;
        private System.Windows.Forms.ToolStripButton tsbDeselect;
        private System.Windows.Forms.ToolStripButton tsbSelectAll;
        private System.Windows.Forms.ToolStripButton tsbPan;
        private System.Windows.Forms.ToolStripButton tsbSelection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList frameImgList;
    }
}