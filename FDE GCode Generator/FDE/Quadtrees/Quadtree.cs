namespace FDE.Quadtrees
{
    public abstract class Quadtree
    {
        public Field field;
        public double z;
        public BoundingBox2D bb;
        public int level;
        public int maxLevel;

        public Vec3 tr;
        public Vec3 br;
        public Vec3 bl;
        public Vec3 tl;

        public double distTR;
        public double distBR;
        public double distBL;
        public double distTL;

        public Vec3? gradTR;
        public Vec3? gradBR;
        public Vec3? gradBL;
        public Vec3? gradTL;

        public int inOut;
        public double angTR;
        public double angBR;
        public double angBL;
        public double angTL;


        public abstract void Subdivide();

        public Vec2 SurfPointBetweenPoints(Vec2 pt1, double distPt1, Vec2 pt2, double distPt2)
        {
            double ratio = distPt1 / (distPt1 - distPt2);
            return Vec2.PointBetweenPoints(pt1, pt2, ratio);
        }
    }
}
