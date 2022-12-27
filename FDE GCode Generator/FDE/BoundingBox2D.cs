
using System.Linq.Expressions;

namespace FDE
{    
    public class BoundingBox2D
    {
        public Vec2 c;
        public Vec2 span;

        public BoundingBox2D(Vec2 centre, double width, double height)
        {
            c = centre;
            span = new Vec2(width, height);
        }

        public BoundingBox2D(double x, double y, double width, double height)
        {
            Vec2 centre = new Vec2(x,y);
            c = centre;
            span = new Vec2(width, height);
        }

        public BoundingBox2D(Vec2 minPoint, Vec2 maxPoint)
        {
            Vec2 spanSigned = maxPoint - minPoint;
            span = spanSigned.Abs();

            c = minPoint + span / 2;
        }

        public BoundingBox2D(BoundingBox3D bb3D, double z)
        {
            Vec2 minPoint = new Vec2(bb3D.minPt.x, bb3D.minPt.y);
            Vec2 maxPoint = new Vec2(bb3D.maxPt.x, bb3D.maxPt.y);
            Vec2 spanSigned = maxPoint - minPoint;
            span = spanSigned.Abs();

            c = minPoint + span / 2;
        }

        public bool withinBounds(Vec3 pt)
        {
            Vec2 tr = TR();
            Vec2 br = BR();
            Vec2 bl = BL();
            Vec2 tl = TL();
            return tl.x <= pt.x && pt.x <= br.x && bl.y <= pt.y && pt.y <= tl.y;
        }

        public Vec2 TR()
        {
            return c + new Vec2(span.x / 2, span.y / 2);
        }

        public Vec2 BR()
        {
            return c + new Vec2(span.x / 2, -span.y / 2);
        }

        public Vec2 BL()
        {
            return c + new Vec2(-span.x / 2, -span.y / 2);
        }

        public Vec2 TL()
        {
            return c + new Vec2(-span.x / 2, span.y / 2);
        }
    }
}
