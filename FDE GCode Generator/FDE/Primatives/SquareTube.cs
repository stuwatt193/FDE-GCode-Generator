using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Primatives
{
    public  class SquareTube:Field
    {
        public Vec2 size;
        public double r;

        public SquareTube(Vec2 _size, double radius)
        {
            this.size = _size;
            this.r = radius;
        }

        public override double Value(Vec3 pos)
        {
            // Derived myself based on a similar principal to the torus
            Vec2 halfSize = size / 2;
            Vec2 q = pos.XY().Abs() - halfSize;
            double sd = Math.Abs(q.ItemwiseMax(0).Length() + Math.Min(q.Max(), 0.0));
            double hyp = Math.Sqrt(sd * sd + pos.z * pos.z);
            return hyp - r;
        }
    }
}
