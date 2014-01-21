using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animat.UI
{
    /// <summary>
    /// Enum to identify different parts of the 
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
    /// Event args for cross-window update.
    /// </summary>
    public sealed class UpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scope">Scope of the update.</param>
        public UpdateEventArgs(UpdateScope scope = UpdateScope.All)
        {
            Scope = scope;
        }

        /// <summary>
        /// Gets the scope of the update.
        /// </summary>
        public UpdateScope Scope { get; set; }
    }

    /// <summary>
    /// Singleton core class that ties all Animat Studio component together.
    /// </summary>
    public sealed class StudioCore
    {
        #region Singleton

        private static StudioCore instance = null;

        /// <summary>
        /// Gets the global instance of the StudioCore class.
        /// </summary>
        public static StudioCore Instance
        {
            get { return instance ?? (instance = new StudioCore()); }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// Private to prevent creation of redundant instances.
        /// </summary>
        private StudioCore()
        {

        }

        #region Component Updating

        private EventHandler<UpdateEventArgs> onUpdateRequest;

        /// <summary>
        /// Event raised when an update of a Studio Component is requested.
        /// </summary>
        public event EventHandler<UpdateEventArgs>  OnUpdateRequest
        {
            add { onUpdateRequest += value; }
            remove { onUpdateRequest -= value; }
        }

        /// <summary>
        /// Requests a Animat Studio Update.
        /// </summary>
        /// <param name="scope">Scope of the update; default value is UpdateScope.All</param>
        public void RequestUpdate(UpdateScope scope = UpdateScope.All)
        {
            if (onUpdateRequest != null)
                onUpdateRequest(this, new UpdateEventArgs(scope));
        }

        #endregion



    }
}
