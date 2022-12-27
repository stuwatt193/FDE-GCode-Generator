using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Primatives
{
    public class Cylinder:Field
    {
        Vec3 p1, p2;
        double r;
        BoundingBox3D bb;

        public Cylinder(Vec3 point1, Vec3 point2, double radius)
        {
            this.p1 = point1;
            this.p2 = point2;
            this.r = radius;
        }

        public override double Value(Vec3 pos)
        {
            Vec3 a = pos - p1;
            Vec3 b = p2 - p1;
            Vec3 bHat = b.Normalise();
            Vec3 c = Vec3.DotProduct(a, bHat) * bHat;
            Vec3 d = a - c;

            double cylInf = d.Length() - r;
            double pln1 = Vec3.DotProduct(b.Inv().Normalise(), pos - p1);
            double pln2 = Vec3.DotProduct(b.Normalise(), pos - p2);

            return Math.Max(cylInf, Math.Max(pln1, pln2));
        }

        void CalculateBoundingBox() //https://iquilezles.org/articles/diskbbox/
        {
            Vec3 span = p2 - p1;
            Vec3 c = p1 + span / 2;
            bb = new BoundingBox3D(c, 2 * r, 2 * r, span.Abs().z);
        }
    }
}
