using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animat.Project.Moduality
{
    /// <summary>
    /// Attribute that marks component factory classes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentFactoryAttribute : Attribute
    {
        /// <summary>
        /// Constructor.
        /// Ordered argument.
        /// </summary>
        /// <param name="type"></param>
        public ComponentFactoryAttribute(Type type)
        {
            //if (type.GetInterface("IComponentFactory") == null)
            //    throw new Exception("Invalid attribute usage: ModularComponentFactoryAttribute must be used on a class implementing IComponentFactory interface.");

            Type = type;
        }

        /// <summary>
        /// Gets or sets the component type of the factory class marked by this attribute.
        /// </summary>
        public Type Type { get; set; }
    }

    /// <summary>
    /// Attribute that provides a file filter info for component factory classes.
    /// </summary>
    /// <remarks>
    /// Format of the filter is like this: ComponentName (*.ext1; *.ext2)|*.ext1; *.ext2
    /// It is strongly advised that you use component name instead of file type description in the first part of the filter.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentFileFilterAttribute : Attribute
    {
        public ComponentFileFilterAttribute(String filter)
        {
            Filter = filter;
        }

        /// <summary>
        /// Gets or sets the file filter.
        /// </summary>
        public String Filter { get; set; }
    }

    /// <summary>
    /// Attribute that contains information about authorship and versioning of the component.
    /// Not required but encouraged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentInfoAttribute : Attribute
    {
        public ComponentInfoAttribute(String author, String version)
        {
            Author = author;
            Version = version;
        }

        public String Author { get; set; }

        public String Version { get; set; }
    }
}
