namespace Animat.UI
{
    partial class NewProjectWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectWizard));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.projectDetailsState = new libWyvernzora.Nightingale.WizardState();
            this.btnProjNameNext = new System.Windows.Forms.Button();
            this.btnProjNameBack = new System.Windows.Forms.Button();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.projectFolderState = new libWyvernzora.Nightingale.WizardState();
            this.btnProjFolderNext = new System.Windows.Forms.Button();
            this.btnProjFolderCancel = new System.Windows.Forms.Button();
            this.btnProjFolderReset = new System.Windows.Forms.Button();
            this.btnProjFolderBrowse = new System.Windows.Forms.Button();
            this.txtProjFolderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWizardPrompt = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.projectDetailsState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.projectFolderState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // projectDetailsState
            // 
            this.projectDetailsState.Controls.Add(this.btnProjNameNext);
            this.projectDetailsState.Controls.Add(this.btnProjNameBack);
            this.projectDetailsState.Controls.Add(this.txtProjectName);
            this.projectDetailsState.Controls.Add(this.label2);
            this.projectDetailsState.Controls.Add(this.label3);
            this.projectDetailsState.Controls.Add(this.pictureBox2);
            this.projectDetailsState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectDetailsState.Location = new System.Drawing.Point(0, 0);
            this.projectDetailsState.Name = "projectDetailsState";
            this.projectDetailsState.Size = new System.Drawing.Size(610, 316);
            this.projectDetailsState.StateManager = null;
            this.projectDetailsState.TabIndex = 1;
            // 
            // btnProjNameNext
            // 
            this.btnProjNameNext.Location = new System.Drawing.Point(304, 281);
            this.btnProjNameNext.Name = "btnProjNameNext";
            this.btnProjNameNext.Size = new System.Drawing.Size(277, 23);
            this.btnProjNameNext.TabIndex = 1;
            this.btnProjNameNext.Text = "Finish";
            this.btnProjNameNext.UseVisualStyleBackColor = true;
            // 
            // btnProjNameBack
            // 
            this.btnProjNameBack.Location = new System.Drawing.Point(223, 281);
            this.btnProjNameBack.Name = "btnProjNameBack";
            this.btnProjNameBack.Size = new System.Drawing.Size(75, 23);
            this.btnProjNameBack.TabIndex = 6;
            this.btnProjNameBack.Text = "Back";
            this.btnProjNameBack.UseVisualStyleBackColor = true;
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(223, 142);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(358, 21);
            this.txtProjectName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(220, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Name";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 51);
            this.label3.TabIndex = 1;
            this.label3.Text = "Now that we know where the project will be, it is time to name it.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::Animat.UI.Properties.Resources.chibi_f;
            this.pictureBox2.Location = new System.Drawing.Point(12, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(194, 310);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // projectFolderState
            // 
            this.projectFolderState.Controls.Add(this.btnProjFolderNext);
            this.projectFolderState.Controls.Add(this.btnProjFolderCancel);
            this.projectFolderState.Controls.Add(this.btnProjFolderReset);
            this.projectFolderState.Controls.Add(this.btnProjFolderBrowse);
            this.projectFolderState.Controls.Add(this.txtProjFolderPath);
            this.projectFolderState.Controls.Add(this.label1);
            this.projectFolderState.Controls.Add(this.lblWizardPrompt);
            this.projectFolderState.Controls.Add(this.pictureBox1);
            this.projectFolderState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectFolderState.Location = new System.Drawing.Point(0, 0);
            this.projectFolderState.Name = "projectFolderState";
            this.projectFolderState.Size = new System.Drawing.Size(610, 316);
            this.projectFolderState.StateManager = null;
            this.projectFolderState.TabIndex = 0;
            // 
            // btnProjFolderNext
            // 
            this.btnProjFolderNext.Location = new System.Drawing.Point(304, 281);
            this.btnProjFolderNext.Name = "btnProjFolderNext";
            this.btnProjFolderNext.Size = new System.Drawing.Size(277, 23);
            this.btnProjFolderNext.TabIndex = 1;
            this.btnProjFolderNext.Text = "Next";
            this.btnProjFolderNext.UseVisualStyleBackColor = true;
            // 
            // btnProjFolderCancel
            // 
            this.btnProjFolderCancel.Location = new System.Drawing.Point(223, 281);
            this.btnProjFolderCancel.Name = "btnProjFolderCancel";
            this.btnProjFolderCancel.Size = new System.Drawing.Size(75, 23);
            this.btnProjFolderCancel.TabIndex = 6;
            this.btnProjFolderCancel.Text = "Cancel";
            this.btnProjFolderCancel.UseVisualStyleBackColor = true;
            // 
            // btnProjFolderReset
            // 
            this.btnProjFolderReset.Location = new System.Drawing.Point(425, 169);
            this.btnProjFolderReset.Name = "btnProjFolderReset";
            this.btnProjFolderReset.Size = new System.Drawing.Size(75, 23);
            this.btnProjFolderReset.TabIndex = 4;
            this.btnProjFolderReset.Text = "Reset";
            this.btnProjFolderReset.UseVisualStyleBackColor = true;
            // 
            // btnProjFolderBrowse
            // 
            this.btnProjFolderBrowse.Location = new System.Drawing.Point(506, 169);
            this.btnProjFolderBrowse.Name = "btnProjFolderBrowse";
            this.btnProjFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnProjFolderBrowse.TabIndex = 5;
            this.btnProjFolderBrowse.Text = "Browse...";
            this.btnProjFolderBrowse.UseVisualStyleBackColor = true;
            // 
            // txtProjFolderPath
            // 
            this.txtProjFolderPath.Location = new System.Drawing.Point(223, 142);
            this.txtProjFolderPath.Name = "txtProjFolderPath";
            this.txtProjFolderPath.ReadOnly = true;
            this.txtProjFolderPath.Size = new System.Drawing.Size(358, 21);
            this.txtProjFolderPath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project Folder Location";
            // 
            // lblWizardPrompt
            // 
            this.lblWizardPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWizardPrompt.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWizardPrompt.Location = new System.Drawing.Point(220, 60);
            this.lblWizardPrompt.Name = "lblWizardPrompt";
            this.lblWizardPrompt.Size = new System.Drawing.Size(361, 51);
            this.lblWizardPrompt.TabIndex = 1;
            this.lblWizardPrompt.Text = "Welcome to New Project wizard!\r\nBefore we start, you need to specify a folder whe" +
    "re you will store your AniMat projects.";
            this.lblWizardPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Animat.UI.Properties.Resources.chibi_f;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 310);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // NewProjectWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 316);
            this.ControlBox = false;
            this.Controls.Add(this.projectDetailsState);
            this.Controls.Add(this.projectFolderState);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Project";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.projectDetailsState.ResumeLayout(false);
            this.projectDetailsState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.projectFolderState.ResumeLayout(false);
            this.projectFolderState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private libWyvernzora.Nightingale.WizardState projectFolderState;
        private System.Windows.Forms.Button btnProjFolderNext;
        private System.Windows.Forms.Button btnProjFolderCancel;
        private System.Windows.Forms.Button btnProjFolderReset;
        private System.Windows.Forms.Button btnProjFolderBrowse;
        private System.Windows.Forms.TextBox txtProjFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWizardPrompt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private libWyvernzora.Nightingale.WizardState projectDetailsState;
        private System.Windows.Forms.Button btnProjNameNext;
        private System.Windows.Forms.Button btnProjNameBack;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}