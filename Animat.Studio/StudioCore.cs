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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using Animat.Project;
using Animat.Project.Moduality;
using Animat.Studio.Properties;
using Animat.Studio.UI.ToolWindows;
using DigitalRune.Windows.Docking;
using libWyvernzora.IO;
using Mustache;
using NLog;

namespace Animat.Studio
{

    /// <summary>
    ///     Singleton core class that ties all Animat Studio component together.
    /// </summary>
    public sealed class StudioCore
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

        #region Cross-Component Messaging

        #region Update Requests

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

        /// <summary>
        /// Resuest update of a specifiv UI component with a message
        /// containing informationg about updating.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="target"></param>
        /// <param name="message"></param>
        public void RequestUpdate(UpdateScope scope, String target, Object message)
        {
            if (onUpdateRequest != null)
                onUpdateRequest(this, new UpdateEventArgs(scope) { Target = target, UpdateMessage = message });
            
        }

        #endregion

        public void ProcessProjectScopedCommand(String command, String target)
        {
            switch (command)
            {
                case "create": MainForm.Instance.CreateProject();
                    break;
                case "open":
                    break;
                case "pin":
                    var p0 = StudioSettings.Instance.FindProjectById(target);
                    p0.IsPinned = true;
                    RequestUpdate(UpdateScope.StartPage);
                    StudioSettings.Instance.Save();
                    break;
                case "unpin":
                    var p1 = StudioSettings.Instance.FindProjectById(target);
                    p1.IsPinned = false;
                    RequestUpdate(UpdateScope.StartPage);
                    StudioSettings.Instance.Save();
                    break;
            }
        }

        /// <summary>
        /// Opens the specified asset in the asset viewer.
        /// </summary>
        /// <param name="asset"></param>
        public void ViewAsset(AssetBase asset)
        {
            var viewer = AssetViewer.GetInstance(asset);
            MainForm.Instance.PushDockableWindow(viewer, DockState.Document);
        }


        #endregion
    }

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
        AssetViewer = 16,
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

        /// <summary>
        /// String specifying the target of the update request,
        /// null if there is no specific target.
        /// </summary>
        public String Target { get; set; }

        /// <summary>
        /// Specific message attached to the update request.
        /// Null if there is no message.
        /// </summary>
        public Object UpdateMessage { get; set; }
    }
}