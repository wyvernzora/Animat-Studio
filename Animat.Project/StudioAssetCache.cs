using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libWyvernzora.Core;

namespace Animat.Project
{
    /// <summary>
    /// Asset Cache Manager
    /// </summary>
    public class StudioAssetCache : IDisposable
    {
        #region Nester Types

        /// <summary>
        /// Single cache entry
        /// </summary>
        private class CacheEntry
        {
            public String Name { get; set; }
            public VInt Address { get; set; }
            public VInt Length { get; set; }
        }

        #endregion

        private Dictionary<String, CacheEntry> entries;     // Map of filenames to entries
        private SortedList<Int32, Int32> holes;     // Sorted list of holes (unused data sections)





        #region IDisposable Members 

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
