namespace FDE
{
    public class Vec2
    {
        public double x, y;

        public Vec2()
        {
            this.x = 0;
            this.y = 0;
        }
        public Vec2(double _a)
        {
            this.x = _a;
            this.y = _a;
        }

        public Vec2(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
        }

        /// <summary>
        /// Returns the dot product of 2 vectors
        /// </summary>
        public static double DotProduct(Vec2 a, Vec2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        public static double AngleBetweenVectors(Vec2 a, Vec2 b)
        {
            return Math.Acos(Vec2.DotProduct(a, b) / (a.Length() * b.Length()));
        }

        /// <summary>
        /// Returns the length of the vector
        /// </summary>
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y);
        }

        /// <summary>
        /// Returns the unit vector of the parent vector
        /// </summary>
        public Vec2 Normalise()
        {
            return this / Length();
        }

        /// <summary>
        /// Returns the inverse of the vector
        /// </summary>
        public Vec2 Inv()
        {
            return new Vec2(-x, -y);
        }

        /// <summary>
        /// Returns a vector with elementwise absolute values of the parent vector
        /// </summary>
        public Vec2 Abs()
        {
            Vec2 absV = new Vec2();
            absV.x = Math.Abs(x);
            absV.y = Math.Abs(y);
            return absV;
        }

        /// <summary>
        /// Returns the value of the smallest element of the vector
        /// </summary>
        public double Min()
        {
            return Math.Min(x, y);
        }

        /// <summary>
        /// Returns the value of the largest element of the vector
        /// </summary>
        public double Max()
        {
            return Math.Max(x, y);
        }

        /// <summary>
        /// Returns a vector with the elementwise smallest of the element and the given value
        /// </summary>
        public Vec2 ItemwiseMin(double c)
        {
            Vec2 minV = new Vec2();
            minV.x = Math.Min(x, c);
            minV.y = Math.Min(y, c);
            return minV;
        }

        /// <summary>
        /// Returns a vector with the elementwise largest of the element and the given value
        /// </summary>
        public Vec2 ItemwiseMax(double c)
        {
            Vec2 maxV = new Vec2();
            maxV.x = Math.Max(x, c);
            maxV.y = Math.Max(y, c);
            return maxV;
        }

        /// <summary>
        /// Returns a unit vector in the x axis
        /// </summary>
        public static Vec2 xAxis()
        {
            return new Vec2 (1, 0);
        }

        /// <summary>
        /// Returns the elementwise addition of two vectors
        /// </summary>
        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            Vec2 c = new Vec2();
            c.x = a.x + b.x;
            c.y = a.y + b.y;
            return c;
        }

        /// <summary>
        /// Add the given value to each element
        /// </summary>
        public static Vec2 operator +(Vec2 a, double b)
        {
            Vec2 c = new Vec2();
            c.x = a.x + b;
            c.y = a.y + b;
            return c;
        }

        /// <summary>
        /// Returns the elementwise subtraction of two vectors
        /// </summary>
        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            Vec2 c = new Vec2();
            c.x = a.x - b.x;
            c.y = a.y - b.y;
            return c;
        }

        /// <summary>
        /// Subtracts the given value to each element
        /// </summary>
        public static Vec2 operator -(Vec2 a, double b)
        {
            Vec2 c = new Vec2();
            c.x = a.x - b;
            c.y = a.y - b;
            return c;
        }

        /// <summary>
        /// Multiplies each element by the given value
        /// </summary>
        public static Vec2 operator *(Vec2 a, double d)
        {
            return new Vec2(a.x * d, a.y * d);
        }

        /// <summary>
        /// Divides each element by the given value
        /// </summary>
        public static Vec2 operator /(Vec2 a, double d)
        {
            return new Vec2(a.x / d, a.y / d);
        }

        /// <summary>
        /// Returns a string of the vector in a print suitable format
        /// </summary>
        public override string ToString()
        {
            return String.Format("[{0:0.000}|{1:0.000}]", x, y);
        }

        /// <summary>
        /// Returns a string of the vector in a comma seperated format
        /// </summary>
        public string ToCsvString()
        {
            return String.Format("{0:0.000},{1:0.000}", x, y);
        }
    }
}
