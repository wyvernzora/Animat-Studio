using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animat.Project
{
    /// <summary>
    /// Attribute that marks custom asset loaders.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AssetLoaderAttribute : Attribute
    {
        public String Name { get; set; }

        public String Author { get; set; }

        public String Version { get; set; }
        
        public String Filter { get; set; }
    }


}
