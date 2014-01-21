using System;


namespace Animat.UI
{ 
    /// <summary>
    /// Exception that occurs when creation of a duplicate singleton instance is attempted.
    /// </summary>
    public class DuplicateInstanceException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        public DuplicateInstanceException(Type type)
        {
            Singleton = type;
        }

        /// <summary>
        /// Gets or sets the type of the singleton.
        /// </summary>
        public Type Singleton { get; set; }
    }

}