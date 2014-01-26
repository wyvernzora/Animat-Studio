using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Animat.UI.Utilities
{
    public static class IOUtilities
    {
        public static void EnsureDirectoryExists(String path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
