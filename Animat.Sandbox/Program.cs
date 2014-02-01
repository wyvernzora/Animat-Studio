using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animat.UI;

namespace Animat.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            var holes = new Animat.Project.CacheManager.HoleCollection();

            holes.Add(100, 100);
            holes.Add(300, 100);
            holes.Add(500, 100);
            holes.Add(700, 100);

            holes.Add(0, 100);
            holes.Add(200, 99);
            holes.Add(299, 1);

        }
    }
}
