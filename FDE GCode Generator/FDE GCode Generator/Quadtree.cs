using FDE;

namespace FDE_GCode_Generator
{
    public abstract class Quadtree
    {
        public int level;
        public int maxLevel;
        public BoundingSquare rect;
        public Field finalShape;

        public Vec2 tr;
        public Vec2 br;
        public Vec2 bl;
        public Vec2 tl;

        public double distTR;
        public double distBR;
        public double distBL;
        public double distTL;

        public Quadtree()
        {

        }
    }
}
