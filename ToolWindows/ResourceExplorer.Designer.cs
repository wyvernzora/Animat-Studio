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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Resources");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Frames");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Sequences");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Events");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceExplorer));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.iconList;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageKey = "resources";
            treeNode1.Name = "Resources";
            treeNode1.SelectedImageKey = "resources";
            treeNode1.Text = "Resources";
            treeNode1.ToolTipText = "Resources used by the project.";
            treeNode2.Name = "Frames";
            treeNode2.Text = "Frames";
            treeNode2.ToolTipText = "Animation Frame definitions.";
            treeNode3.ImageKey = "sequences";
            treeNode3.Name = "Sequences";
            treeNode3.SelectedImageKey = "sequences";
            treeNode3.Text = "Sequences";
            treeNode3.ToolTipText = "Frame Sequences and Transitions.";
            treeNode4.ImageKey = "events";
            treeNode4.Name = "Events";
            treeNode4.SelectedImageKey = "events";
            treeNode4.Text = "Events";
            treeNode4.ToolTipText = "Events that the animation may respond to.";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(279, 709);
            this.treeView1.TabIndex = 0;
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "frames");
            this.iconList.Images.SetKeyName(1, "resources");
            this.iconList.Images.SetKeyName(2, "sequences");
            this.iconList.Images.SetKeyName(3, "events");
            // 
            // ResourceExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 709);
            this.Controls.Add(this.treeView1);
            this.DockAreas = ((DigitalRune.Windows.Docking.DockAreas)((((((DigitalRune.Windows.Docking.DockAreas.Float | DigitalRune.Windows.Docking.DockAreas.Left) 
            | DigitalRune.Windows.Docking.DockAreas.Right) 
            | DigitalRune.Windows.Docking.DockAreas.Top) 
            | DigitalRune.Windows.Docking.DockAreas.Bottom) 
            | DigitalRune.Windows.Docking.DockAreas.Document)));
            this.HideOnClose = true;
            this.Name = "ResourceExplorer";
            this.TabText = "Project Explorer";
            this.Text = "Project Explorer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList iconList;
    }
}