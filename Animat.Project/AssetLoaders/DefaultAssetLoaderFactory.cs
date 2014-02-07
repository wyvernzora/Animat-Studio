// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/DefaultAssetLoaderFactory.cs
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
using System.IO;
using Animat.Project.Moduality;

namespace Animat.Project.AssetLoaders
{
    /// <summary>
    ///     Default asset loader factory.
    ///     Creates instances of all asset formats that are supported out of the box.
    /// </summary>
    [ComponentFactory(typeof(AssetBase))]
    [ComponentInfo("Aragorn Wyvernzora", "1.0.0.0")]
    [ComponentFileFilter("Default Assets (*.jpg; *.png; *.bmp; *.gif)|*.jpg; *.png; *.bmp; *.gif")]
    public sealed class DefaultAssetLoaderFactory : IComponentFactory<AssetBase>
    {
        /// <summary>
        ///     Creates a new instance of AssetBase.
        ///     Implementation depends on the file type.
        /// </summary>
        /// <param name="arguments">StudioProject project, String displayName, String filename</param>
        /// <returns></returns>
        public AssetBase Create(params object[] arguments)
        {
            // Check arguments
            if (arguments.Length != 3)
                throw new ArgumentOutOfRangeException("Unexpected number of arguments!");

            StudioProject project = arguments[0] as StudioProject;
            string displayName = arguments[1] as String;
            string fileName = arguments[2] as String;

            if (project == null || displayName == null || fileName == null)
                throw new ArgumentException("Unexpected argument types!");

            // Get extension and create loaders
            string extension = Path.GetExtension(fileName).ToLower();
            if (extension == ".jpg" || extension == ".bmp" || extension == ".png")
                return new SingleFrameImageAsset(project, displayName, fileName) {FactoryName = GetType().FullName};
            if (extension == ".gif")
                return new MultiFrameImageAsset(project, displayName, fileName) {FactoryName = GetType().FullName};

            throw new NotSupportedException("The file type is not supported by the asset loader factory!");
        }
        /// <summary>
        ///     Creates a new instance of AssetBase.
        ///     Implementation depends on the file type.
        /// </summary>
        /// <param name="arguments">StudioProject project, String displayName, String filename</param>
        /// <returns></returns>
        object IComponentFactory.Create(params object[] arguments)
        {
            return Create(arguments);
        }
        /// <summary>
        /// Specifies the error that occured during loading.
        /// If there is no error, this property is null.
        /// </summary>
        public Exception LoadingError { get; set; }
    }
}