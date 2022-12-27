using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE
{
    public  class BoundingBox3D
    {
        public Vec3 c;
        public Vec3 minPt, maxPt;
        public Vec3 span;

        public BoundingBox3D(Vec3 centre, double width, double height, double depth)
        {
            c = centre;      

            span = new Vec3(width, height, depth);
            minPt = c - span / 2;
            maxPt = c + span / 2;
        }

        public BoundingBox3D(Vec3 minPoint, Vec3 maxPoint) 
        {
            Vec3 spanSigned = maxPoint - minPoint;
            span = spanSigned.Abs();

            c = minPoint + span / 2;
        }

    }
}
