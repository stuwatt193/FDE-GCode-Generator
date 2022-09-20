using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE
{
    public abstract class Field
    { 
        public Field()
        {
        }

        /// <summary>
        /// Gives the value of the field at supplied position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public abstract double Value(Vec3 pos);

        /// <summary>
        /// Returns the gradient of the field at the given position by sampling 6 points arouund the position.
        /// </summary>
        /// <param name="pos">Sample Position</param>
        /// <returns></returns>
        public Vec3 Gradient(Vec3 pos)
        {
            double d = 0.00001;
            Vec3 delX = new Vec3(d, 0, 0);
            double dx = Value(pos + delX) - Value(pos - delX);
            Vec3 delY = new Vec3(0, d, 0);
            double dy = Value(pos + delY) - Value(pos - delY);
            Vec3 delZ = new Vec3(0, d, 0);
            double dz = Value(pos + delZ) - Value(pos - delZ);
            return new Vec3(dx, dy, dz).Normalise();
        }
    }    
}
