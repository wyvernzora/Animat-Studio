namespace Animat.Studio.UI.ToolWindows
{
    partial class TimelineWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimelineWindow));
            this.timelineControl1 = new Animat.Studio.UI.Controls.TimelineControl();
            this.SuspendLayout();
            // 
            // timelineControl1
            // 
            this.timelineControl1.BackColor = System.Drawing.Color.White;
            this.timelineControl1.CaretPosition = 27;
            this.timelineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timelineControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timelineControl1.ForeColor = System.Drawing.Color.Black;
            this.timelineControl1.LayerHeight = 30;
            this.timelineControl1.Location = new System.Drawing.Point(0, 0);
            this.timelineControl1.Name = "timelineControl1";
            this.timelineControl1.ScaleStart = 0;
            this.timelineControl1.SeparatorLocation = 120;
            this.timelineControl1.Size = new System.Drawing.Size(1191, 262);
            this.timelineControl1.TabIndex = 0;
            this.timelineControl1.Text = "timelineControl1";
            this.timelineControl1.TickAccentFrequency = 5;
            this.timelineControl1.TickFrequency = 10;
            // 
            // TimelineWindow
            // 
            this.AutoHidePortion = 220D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1191, 262);
            this.Controls.Add(this.timelineControl1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimelineWindow";
            this.ShowInTaskbar = false;
            this.TabText = "Timeline";
            this.Text = "Timeline";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TimelineControl timelineControl1;


    }
}