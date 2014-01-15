using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Animat.UI.Project
{
    /* ProjectRoot
     *     
     * 
     * 
     *
     * 
     * 
     */

    #region Project Model
    /* Yuai Project File (*.bxproj) */

    [DataContract]
    public class ProjectModel
    {
        // Metadata

        [DataMember(Name = "fps")]
        public Int32 FPS { get; set; }

        [DataMember(Name = "name")]
        public String Name { get; set; }


        // Resources
        /// <summary>
        /// Gets paths to resource files used in the project.
        /// </summary>
        [DataMember(Name = "resources")]
        public String[] Resources { get; set; }

        // Frames
        /// <summary>
        /// Gets the list of frame definition files.
        /// </summary>
        [DataMember(Name = "frames")]
        public String[] FrameFiles { get; set; }

        // Sequences
        /// <summary>
        /// Gets the list of sequence definition files.
        /// </summary>
        [DataMember(Name = "sequences")]
        public String[] SequenceFiles { get; set; }

        // Events
        /// <summary>
        /// Gets the list of event definition files.
        /// </summary>
        [DataMember(Name = "events")]
        public String[] EventFiles { get; set; }

    }

    #endregion



}
