using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Animat.Project
{
    /// <summary>
    /// Represents a sequence of Animat Frames.
    /// </summary>
    [DataContract]
    public class StudioSequence
    {
        #region Nested Types

        /// <summary>
        /// Represents a layer in the sequence.
        /// </summary>
        [DataContract]
        public class Layer
        {
            /// <summary>
            /// Gets or sets the name of the layer.
            /// </summary>
            public String Name { get; set; }

            /// <summary>
            /// Gets or sets the list of frames in the layer.
            /// </summary>
            public SortedList<Int32, StudioFrame> Frames { get; set; } 
        }

        #endregion


    }
}
