namespace FDE
{
    public class Vec4
    {
        public double x, y, z, a;

        public Vec4(double _x, double _y, double _z, double _a)
        {
            this.x = _x;
            this.y = _y;
            this.z = _z;
            this.a = _a;
        }

        public Vec4(Vec3 v, double _a)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.a = _a;
        }

        public static Vec4 operator *(Mat4x4 m, Vec4 v)
        {
            double xn = m.r1.x*v.x + m.r1.y*v.y + m.r1.z*v.z + m.r1.a*v.a;
            double yn = m.r2.x * v.x + m.r2.y * v.y + m.r2.z * v.z + m.r2.a * v.a;
            double zn = m.r3.x * v.x + m.r3.y * v.y + m.r3.z * v.z + m.r3.a * v.a;
            double an = m.r4.x * v.x + m.r4.y * v.y + m.r4.z * v.z + m.r4.a * v.a;
            return new Vec4(xn,yn,zn,an);
        }
    }
}
