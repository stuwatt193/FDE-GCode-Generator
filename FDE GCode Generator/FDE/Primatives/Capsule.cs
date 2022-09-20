using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Primatives
{
    public class Capsule:Field
    {
        public Vec3 p1;
        public Vec3 p2;
        public double r;

        public Capsule(Vec3 point1, Vec3 point2, double radius)
        {
            this.p1 = point1;
            this.p2 = point2;
            this.r = radius;
        }

        public override double Value(Vec3 pos)
        {
            // https://iquilezles.org/articles/distfunctions/
            Vec3 pa = pos - p1;
            Vec3 ba = p2 - p1;
            double h = Math.Clamp(Vec3.DotProduct(pa, ba) / Vec3.DotProduct(ba, ba), 0, 1);
            Vec3 rDist = pa - ba*h;
            return rDist.Length() - r;

        }
    }
}
