using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDE.Quadtrees;
namespace FDE
{
    public class PlanarSliceInZ: Field
    {
        public Field originalField;
        public Quadtree qt;
        public QuadtreeData qtd;

        public PlanarSliceInZ(Field _field, double _z, BoundingBox2D sliceVol, double resolution) 
        {
            this.originalField = _field;
            this.qtd= new QuadtreeData();

            int levels = (int)Math.Ceiling(Math.Log2(sliceVol.span.x/resolution));
            //Console.WriteLine("{0} levels for {1}mm Resolution", levels, resolution);
            this.qt = new QuadtreeV1(originalField, _z, sliceVol, 0, levels, "Top", qtd);
            qtd.RemoveDuplicatesFromSurfPoints();
            DetermineBoundingBox();
        }

        public void DetermineBoundingBox()
        {
            Vec2 pt = qtd.surfPts[0];
            double minX = pt.x, minY = pt.y;
            double maxX = pt.x, maxY = pt.y;

            for (int i = 1; i < qtd.surfPts.Count; i++)
            {
                pt = qtd.surfPts[i];

                if (pt.x < minX) { minX = pt.x; }
                else if (pt.x > maxX) { maxX = pt.x; }

                if (pt.y < minY) { minY = pt.y; }
                else if (pt.y > maxY) { maxY = pt.y; }
            }

            boundingBox3D = new BoundingBox3D(new Vec3(minX,minY,-10000000), new Vec3(maxX, maxY,10000000));
        }

        public override double Value(Vec3 pos)
        {            
            Vec2 pt;
            double d;            
            Vec2 pt1 = new Vec2(), pt2 = new Vec2();
            double d1 = double.PositiveInfinity, d2 = double.PositiveInfinity;

            for (int i = 0; i < qtd.surfPts.Count; i++)
            {
                pt= qtd.surfPts[i];
                d = Vec2.DistanceBetweenPoints(pos.XY(), pt);

                if (d<d1)
                {
                    d2 = d1;
                    d1 = d;

                    pt2 = pt1;
                    pt1 = pt;
                }
                else if(d<d2)
                {
                    d2 = d;
                    pt2 = pt;
                }                
            }


            Vec2 closestPt;
            if (Vec2.VectorsIdentical(pt1,pt2))
            {
                closestPt = pt1;
            }
            else
            {
                double d12 = Vec2.DistanceBetweenPoints(pt1, pt2);
                double intRatio = (d12 * d12 + d1 * d1 - d2 * d2) / (2 * d12 * d12); // derived using triangle equations https://www.mathsisfun.com/algebra/trig-solving-sss-triangles.html
                if (intRatio > 1) { intRatio = 1; }
                else if (intRatio < 0) { intRatio = 0; }
                Vec2 v12 = pt2 - pt1;
                closestPt = pt1 + intRatio * v12;
            }           

            
            return originalField.Sign(pos) * Vec2.DistanceBetweenPoints(closestPt, pos.XY());
        }
    }
}
