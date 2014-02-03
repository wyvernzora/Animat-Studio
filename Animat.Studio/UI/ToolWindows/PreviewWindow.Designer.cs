namespace Animat.Studio.ToolWindows
{
    partial class PreviewWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewWindow));
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.AllowZoom = false;
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.imageBox.GridColor = System.Drawing.Color.WhiteSmoke;
            this.imageBox.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.Medium;
            this.imageBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.ScaleText = true;
            this.imageBox.Size = new System.Drawing.Size(366, 348);
            this.imageBox.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.imageBox.TabIndex = 0;
            // 
            // PreviewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 348);
            this.Controls.Add(this.imageBox);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "PreviewWindow";
            this.ShowInTaskbar = false;
            this.TabText = "Preview";
            this.Text = "Preview";
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox imageBox;


    }
}