using System.Diagnostics.Metrics;
using System.Drawing;

namespace FDE.Quadtrees
{
    public class QuadtreeDev1 : Quadtree
    {
        public string coord;
        public QuadTreeRecorder qtr;

        public QuadtreeDev1(Field _field, double _z, BoundingBox2D _bb, int _level, int _maxLevel, string _coord, QuadTreeRecorder _qtr)
        {
            field = _field;
            z = _z;
            bb = _bb;
            level = _level;
            maxLevel = _maxLevel;
            coord = _coord;
            qtr = _qtr;

            tr = new Vec3(bb.TR(), z);
            br = new Vec3(bb.BR(), z);
            bl = new Vec3(bb.BL(), z);
            tl = new Vec3(bb.TL(), z);

            distTR = field.Value(tr);
            distBR = field.Value(br);
            distBL = field.Value(bl);
            distTL = field.Value(tl);

            inOut = 0;
            if (distTR > 0) { inOut++; } else { inOut--; }
            if (distBR > 0) { inOut++; } else { inOut--; }
            if (distBL > 0) { inOut++; } else { inOut--; }
            if (distTL > 0) { inOut++; } else { inOut--; }

            bool subDiv = false;
            if (Math.Abs(inOut) != 4)
            {
                subDiv = true;
            }
            else
            {
                gradTR = field.Gradient(tr);
                gradBR = field.Gradient(br);
                gradBL = field.Gradient(bl);
                gradTL = field.Gradient(tl);

                angTR = Math.Abs(Vec2.AngleBetweenVectors(gradTR.XY(), new Vec2(1, 1)));
                angBR = Math.Abs(Vec2.AngleBetweenVectors(gradBR.XY(), new Vec2(1, -1)));
                angBL = Math.Abs(Vec2.AngleBetweenVectors(gradBL.XY(), new Vec2(-1, -1)));
                angTL = Math.Abs(Vec2.AngleBetweenVectors(gradTL.XY(), new Vec2(-1, 1)));

                double ang = Math.PI / 2;

                if (angTR < ang && angBR < ang && angBL < ang && angTL < ang)
                {
                    subDiv = true;
                }
            }
            qtr.AddData(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", level, distTR, distBR, distBL, distTL, inOut, angTR, angBR, angBL, angTL, coord, subDiv));

            if (level < maxLevel && subDiv)
            {
                Subdivide();
            }
            else if (level == maxLevel)
            {
                if (subDiv)
                {
                    int signTR = field.Sign(tr);
                    int signBR = field.Sign(br);
                    int signBL = field.Sign(bl);
                    int signTL = field.Sign(tl);

                    if (signTR != signBR) { qtr.AddSurfPoint(SurfPointBetweenPoints(tr.XY(), distTR, br.XY(), distBR)); };
                    if (signBR != signBL) { qtr.AddSurfPoint(SurfPointBetweenPoints(br.XY(), distBR, bl.XY(), distBL)); };
                    if (signBL != signTL) { qtr.AddSurfPoint(SurfPointBetweenPoints(bl.XY(), distBL, tl.XY(), distTL)); };
                    if (signTL != signTR) { qtr.AddSurfPoint(SurfPointBetweenPoints(tl.XY(), distTL, tr.XY(), distTR)); };

                    qtr.AddQtPoint(bb.TR());
                    qtr.AddQtPoint(bb.BR());
                    qtr.AddQtPoint(bb.BL());
                    qtr.AddQtPoint(bb.TL());
                }
            }
        }

        public override void Subdivide()
        {
            level++;

            double delX = bb.span.x / 4;
            double delY = bb.span.x / 4;
            double newW = bb.span.y / 2;
            double newH = bb.span.y / 2;

            Quadtree NE = new QuadtreeDev1(field, z, new BoundingBox2D(bb.c.x + delX, bb.c.y + delY, newW, newH), level, maxLevel, "TR", qtr);
            Quadtree SE = new QuadtreeDev1(field, z, new BoundingBox2D(bb.c.x + delX, bb.c.y - delY, newW, newH), level, maxLevel, "BR", qtr);
            Quadtree SW = new QuadtreeDev1(field, z, new BoundingBox2D(bb.c.x - delX, bb.c.y - delY, newW, newH), level, maxLevel, "BL", qtr);
            Quadtree NW = new QuadtreeDev1(field, z, new BoundingBox2D(bb.c.x - delX, bb.c.y + delY, newW, newH), level, maxLevel, "TL", qtr);
        }
    }
}
