namespace FDE
    ///<summary>
    ///A 3D vector for use in vector calculus
    ///</summary>
{    public class Vec3
    {
        public double x, y, z;

        /// <summary>
        /// Creates a 3D vector with all the components set to 0
        /// </summary>
        public Vec3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        /// <summary>
        /// Creates a 3D vector with all the components set to the same value
        /// </summary>
        public Vec3(double _a)
        {
            this.x = _a;
            this.y = _a;
            this.z = _a;
        }

        /// <summary>
        /// Creates a 3D vector with different x,y,z components
        /// </summary>
        public Vec3(double _x, double _y, double _z)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
        }

        /// <summary>
        /// Returns the dot product of 2 vectors
        /// </summary>
        public static double DotProduct(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// Returns the cross product of 2 vectors
        /// </summary>
        public static Vec3 CrossProduct(Vec3 a, Vec3 b)
        {
            double i = a.y * b.z - a.z * b.y;
            double j = a.z * b.x - a.x * b.z;
            double k = a.x * b.y - a.y * b.x;
            return new Vec3(i, j, k);
        }

        /// <summary>
        /// Returns the angle between two vectors
        /// </summary>
        public static double AngleBetweenVectors(Vec3 a, Vec3 b)
        {
            return Math.Acos(Vec3.DotProduct(a, b) / (a.Length() * b.Length()));
        }

        /// <summary>
        /// Returns the length of the vector
        /// </summary>
        public double Length()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        /// <summary>
        /// Returns the unit vector of the parent vector
        /// </summary>
        public Vec3 Normalise()
        {
            return this / Length();
        }

        /// <summary>
        /// Returns the inverse of the vector
        /// </summary>
        public Vec3 Inv()
        {
            return new Vec3(-x, -y, -z);
        }

        /// <summary>
        /// Returns a vector with elementwise absolute values of the parent vector
        /// </summary>
        public Vec3 Abs()
        {
            Vec3 absV = new Vec3();
            absV.x = Math.Abs(x);
            absV.y = Math.Abs(y);
            absV.z = Math.Abs(z);
            return absV;
        }

        /// <summary>
        /// Give the 2d vector containing the X and Y elements of the 3D vector
        /// </summary>
        public Vec2 XY()
        {
            Vec2 v = new Vec2(x, y);
            return v;
        }

        /// <summary>
        /// Give the 2d vector containing the Y and Z elements of the 3D vector
        /// </summary>
        public Vec2 YZ()
        {
            Vec2 v = new Vec2(y, z);
            return v;
        }

        /// <summary>
        /// Give the 2d vector containing the Z and X elements of the 3D vector
        /// </summary>
        public Vec2 ZX()
        {
            Vec2 v = new Vec2(z, x);
            return v;
        }

        /// <summary>
        /// Returns the value of the smallest element of the vector
        /// </summary>
        public double Min()
        {
            return Math.Min(x,Math.Min(y,z));
        }

        /// <summary>
        /// Returns the value of the largest element of the vector
        /// </summary>
        public double Max()
        {
            return Math.Max(x, Math.Max(y, z));
        }

        /// <summary>
        /// Returns a vector with the elementwise smallest of the element and the given value
        /// </summary>
        public Vec3 ItemwiseMin(double c)
        {
            Vec3 minV = new Vec3();
            minV.x = Math.Min(x, c);
            minV.y = Math.Min(y, c);
            minV.z = Math.Min(z, c);
            return minV;
        }

        /// <summary>
        /// Returns a vector with the elementwise largest of the element and the given value
        /// </summary>
        public Vec3 ItemwiseMax(double c)
        {
            Vec3 maxV = new Vec3();
            maxV.x = Math.Max(x, c);
            maxV.y = Math.Max(y, c);
            maxV.z = Math.Max(z, c);
            return maxV;
        }

        /// <summary>
        /// Returns a unit vector in the x axis
        /// </summary>
        public static Vec3 xAxis()
        {
            return new Vec3(1, 0, 0);
        }

        /// <summary>
        /// Returns a unit vector in the y axis
        /// </summary>
        public static Vec3 yAxis()
        {
            return new Vec3(0, 1, 0);
        }

        /// <summary>
        /// Returns a unit vector in the z axis
        /// </summary>
        public static Vec3 zAxis()
        {
            return new Vec3(0, 0, 1);
        }


        /// <summary>
        /// Returns the elementwise addition of two vectors
        /// </summary>
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            Vec3 c = new Vec3();
            c.x = a.x + b.x;
            c.y = a.y + b.y;
            c.z = a.z + b.z;
            return c;
        }

        /// <summary>
        /// Add the given value to each element
        /// </summary>
        public static Vec3 operator +(Vec3 a, double b)
        {
            Vec3 c = new Vec3();
            c.x = a.x + b;
            c.y = a.y + b;
            c.z = a.z + b;
            return c;
        }

        /// <summary>
        /// Returns the elementwise subtraction of two vectors
        /// </summary>
        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            Vec3 c = new Vec3();
            c.x = a.x - b.x;
            c.y = a.y - b.y;
            c.z = a.z - b.z;
            return c;
        }

        /// <summary>
        /// Subtracts the given value to each element
        /// </summary>
        public static Vec3 operator -(Vec3 a, double b)
        {
            Vec3 c = new Vec3();
            c.x = a.x - b;
            c.y = a.y - b;
            c.z = a.z - b;
            return c;
        }

        /// <summary>
        /// Multiplies each element by the given value
        /// </summary>
        public static Vec3 operator *(Vec3 a, double d)
        {
            return new Vec3(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Multiplies each element by the given value
        /// </summary>
        public static Vec3 operator *(double d, Vec3 a)
        {
            return new Vec3(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Divides each element by the given value
        /// </summary>
        public static Vec3 operator /(Vec3 a, double d)
        {
            return new Vec3(a.x / d, a.y / d, a.z / d);
        }


        /// <summary>
        /// Returns a string of the vector in a print suitable format
        /// </summary>
        public override string ToString()
        {
            return String.Format("[{0:0.000}|{1:0.000}|{2:0.000}]", x, y, z);
        }

        /// <summary>
        /// Returns a string of the vector in a comma seperated format
        /// </summary>
        public string ToCsvString()
        {
            return String.Format("{0:0.000},{1:0.000},{2:0.000}", x, y, z);
        }
    }
}
