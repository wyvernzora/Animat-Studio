using System;
using System.IO;

namespace Animat.Foundation
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
