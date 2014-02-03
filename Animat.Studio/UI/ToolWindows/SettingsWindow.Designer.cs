namespace Animat.Studio.UI.ToolWindows
{
    partial class SettingsWindow
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
            this.tabList1 = new Cyotek.Windows.Forms.TabList();
            this.tabListPage1 = new Cyotek.Windows.Forms.TabListPage();
            this.tabListPage2 = new Cyotek.Windows.Forms.TabListPage();
            this.tabList1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabList1
            // 
            this.tabList1.Controls.Add(this.tabListPage1);
            this.tabList1.Controls.Add(this.tabListPage2);
            this.tabList1.Location = new System.Drawing.Point(12, 12);
            this.tabList1.Name = "tabList1";
            this.tabList1.Size = new System.Drawing.Size(781, 574);
            this.tabList1.TabIndex = 0;
            // 
            // tabListPage1
            // 
            this.tabListPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabListPage1.Name = "tabListPage1";
            this.tabListPage1.Size = new System.Drawing.Size(623, 566);
            this.tabListPage1.TabIndex = 0;
            this.tabListPage1.Text = "General";
            // 
            // tabListPage2
            // 
            this.tabListPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabListPage2.Name = "tabListPage2";
            this.tabListPage2.Size = new System.Drawing.Size(623, 566);
            this.tabListPage2.TabIndex = 1;
            this.tabListPage2.Text = "Components";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 598);
            this.Controls.Add(this.tabList1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.Name = "SettingsWindow";
            this.TabText = "SettingsWindow";
            this.Text = "SettingsWindow";
            this.tabList1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Cyotek.Windows.Forms.TabList tabList1;
        private Cyotek.Windows.Forms.TabListPage tabListPage1;
        private Cyotek.Windows.Forms.TabListPage tabListPage2;
    }
}