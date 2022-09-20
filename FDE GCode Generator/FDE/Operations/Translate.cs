using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Operations
{
    public class Translate:Field
    {
        Field field;
        Vec3 v;

        /// <summary>
        /// Moves a field by the given vector
        /// </summary>
        /// <param name="_field"></param>
        /// <param name="vector"></param>
        public Translate(Field _field, Vec3 vector)
        {
            this.field = _field;
            this.v = vector;
        }

        public override double Value(Vec3 pos)
        {
            // To move shape in one direction you must move
            // the sampling position in the opposite direction
            Vec3 newPos = pos - v; 
            return field.Value(newPos);
        }
    }
}
