namespace FDE
{    
    public class Mat4x4
    {
        public Vec4 r1, r2, r3, r4;

        public Mat4x4(Vec4 row1, Vec4 row2, Vec4 row3, Vec4 row4)
        {
            r1 = row1;
            r2 = row2;
            r3 = row3;
            r4 = row4;
        }

        public Mat4x4(double[,] m)
        {
            r1 = new Vec4(m[0,0],m[0,1],m[0,2],m[0,3]);
            r2 = new Vec4(m[1,0],m[1,1],m[1,2],m[1,3]);
            r3 = new Vec4(m[2, 0], m[2, 1], m[2, 2], m[2, 3]);
            r4 = new Vec4(m[3, 0], m[3, 1], m[3, 2], m[3, 3]);
        }



    }
}
