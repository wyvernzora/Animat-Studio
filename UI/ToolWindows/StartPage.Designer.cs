namespace Animat.UI.ToolWindows
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.startPageBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // startPageBrowser
            // 
            this.startPageBrowser.AllowWebBrowserDrop = false;
            this.startPageBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPageBrowser.IsWebBrowserContextMenuEnabled = false;
            this.startPageBrowser.Location = new System.Drawing.Point(0, 0);
            this.startPageBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.startPageBrowser.Name = "startPageBrowser";
            this.startPageBrowser.ScriptErrorsSuppressed = true;
            this.startPageBrowser.Size = new System.Drawing.Size(975, 641);
            this.startPageBrowser.TabIndex = 0;
            this.startPageBrowser.TabStop = false;
            this.startPageBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 641);
            this.Controls.Add(this.startPageBrowser);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartPage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TabText = "Start Page";
            this.Text = "Start Page";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser startPageBrowser;
    }
}