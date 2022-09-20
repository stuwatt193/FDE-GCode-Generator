using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE
{
    public class UniformField:Field
    {
        double value;

        /// <summary>
        /// Creates a field with the same value everywhere
        /// </summary>
        /// <param name="_value"></param>
        public UniformField(double _value)
        {
            this.value = _value;
        }

        public override double Value(Vec3 pos)
        {
            return value;
        }
    }
}
