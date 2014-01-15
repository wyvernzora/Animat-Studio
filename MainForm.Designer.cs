namespace Animat.UI
{
    partial class MainForm
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
            this.mainMenuSrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowStartPage = new System.Windows.Forms.ToolStripMenuItem();
            this.dEBUGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel = new DigitalRune.Windows.Docking.DockPanel();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowProjectExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.importResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuSrip.SuspendLayout();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuSrip
            // 
            this.mainMenuSrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.dEBUGToolStripMenuItem});
            this.mainMenuSrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuSrip.Name = "mainMenuSrip";
            this.mainMenuSrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenuSrip.Size = new System.Drawing.Size(1264, 24);
            this.mainMenuSrip.TabIndex = 0;
            this.mainMenuSrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNewProject,
            this.tsmOpenProject,
            this.toolStripMenuItem2,
            this.importResourceToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsmNewProject
            // 
            this.tsmNewProject.Name = "tsmNewProject";
            this.tsmNewProject.Size = new System.Drawing.Size(161, 22);
            this.tsmNewProject.Text = "New Project...";
            // 
            // tsmOpenProject
            // 
            this.tsmOpenProject.Name = "tsmOpenProject";
            this.tsmOpenProject.Size = new System.Drawing.Size(161, 22);
            this.tsmOpenProject.Text = "Open Project...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowStartPage,
            this.toolStripMenuItem1,
            this.tsmShowProjectExplorer});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tsmShowStartPage
            // 
            this.tsmShowStartPage.Name = "tsmShowStartPage";
            this.tsmShowStartPage.Size = new System.Drawing.Size(188, 22);
            this.tsmShowStartPage.Text = "Show Start Page";
            // 
            // dEBUGToolStripMenuItem
            // 
            this.dEBUGToolStripMenuItem.Name = "dEBUGToolStripMenuItem";
            this.dEBUGToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.dEBUGToolStripMenuItem.Text = "DEBUG";
            // 
            // mainToolStripContainer
            // 
            this.mainToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // mainToolStripContainer.ContentPanel
            // 
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.dockPanel);
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1264, 657);
            this.mainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainToolStripContainer.LeftToolStripPanelVisible = false;
            this.mainToolStripContainer.Location = new System.Drawing.Point(0, 24);
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.RightToolStripPanelVisible = false;
            this.mainToolStripContainer.Size = new System.Drawing.Size(1264, 657);
            this.mainToolStripContainer.TabIndex = 1;
            this.mainToolStripContainer.Text = "toolStripContainer1";
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.DefaultFloatingWindowSize = new System.Drawing.Size(300, 300);
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockLeftPortion = 0.2D;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1264, 657);
            this.dockPanel.TabIndex = 0;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 6);
            // 
            // tsmShowProjectExplorer
            // 
            this.tsmShowProjectExplorer.Name = "tsmShowProjectExplorer";
            this.tsmShowProjectExplorer.Size = new System.Drawing.Size(188, 22);
            this.tsmShowProjectExplorer.Text = "Show Project Explorer";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 6);
            // 
            // importResourceToolStripMenuItem
            // 
            this.importResourceToolStripMenuItem.Name = "importResourceToolStripMenuItem";
            this.importResourceToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.importResourceToolStripMenuItem.Text = "Import Resource";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.mainToolStripContainer);
            this.Controls.Add(this.mainMenuSrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.mainMenuSrip;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AniMat Studio";
            this.mainMenuSrip.ResumeLayout(false);
            this.mainMenuSrip.PerformLayout();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuSrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private DigitalRune.Windows.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripMenuItem dEBUGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmShowStartPage;
        private System.Windows.Forms.ToolStripMenuItem tsmNewProject;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowProjectExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem importResourceToolStripMenuItem;
    }
}

