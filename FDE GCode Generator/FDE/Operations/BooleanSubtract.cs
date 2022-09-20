using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Operations
{
    public class BooleanSubtract:Field
    {
        Field sdfPrimary;
        List<Field> sdfSubList;

        /// <summary>
        /// Gives the boolean subtraction of multiple fields from the primary field
        /// </summary>
        /// <param name="primaryShape"></param>
        /// <param name="subtractionShapes"></param>
        public BooleanSubtract(Field primaryShape, List<Field> subtractionShapes)
        {
            this.sdfPrimary = primaryShape;
            this.sdfSubList = subtractionShapes;
        }

        /// <summary>
        /// Gives the boolean subtraction of a single field from the primary field
        /// </summary>
        /// <param name="primaryShape"></param>
        /// <param name="subtractionShape"></param>
        public BooleanSubtract(Field primaryShape, Field subtractionShape)
        {
            sdfPrimary = primaryShape;
            List<Field> subtractionShapes = new List<Field>();
            subtractionShapes.Add(subtractionShape);
            sdfSubList = subtractionShapes;
        }

        public override double Value(Vec3 pos)
        {
            // Loops through the subtraction fields and unions them to create a single field to subtract
            double subDist = double.PositiveInfinity;
            for (int i = 0; i < sdfSubList.Count; i++)
            {
                Field s = sdfSubList[i];
                subDist = Math.Min(s.Value(pos), subDist);
            }

            // takes the boolean intersection of the primary field and the inverted subtraction field
            return Math.Max(sdfPrimary.Value(pos), - subDist);
        }
    }
}
