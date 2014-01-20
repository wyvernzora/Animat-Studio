using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animat.UI.ToolWindows
{
    /// <summary>
    /// Interface for all UI elements that may update when the state changes.
    /// </summary>
    public interface IUpdateState
    {
        /// <summary>
        /// Execute update
        /// </summary>
        void UpdateState();
    }
}
