using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE
{
    public class Rotate:Field
    {
        public Vec3 c;
        public Vec3 v;
        public double ang;
        public Field field;

        Mat4x4 t, tInv;
        Mat4x4 rX, rXInv;
        Mat4x4 rY, rYInv;
        Mat4x4 rZ;



        public Rotate(Field _field, Vec3 rotationCentre, Vec3 rotationVector, double angleInDegrees)
        {
            //https://www.eng.uc.edu/~beaucag/Classes/Properties/OptionalProjects/CoordinateTransformationCode/Rotate%20about%20an%20arbitrary%20axis%20(3%20dimensions).html
            this.field = _field;
            this.c = rotationCentre;
            this.v = rotationVector;
            this.ang = Math.PI* angleInDegrees/180;

            t = new Mat4x4(new double[,] { { 1, 0, 0, -c.x }, { 0, 1, 0, -c.y }, { 0, 0, 1, -c.z }, { 0, 0, 0, 1 } });
            tInv = new Mat4x4(new double[,] { { 1, 0, 0, c.x }, { 0, 1, 0, c.y }, { 0, 0, 1, c.z }, { 0, 0, 0, 1 } });

            double d = Math.Sqrt(v.y*v.y + v.z*v.z);
            double cA = v.z/d;
            double sA = v.y/d;

            rX = new Mat4x4(new double[,] { { 1, 0, 0, 0 }, { 0, cA, -sA, 0 }, { 0, sA, cA, 0 }, { 0, 0, 0, 1 } });
            rXInv = new Mat4x4(new double[,] { { 1, 0, 0, 0 }, { 0, cA, sA, 0 }, { 0, -sA, cA, 0 }, { 0, 0, 0, 1 } });

            rY = new Mat4x4(new double[,] { { d, 0, -v.x, 0 }, { 0, 1, 0, 0 }, { v.x, 0, d, 0 }, { 0, 0, 0, 1 } });
            rYInv = new Mat4x4(new double[,] { { d, 0, v.x, 0 }, { 0, 1, 0, 0 }, { -v.x, 0, d, 0 }, { 0, 0, 0, 1 } });

            double cAng = Math.Cos(ang);
            double sAng = Math.Sin(ang);
            rZ = new Mat4x4(new double[,] { { cAng, sAng, 0, 0 }, { -sAng, cAng, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
        }

        public override double Value(Vec3 pos)
        {
            Vec4 pos4 = new Vec4(pos, 1);

            Vec4 posRot4 = tInv*(rXInv*(rYInv*(rZ*(rY*(rX*(t*(pos4)))))));
            Vec3 posRot3 = new Vec3(posRot4.x, posRot4.y, posRot4.z);

            return field.Value(posRot3);
        }
    }
}
