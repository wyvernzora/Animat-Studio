using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animat.Project.Moduality;

namespace Animat.Project.AssetLoaders
{
    /// <summary>
    /// Default asset loader factory.
    /// Creates instances of all asset formats that are supported out of the box.
    /// </summary>
    [ComponentFactory(typeof(AssetBase))]
    [ComponentInfo("Aragorn Wyvernzora", "1.0.0.0")]
    [ComponentFileFilter("Default Assets (*.jpg; *.png; *.bmp; *.gif)|*.jpg; *.png; *.bmp; *.gif")]
    public sealed class DefaultAssetLoaderFactory : IComponentFactory<AssetBase>
    {
        /// <summary>
        /// Creates a new instance of AssetBase.
        /// Implementation depends on the file type.
        /// </summary>
        /// <param name="arguments">StudioProject project, String displayName, String filename</param>
        /// <returns></returns>
        public AssetBase Create(params object[] arguments)
        {
            // Check arguments
            if (arguments.Length != 3)
                throw new ArgumentOutOfRangeException("Unexpected number of arguments!");

            var project = arguments[0] as StudioProject;
            var displayName = arguments[1] as String;
            var fileName = arguments[2] as String;

            if (project == null || displayName == null || fileName == null)
                throw new ArgumentException("Unexpected argument types!");

            // Get extension and create loaders
            var extension = Path.GetExtension(fileName).ToLower();
            if (extension == ".jpg" || extension == ".bmp" || extension == ".png" || extension == ".gif")
                return new SingleFrameImageAsset(project, displayName, fileName) { FactoryName = GetType().FullName };
            else
            {
                throw new NotImplementedException();
            }

        }

        object IComponentFactory.Create(params object[] arguments)
        {
            return Create(arguments);
        }

        public Exception LoadingError
        { get; set; }
    }
}
