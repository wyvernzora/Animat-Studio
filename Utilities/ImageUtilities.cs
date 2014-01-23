using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Animat.UI.Utilities
{
    /// <summary>
    /// Class with image/bitmap utility methods.
    /// </summary>
    public static class ImageUtilities
    {

        /// <summary>
        /// Wrapper around Image.GetThumbnailImage() method.
        /// </summary>
        /// <param name="img">Image to get the thumbnail of.</param>
        /// <param name="width">Width of the resulting thumbnail image.</param>
        /// <param name="height">Height of the resulting thumbnail image.</param>
        /// <returns></returns>
        public static Image GetThumbnailEx(this Image img, int width, int height)
        {
            return img.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero);
        }

        /// <summary>
        /// Wrapper around Image.GetThumbnailImage().
        /// </summary>
        /// <param name="img">Image to get the thumbnail of.</param>
        /// <param name="boxSize">Size of the rectangulat box the image has to fit in.</param>
        /// <returns></returns>
        public static Image GetThumbnailEx(this Image img, int boxSize)
        {
            double scale = 1.0;
            if (img.Width > img.Height)
                scale = boxSize / (double) img.Width;
            else
                scale = boxSize / (double) img.Height;
            if (scale > 1) scale = 1.0;

            int nWidth = (int) (img.Width * scale);
            int nHeight = (int) (img.Height * scale);

            return GetThumbnailEx(img, nWidth, nHeight);
        }

    }
}
