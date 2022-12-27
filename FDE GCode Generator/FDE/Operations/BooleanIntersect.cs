namespace FDE.Operations
{
    public class BooleanIntersect:Field 
    {
        List<Field> sdfList;

        /// <summary>
        /// Gives the boolean intersection of multiple (>2) fields
        /// </summary>
        /// <param name="shapes"></param>
        public BooleanIntersect(List<Field> shapes)
        {
            this.sdfList = shapes;
        }

        /// <summary>
        /// Gives the boolean intersection of two fields
        /// </summary>
        public BooleanIntersect(Field shape1, Field shape2)
        {
            sdfList = new List<Field>();
            sdfList.Add(shape1);
            sdfList.Add(shape2);
        }

        public override double Value(Vec3 pos)
        {
            // Loops through the list of fields and returns the maximum value
            double dist = double.PositiveInfinity;
            for (int i = 0; i < sdfList.Count; i++)
            {
                Field sdf = sdfList[i];
                dist = Math.Max(sdf.Value(pos), dist);
            }
            return dist;
        }
    }
}
