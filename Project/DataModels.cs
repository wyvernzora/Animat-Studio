using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Runtime.Serialization;
using libWyvernzora.IO;

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
        public List<String> Resources { get; set; }

        // Frames
        /// <summary>
        /// Gets the list of frame definition files.
        /// </summary>
        [DataMember(Name = "frames")]
        public List<String> FrameFiles { get; set; }

        // Sequences
        /// <summary>
        /// Gets the list of sequence definition files.
        /// </summary>
        [DataMember(Name = "sequences")]
        public List<String> SequenceFiles { get; set; }

        // Events
        /// <summary>
        /// Gets the list of event definition files.
        /// </summary>
        [DataMember(Name = "events")]
        public List<String> EventFiles { get; set; }


        #region Save/Load Methods

        /// <summary>
        /// Serializes the Project Model to a file.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        public static void Serialize(String path, ProjectModel model)
        {
            // Make sure that the parent directory exists
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Serialize
            using (var stream = new StreamEx(path, FileMode.Create))
            {
                var serializer = new DataContractJsonSerializer(typeof(ProjectModel));
                serializer.WriteObject(stream, model);
            }
        }

        /// <summary>
        /// Deserializes the Project Model froma file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ProjectModel Deserialize(String path)
        {
            // Make sure the file is there
            if (!File.Exists(path))
                throw new FileNotFoundException("Cannot find the specified file!", path);

            // Deserialize
            ProjectModel model = null;
            using (var stream = new StreamEx(path))
            {
                var serializer = new DataContractJsonSerializer(typeof(ProjectModel));
                model = (ProjectModel)serializer.ReadObject(stream);
            }

            return model;
        }

        #endregion
    }

    #endregion



}
