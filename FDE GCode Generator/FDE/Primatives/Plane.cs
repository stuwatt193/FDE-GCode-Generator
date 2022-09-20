using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Primatives
{
    public class Plane:Field
    {
        Vec3 c, N;

        public Plane(Vec3 centre, Vec3 normal)
        {
            c = centre;
            N = normal;
        }

        public override double Value(Vec3 pos)
        {
            // https://mathinsight.org/distance_point_plane
            Vec3 v = pos - c;
            return Vec3.DotProduct(N.Normalise(), v); ;
        }

        
    }
}
