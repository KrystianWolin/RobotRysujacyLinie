using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;

namespace RobotRysujacyLinie
{
    class Rysuj
    {
        public void BoiskoPilkaNozna(Image<Rgb, byte> image)
        {
            image.SetValue(new MCvScalar(0, 0, 0));
        }
    }
}
