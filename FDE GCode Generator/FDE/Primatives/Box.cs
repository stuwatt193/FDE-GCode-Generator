namespace FDE.Primatives
{
    public class Box:Field
    {
        public Vec3 size;
        public Vec3 c;

        public Box(Vec3 _size, Vec3 centre)
        {
            this.size = _size;
            this.c = centre;
        }

        public Box(double x, double y, double z, Vec3 centre)
        {
            this.size = new Vec3(x, y, z);
            this.c = centre;
        }

        public override double Value(Vec3 pos)
        {
            pos = pos - c;
            // https://www.youtube.com/watch?v=62-pRVZuS5c&t=3s
            Vec3 halfSize = size / 2;
            Vec3 q = pos.Abs() - halfSize;
            return q.ItemwiseMax(0).Length() + Math.Min(q.Max(), 0.0);
        }       
    }
}
