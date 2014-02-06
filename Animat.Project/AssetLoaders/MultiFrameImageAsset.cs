using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animat.Project.AssetLoaders
{
    public class MultiFrameImageAsset : AssetBase
    {

        public override int FrameCount
        {
            get { throw new NotImplementedException(); }
        }

        public override void BuildCache()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Image GetOriginalImage()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Image GetFrameImage(int index)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Image GetFrameThumbnail(int index)
        {
            throw new NotImplementedException();
        }
    }
}
