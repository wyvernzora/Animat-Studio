using DigitalRune.Windows.Docking;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuSrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowStartPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowProjectExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportResource = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dEBUGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel = new DigitalRune.Windows.Docking.DockPanel();
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
            this.projectToolStripMenuItem,
            this.tOOLSToolStripMenuItem,
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
            this.tsmOpenProject});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "FILE";
            // 
            // tsmNewProject
            // 
            this.tsmNewProject.Name = "tsmNewProject";
            this.tsmNewProject.Size = new System.Drawing.Size(152, 22);
            this.tsmNewProject.Text = "New Project...";
            // 
            // tsmOpenProject
            // 
            this.tsmOpenProject.Name = "tsmOpenProject";
            this.tsmOpenProject.Size = new System.Drawing.Size(152, 22);
            this.tsmOpenProject.Text = "Open Project...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.editToolStripMenuItem.Text = "EDIT";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowStartPage,
            this.toolStripMenuItem1,
            this.tsmShowProjectExplorer});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "VIEW";
            // 
            // tsmShowStartPage
            // 
            this.tsmShowStartPage.Name = "tsmShowStartPage";
            this.tsmShowStartPage.Size = new System.Drawing.Size(188, 22);
            this.tsmShowStartPage.Text = "Show Start Page";
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
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmImportResource});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.projectToolStripMenuItem.Text = "PROJECT";
            // 
            // tsmImportResource
            // 
            this.tsmImportResource.Name = "tsmImportResource";
            this.tsmImportResource.Size = new System.Drawing.Size(161, 22);
            this.tsmImportResource.Text = "Import Resource";
            // 
            // tOOLSToolStripMenuItem
            // 
            this.tOOLSToolStripMenuItem.Name = "tOOLSToolStripMenuItem";
            this.tOOLSToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.tOOLSToolStripMenuItem.Text = "TOOLS";
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
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1264, 632);
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
            this.dockPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dockPanel.DefaultFloatingWindowSize = new System.Drawing.Size(300, 300);
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockLeftPortion = 280D;
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1264, 632);
            this.dockPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.mainToolStripContainer);
            this.Controls.Add(this.mainMenuSrip);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOOLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmImportResource;
    }
}

