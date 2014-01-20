namespace Animat.UI.ToolWindows
{
    partial class ResourceExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceExplorer));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Resources ");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("default");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Frames ", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("default");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Sequences ", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("default");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Events ", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.browseState = new libWyvernzora.Nightingale.WizardState();
            this.treeView = new System.Windows.Forms.TreeView();
            this.noProjectState = new libWyvernzora.Nightingale.WizardState();
            this.noProjectLabel = new System.Windows.Forms.Label();
            this.browseState.SuspendLayout();
            this.noProjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "frames");
            this.iconList.Images.SetKeyName(1, "resources");
            this.iconList.Images.SetKeyName(2, "sequences");
            this.iconList.Images.SetKeyName(3, "events");
            this.iconList.Images.SetKeyName(4, "file");
            // 
            // browseState
            // 
            this.browseState.Controls.Add(this.treeView);
            this.browseState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseState.Location = new System.Drawing.Point(0, 0);
            this.browseState.Name = "browseState";
            this.browseState.Size = new System.Drawing.Size(229, 646);
            this.browseState.StateManager = null;
            this.browseState.TabIndex = 1;
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.FullRowSelect = true;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.iconList;
            this.treeView.Indent = 20;
            this.treeView.ItemHeight = 20;
            this.treeView.LabelEdit = true;
            this.treeView.LineColor = System.Drawing.Color.DimGray;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.ImageKey = "resources";
            treeNode1.Name = "Resources";
            treeNode1.SelectedImageKey = "resources";
            treeNode1.Text = "Resources ";
            treeNode1.ToolTipText = "Resources used by the project.";
            treeNode2.ImageKey = "file";
            treeNode2.Name = "default";
            treeNode2.SelectedImageKey = "file";
            treeNode2.Text = "default";
            treeNode3.Name = "Frames";
            treeNode3.Text = "Frames ";
            treeNode3.ToolTipText = "Animation Frame definitions.";
            treeNode4.ImageKey = "file";
            treeNode4.Name = "default";
            treeNode4.SelectedImageKey = "file";
            treeNode4.Text = "default";
            treeNode5.ImageKey = "sequences";
            treeNode5.Name = "Sequences";
            treeNode5.SelectedImageKey = "sequences";
            treeNode5.Text = "Sequences ";
            treeNode5.ToolTipText = "Frame Sequences and Transitions.";
            treeNode6.ImageKey = "file";
            treeNode6.Name = "default";
            treeNode6.SelectedImageKey = "file";
            treeNode6.Text = "default";
            treeNode7.ImageKey = "events";
            treeNode7.Name = "Events";
            treeNode7.SelectedImageKey = "events";
            treeNode7.Text = "Events ";
            treeNode7.ToolTipText = "Events that the animation may respond to.";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode5,
            treeNode7});
            this.treeView.PathSeparator = "/";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(229, 646);
            this.treeView.TabIndex = 0;
            // 
            // noProjectState
            // 
            this.noProjectState.Controls.Add(this.noProjectLabel);
            this.noProjectState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noProjectState.Location = new System.Drawing.Point(0, 0);
            this.noProjectState.Name = "noProjectState";
            this.noProjectState.Size = new System.Drawing.Size(229, 646);
            this.noProjectState.StateManager = null;
            this.noProjectState.TabIndex = 2;
            // 
            // noProjectLabel
            // 
            this.noProjectLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.noProjectLabel.AutoSize = true;
            this.noProjectLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noProjectLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.noProjectLabel.Location = new System.Drawing.Point(66, 303);
            this.noProjectLabel.Name = "noProjectLabel";
            this.noProjectLabel.Size = new System.Drawing.Size(97, 40);
            this.noProjectLabel.TabIndex = 0;
            this.noProjectLabel.Text = "No Project\r\nLoaded";
            this.noProjectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResourceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 646);
            this.Controls.Add(this.browseState);
            this.Controls.Add(this.noProjectState);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResourceExplorer";
            this.ShowHint = DigitalRune.Windows.Docking.DockState.DockLeft;
            this.TabText = "Project Explorer";
            this.Text = "Project Explorer";
            this.browseState.ResumeLayout(false);
            this.noProjectState.ResumeLayout(false);
            this.noProjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ImageList iconList;
        private libWyvernzora.Nightingale.WizardState browseState;
        private libWyvernzora.Nightingale.WizardState noProjectState;
        private System.Windows.Forms.Label noProjectLabel;
    }
}