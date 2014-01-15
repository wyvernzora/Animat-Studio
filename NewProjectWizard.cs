using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Animat.UI.Project;
using libWyvernzora.Nightingale;

namespace Animat.UI
{
    public partial class NewProjectWizard : Form
    {
        private readonly WizardStateManager stateManager
            = new WizardStateManager();

        public NewProjectWizard()
        {
            InitializeComponent();
            AttachEventHandlers();

            // Attach States to Manager
            stateManager.AddState(projectFolderState);
            stateManager.AddState(projectDetailsState);
            stateManager.SetCurrentState(projectFolderState.Name);

            // Initialize Data Fields
            Load += (@s, e) =>
                {

                    // Initialize Project Folder
                    txtProjFolderPath.Text = YuaiProject.ProjectFolder;

                };


            
        }

        void AttachEventHandlers()
        {
            // Project Folder
            btnProjFolderCancel.Click += (@s, e) => Close();
            btnProjFolderReset.Click += (@s, e) =>
                {
                    YuaiProject.ProjectFolder = YuaiProject.DefaultProjectFolder;
                    txtProjFolderPath.Text = YuaiProject.ProjectFolder;
                };
            btnProjFolderBrowse.Click += (@s, e) =>
                {
                    var dialog = new FolderBrowserDialog();
                    dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        txtProjFolderPath.Text = dialog.SelectedPath;
                        YuaiProject.ProjectFolder = dialog.SelectedPath;
                    }
                };
            btnProjFolderNext.Click += (@s, e) =>
                {
                    // Clear the errors
                    errorProvider.Clear();  

                    // Try to create the folder and check for validity
                    try
                    {
                        if (!Directory.Exists(txtProjFolderPath.Text))
                            Directory.CreateDirectory(txtProjFolderPath.Text);
                    }
                    catch (Exception x)
                    {
                        errorProvider.SetError(txtProjFolderPath, x.Message);
                        return;
                    }

                    // Move to the next state
                    stateManager.SetCurrentState(projectDetailsState.Name);

                };

            // Project Name
            btnProjNameBack.Click += (@s, e) => stateManager.SetCurrentState(projectFolderState.Name);
            btnProjNameNext.Click += (@s, e) =>
                {
                    // Clear the errors
                    errorProvider.Clear();

                    if (String.IsNullOrWhiteSpace(txtProjectName.Text))
                    {
                        errorProvider.SetError(txtProjectName, "Project name cannot be empty!");
                        return;
                    }

                    ProjectName = txtProjectName.Text;
                    DialogResult = DialogResult.OK;
                };

        }


        #region Return Data

        public String ProjectName { get; private set; }

        #endregion
    }
}
