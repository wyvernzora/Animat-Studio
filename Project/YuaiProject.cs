using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animat.UI.Project
{
    /// <summary>
    /// Singleton Project Class.
    /// Holds references to all data used within the project.
    /// </summary>
    public class YuaiProject
    {
        #region Singleton

        private static YuaiProject instance = null;

        public static YuaiProject Instance
        {
            get { return instance; }
        }

        public static void InitializeProject()
        {
            
        }

        #endregion



    }
}
