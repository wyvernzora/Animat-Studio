using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animat.Project;
using NLog;

namespace Animat.CacheViewer
{
    /// <summary>
    /// Extension of StudioCacheManager that enables enumeration.
    /// </summary>
    class ObservableCacheManager : CacheManager
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Constructor.
        /// Creates a cache manager in the specified directory.
        /// </summary>
        /// <param name="dir"></param>
        public ObservableCacheManager(String dir)
        {
            logger.Info("Creating new StudioCacheManager instance...");


            // Construct paths
            string indexPath = Path.Combine(dir, IndexFile);
            string dataPath = Path.Combine(dir, DataFile);

            Initialize(indexPath, dataPath);
        }


        public IEnumerable<Int32> GetEntryIDs()
        {
            return (from a in entries.Values select (Int32) (a.ID >> 32));
        }   
    }
}
