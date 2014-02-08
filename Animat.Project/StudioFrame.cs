using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Animat.Project
{
    /// <summary>
    /// Studio Frame class.
    /// Represents one single frame in the sequence.
    /// </summary>
    [DataContract]
    public class StudioFrame
    {
        /// <summary>
        /// Gets or sets the ID of the frame.
        /// </summary>
        [DataMember(Name = "id")]
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the ID of the referenced asset.
        /// </summary>
        [DataMember(Name = "asset-id")]
        public Guid AssetID { get; set; }

        /// <summary>
        /// Gets or sets the index of the referenced frame within the asset.
        /// </summary>
        [DataMember(Name = "asset-frame-index")]
        public Int32 AsssetFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the source reference rectangle.
        /// </summary>
        [DataMember(Name = "src-rect")]
        public Rectangle SourceRectangle { get; set; }

        /// <summary>
        /// Gets or sets the destination rectangle.
        /// </summary>
        [DataMember(Name = "dest-rect")]
        public Rectangle DestinationRectangle { get; set; }

        /// <summary>
        /// Gets or sets the start time of the frame (inclusive).
        /// </summary>
        [DataMember(Name = "start-time")]
        public Int32 StartFrame { get; set; }

        /// <summary>
        /// Gets or sets the duration of the frame.
        /// </summary>
        [DataMember(Name = "duration")]
        public Int32 Duration { get; set; }
    }
}
