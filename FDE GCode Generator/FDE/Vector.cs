using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE
{
    public abstract class Vector
    {
        public Vector() { }
        public abstract double Length();
        public abstract Vector Normalise();
        public abstract Vector Inv();
        public abstract Vector Abs();
        public abstract double Min();
        public abstract double Max();
    }
}
