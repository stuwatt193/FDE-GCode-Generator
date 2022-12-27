using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Quadtrees
{
    internal class QuadtreeV1: Quadtree
    {
        public string coord;
        QuadtreeData qtd;

        public QuadtreeV1(Field _field, double _z, BoundingBox2D _bb, int _level, int _maxLevel, string _coord, QuadtreeData _qtd)
        {
            field = _field;
            z = _z;
            bb = _bb;
            level = _level;
            maxLevel = _maxLevel;
            coord = _coord;
            qtd = _qtd;

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

                    if (signTR != signBR) { qtd.surfPts.Add(SurfPointBetweenPoints(tr.XY(), distTR, br.XY(), distBR)); };
                    if (signBR != signBL) { qtd.surfPts.Add(SurfPointBetweenPoints(br.XY(), distBR, bl.XY(), distBL)); };
                    if (signBL != signTL) { qtd.surfPts.Add(SurfPointBetweenPoints(bl.XY(), distBL, tl.XY(), distTL)); };
                    if (signTL != signTR) { qtd.surfPts.Add(SurfPointBetweenPoints(tl.XY(), distTL, tr.XY(), distTR)); };
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

            Quadtree NE = new QuadtreeV1(field, z, new BoundingBox2D(bb.c.x + delX, bb.c.y + delY, newW, newH), level, maxLevel, "TR", qtd);
            Quadtree SE = new QuadtreeV1(field, z, new BoundingBox2D(bb.c.x + delX, bb.c.y - delY, newW, newH), level, maxLevel, "BR", qtd);
            Quadtree SW = new QuadtreeV1(field, z, new BoundingBox2D(bb.c.x - delX, bb.c.y - delY, newW, newH), level, maxLevel, "BL", qtd);
            Quadtree NW = new QuadtreeV1(field, z, new BoundingBox2D(bb.c.x - delX, bb.c.y + delY, newW, newH), level, maxLevel, "TL", qtd);
        }
    }
}
