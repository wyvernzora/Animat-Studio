// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/Attributes.cs
// --------------------------------------------------------------------------------
// Copyright (c) 2014, Jieni Luchijinzhou a.k.a Aragorn Wyvernzora
// 
// This file is a part of Animat-Studio.
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

namespace Animat.Project.Moduality
{
    /// <summary>
    ///     Attribute that marks component factory classes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentFactoryAttribute : Attribute
    {
        /// <summary>
        ///     Constructor.
        ///     Ordered argument.
        /// </summary>
        /// <param name="type"></param>
        public ComponentFactoryAttribute(Type type)
        {
            //if (type.GetInterface("IComponentFactory") == null)
            //    throw new Exception("Invalid attribute usage: ModularComponentFactoryAttribute must be used on a class implementing IComponentFactory interface.");

            Type = type;
        }

        /// <summary>
        ///     Gets or sets the component type of the factory class marked by this attribute.
        /// </summary>
        public Type Type { get; set; }
    }

    /// <summary>
    ///     Attribute that provides a file filter info for component factory classes.
    /// </summary>
    /// <remarks>
    ///     Format of the filter is like this: ComponentName (*.ext1; *.ext2)|*.ext1; *.ext2
    ///     It is strongly advised that you use component name instead of file type description in the first part of the
    ///     filter.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentFileFilterAttribute : Attribute
    {
        public ComponentFileFilterAttribute(String filter)
        {
            Filter = filter;
        }

        /// <summary>
        ///     Gets or sets the file filter.
        /// </summary>
        public String Filter { get; set; }
    }

    /// <summary>
    ///     Attribute that contains information about authorship and versioning of the component.
    ///     Not required but encouraged.
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