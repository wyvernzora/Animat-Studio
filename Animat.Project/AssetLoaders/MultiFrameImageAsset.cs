// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat-Studio/MultiFrameImageAsset.cs
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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using libWyvernzora.IO;

namespace Animat.Project.AssetLoaders
{
    /// <summary>
    /// Asset loader for multi-frame images (i.e. GIF).
    /// </summary>
    public class MultiFrameImageAsset : AssetBase
    {
        private readonly Int32 frameCount;
        private readonly Image[] thumbnails;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="project">The project that this asset is associated with.</param>
        /// <param name="name">The name of the asset that is displayed to the user.</param>
        /// <param name="filename">The name of the asset file.</param>
        public MultiFrameImageAsset(StudioProject project, String name, String filename)
            : base(project, name, filename)
        {
            try
            {
                string path = Path.Combine(Project.GetAssetDirectory(), filename);

                // Load the image and get the frame count
                using (Image img = Image.FromFile(path))
                {
                    FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);
                    frameCount = img.GetFrameCount(dimension);
                }

                thumbnails = new Image[frameCount];
            }
            catch (Exception x) {
                Error = x;
            }
        }

        #region AssetBase Members

        public override int FrameCount
        {
            get { return frameCount; }
        }

        public override void BuildCache()
        {
            // Do not proceed if there is an error
            if (Error != null)
                return;

            // Get the cache manager
            CacheManager cacheMgr = Project.CacheManager;

            string path = Path.Combine(Project.GetAssetDirectory(), Filename);

            using (Image img = Image.FromFile(path))
            {
                // Get Frame Dimension
                FrameDimension dimension = new FrameDimension(img.FrameDimensionsList[0]);

                // Cahce frames and their thumbnails
                for (int i = 0; i < FrameCount; i++)
                {
                    string frameName = String.Format("{0}/F/{1}", ID, i);
                    string thumbName = String.Format("{0}/C/{1}", ID, i);

                    // Select the frame
                    img.SelectActiveFrame(dimension, i);

                    // Cache the frame
                    using (StreamEx frameTmp = new StreamEx(new MemoryStream()))
                    {
                        img.Save(frameTmp, ImageFormat.Png);
                        frameTmp.Position = 0;
                        cacheMgr.AddEntry(frameName, frameTmp);
                    }

                    // Cache the thumbnail
                    using (StreamEx thumbTemp = new StreamEx(new MemoryStream()))
                    {
                        Image thumb = GetThumbnailEx(img, Project.ThumbnailSize);
                        thumb.Save(thumbTemp, ImageFormat.Png);
                        thumbTemp.Position = 0;
                        cacheMgr.AddEntry(thumbName, thumbTemp);
                    }
                }
            }
        }

        public override Image GetOriginalImage()
        {
            // Do not proceed if there is an error.
            if (Error != null)
                return null;

            // This class should not throw exceptions.
            try
            {
                string path = Path.Combine(Project.GetAssetDirectory(), Filename);
                return Image.FromFile(path);
            }
            catch (Exception x)
            {
                Error = x;
                return null;
            }
        }

        public override Image GetFrameImage(int index)
        {
            // Return null if there is an error.
            if (Error != null)
                return null;

            // Check if index is within range
            if (index < 0 || index >= frameCount)
                throw new ArgumentOutOfRangeException("index");

            // Attempt to get frame from cache
            string frameName = String.Format("{0}/F/{1}", ID, index); // Frame name
            if (!Project.CacheManager.ContainsEntry(frameName))
            {
                // If frame is not in the cache, put it in there
                using (Image original = GetOriginalImage())
                {
                    FrameDimension dimension = new FrameDimension(original.FrameDimensionsList[0]);
                    original.SelectActiveFrame(dimension, index);

                    using (StreamEx frametmp = new StreamEx(new MemoryStream()))
                    {
                        original.Save(frametmp, ImageFormat.Png);
                        frametmp.Position = 0;
                        Project.CacheManager.AddEntry(frameName, frametmp);
                    }
                }
            }

            return Image.FromStream(Project.CacheManager.GetEntry(frameName));
        }

        public override Image GetFrameThumbnail(int index)
        {
            // Return null if there is an error.
            if (Error != null)
                return null;

            // Check if index is within range
            if (index < 0 || index >= frameCount)
                throw new ArgumentOutOfRangeException("index");

            string thumbName = String.Format("{0}/C/{1}", ID, index); // Frame name
            // If it is not cached in memory...
            if (thumbnails[index] == null)
            {
                // Attempt to get frame from cache
                if (!Project.CacheManager.ContainsEntry(thumbName))
                {
                    // If thumbnail is not in the cache, put it there
                    using (Image frame = GetFrameImage(index))
                    using (StreamEx stream = new StreamEx(new MemoryStream()))
                    {
                        Image thumb = GetThumbnailEx(frame, Project.ThumbnailSize);
                        thumb.Save(stream, ImageFormat.Png);
                        stream.Position = 0;
                        Project.CacheManager.AddEntry(thumbName, stream);
                    }
                }
            }


            thumbnails[index] = Image.FromStream(Project.CacheManager.GetEntry(thumbName));
            return thumbnails[index];
        }

        #endregion
    }
}