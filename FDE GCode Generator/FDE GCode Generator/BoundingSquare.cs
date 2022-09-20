using FDE;

namespace FDE_GCode_Generator
{    
    public class BoundingSquare
    {
        public Vec2 c;
        public double w, h;
        public Vec2 tr, br, bl, tl;

        public BoundingSquare(Vec2 centre, double width, double height)
        {
            c = centre;
            w = width;
            h = height;

            tr = c+new Vec2(w/2,h/2);
            br = c+new Vec2(w/2,-h/2);
            bl = c+new Vec2(-w/2,-h/2);
            tl = c+new Vec2(-w/2, h/2);
        }
    }
}
