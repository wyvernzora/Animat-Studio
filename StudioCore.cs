// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat Studio/StudioCore.cs
// --------------------------------------------------------------------------------
// Copyright (c) 2014, Jieni Luchijinzhou a.k.a Aragorn Wyvernzora
// 
// This file is a part of Animat Studio.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
// of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Animat.UI.Properties;
using Animat.UI.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.IO;

namespace Animat.UI
{
    /// <summary>
    ///     Enum to identify different parts of the
    /// </summary>
    [Flags]
    public enum UpdateScope
    {
        None = 0,
        ParentForm = 1,
        Explorer = 2,
        Preview = 4,
        StartPage = 8,
        All = -1
    }

    /// <summary>
    ///     Event args for cross-window update.
    /// </summary>
    public sealed class UpdateEventArgs : EventArgs
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="scope">Scope of the update.</param>
        public UpdateEventArgs(UpdateScope scope = UpdateScope.All)
        {
            Scope = scope;
        }

        /// <summary>
        ///     Gets the scope of the update.
        /// </summary>
        public UpdateScope Scope { get; set; }
    }

    /// <summary>
    ///     Singleton core class that ties all Animat Studio component together.
    /// </summary>
    public sealed class StudioCore
    {
        #region Singleton

        private static StudioCore instance;

        /// <summary>
        ///     Gets the global instance of the StudioCore class.
        /// </summary>
        public static StudioCore Instance
        {
            get { return instance ?? (instance = new StudioCore()); }
        }

        #endregion

        /// <summary>
        ///     Constructor.
        ///     Private to prevent creation of redundant instances.
        /// </summary>
        private StudioCore()
        {
        }

        #region Project Management

        private StudioProject project;

        /// <summary>
        /// Gets or sets the loaded project.
        /// </summary>
        public StudioProject Project
        {
            get { return project; }
            set
            {
                if (project != value)
                {
                    project = value;
                    RequestUpdate();
                }
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether there is a loaded project.
        /// </summary>
        public Boolean HasProject
        { get { return Project != null; } }
        
        #endregion

        #region Directory Structure Management

        /* This part is strictly for getting directory names
         * and nothing more. No file IO here! 
         * ONLY FOR PROJECTS IN CURRENT STORE
         */

        // Constants
// ReSharper disable InconsistentNaming
        private const String DEF_PROJ_DIR = "Animat Studio Projects";
// ReSharper restore InconsistentNaming

        /// <summary>
        ///     Gets the default project directory.
        /// </summary>
        public String DefaultProjectStore
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    DEF_PROJ_DIR);
            }
        }

        /// <summary>
        ///     Gets or sets the current project directory.
        /// </summary>
        public String ProjectStore
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Settings.Default.ProjectFolder))
                {
                    Settings.Default.ProjectFolder = DefaultProjectStore;
                    Settings.Default.Save();
                }

                return Settings.Default.ProjectFolder;
            }
            set
            {
                Settings.Default.ProjectFolder = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        ///     Processes the project name to comply with filename requirements.
        /// </summary>
        /// <param name="projectName">Raw project name.</param>
        /// <returns></returns>
        public String ProcessProjectName(String projectName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                projectName = projectName.Replace(c, '_');
            foreach (char c in Path.GetInvalidPathChars())
                projectName = projectName.Replace(c, '_');
            projectName = projectName.Replace(' ', '-');

            return projectName;
        }

        /// <summary>
        ///     Gets the path to a project by name.
        /// </summary>
        /// <param name="name">Unprocessed project name.</param>
        /// <returns></returns>
        public String GetProjectDirectory(String name)
        {
            return Path.Combine(ProjectStore, ProcessProjectName(name));
        }


        #endregion

        #region Component Updating

        private EventHandler<UpdateEventArgs> onUpdateRequest;

        /// <summary>
        ///     Event raised when an update of a Studio Component is requested.
        /// </summary>
        public event EventHandler<UpdateEventArgs> OnUpdateRequest
        {
            add { onUpdateRequest += value; }
            remove { onUpdateRequest -= value; }
        }

        /// <summary>
        ///     Requests a Animat Studio Update.
        /// </summary>
        /// <param name="scope">Scope of the update; default value is UpdateScope.All</param>
        public void RequestUpdate(UpdateScope scope = UpdateScope.All)
        {
            if (onUpdateRequest != null)
                onUpdateRequest(this, new UpdateEventArgs(scope));
        }

        #endregion

        #region Selection and Current State

        /// <summary>
        /// Gets or sets the asset to preview.
        /// </summary>
        public Image PreviewAsset
        { get; set; }

        #endregion

        #region Cross-Component Actions

        /// <summary>
        /// Opens the specified asset in the asset viewer.
        /// </summary>
        /// <param name="asset"></param>
        public void ViewAsset(StudioAsset asset)
        {
            var viewer = AssetViewer.GetInstance(asset);
            MainForm.Instance.PushDockableWindow(viewer, DockState.Document);
        }

        #endregion
    }
}