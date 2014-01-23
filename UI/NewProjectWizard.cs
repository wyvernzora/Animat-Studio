using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libWyvernzora.Nightingale;

namespace Animat.UI
{
    public partial class NewProjectWizard : Form
    {
        private Boolean manualProjectPath = false;

        public NewProjectWizard()
        {
            InitializeComponent();
            AttachEventHandlers();

            txtProjPath.Text = StudioCore.Instance.GetProjectDirectory(String.Empty);
        }

        void AttachEventHandlers()
        {
            txtProjName.TextChanged += (@s, e) =>
            {
                if (!manualProjectPath)
                {
                    ProjectPath = StudioCore.Instance.GetProjectDirectory(txtProjName.Text);
                    txtProjPath.Text = ProjectPath;
                }

                ProjectName = txtProjName.Text;
                
                btnOk.Enabled = CanUseName();
                lblError.Visible = !btnOk.Enabled;
            };

            btnOk.Click += (@s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();    
            };

            btnCancel.Click += (@s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            lnkChangeDir.Click += (@s, e) =>
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = StudioCore.Instance.DefaultProjectStore;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    manualProjectPath = true;
                    ProjectPath = dialog.SelectedPath;
                    txtProjPath.Text = ProjectPath;

                    // Update Error Prompt
                    btnOk.Enabled = CanUseName();
                    lblError.Visible = !btnOk.Enabled;
                }

            };
        }

        Boolean CanUseName()
        {
            // Check if name is empty
            if (String.IsNullOrWhiteSpace(txtProjName.Text))
            {
                lblError.Text = "The project name cannot be empty, please choose another name.";
                return false;
            }
            
            // Check if the folder is empty
            if (Directory.Exists(ProjectPath) &&
                (Directory.GetFiles(ProjectPath).Length > 0 || Directory.GetDirectories(ProjectPath).Length > 0))
            {
                if (manualProjectPath) lblError.Text = "The project path is occupied, please choose another path.";
                else lblError.Text = "The project path is occupied, please choose another name.";

                return false;
            }

            return true;
        }

        #region Return Data

        public String ProjectName { get; private set; }


        public String ProjectPath { get; private set; }

        #endregion
    }
}
