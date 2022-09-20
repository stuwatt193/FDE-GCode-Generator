using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Primatives
{
    public class Torus:Field
    {
        double rTorus, rTube;

        public Torus(double torusRadius, double tubeRadius)
        {
            this.rTorus = torusRadius;
            this.rTube = tubeRadius;
        }

        public override double Value(Vec3 pos)
        {
            double r = Math.Sqrt(pos.x * pos.x + pos.y * pos.y);
            double a = Math.Abs(rTorus * r);
            double hyp = Math.Sqrt(a * a + pos.z * pos.z);
            return hyp - rTube;
        }
    }
}
