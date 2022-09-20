using FDE;

namespace FDE_GCode_Generator
{
    public class Quadtree1:Quadtree
    {       
        Vec3 gradTR;
        Vec3 gradBR;
        Vec3 gradBL;
        Vec3 gradTL;

        int inOut;
        int inOutGreater;
        double angTR;
        double angBR;
        double angBL;
        double angTL;

        public Quadtree1()
        { 
        }
    }
}
