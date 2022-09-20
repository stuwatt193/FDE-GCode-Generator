namespace FDE.Primatives
{
    internal class Sphere:Field
    {
        Vec3 c;
        double r;

        public Sphere(Vec3 center, double radius)
        {
            this.c = center;
            this.r = radius;
        }

        public override double Value(Vec3 pos)
        {
            Vec3 temp = c - pos;
            return temp.Length() - r;
        }
    }
}
