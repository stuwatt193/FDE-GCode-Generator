
namespace FDE.Operations
{
    public class Thicken:Field
    {
        Field sdf;
        Field t;

        /// <summary>
        /// Thickens the field around the zero isoSurface   
        /// </summary>
        public Thicken(Field field, Field variableThickness)
        {
            this.sdf = field;
            this.t = variableThickness;
        }

        public Thicken(Field field, double UniformThickness)
        {
            this.sdf = field;
            this.t = new UniformField(UniformThickness);
        }

        public override double Value(Vec3 pos)
        {
            // taking the absolute value gives an unsigned distance field (UDF),
            // which can then be offset by half the thickness to give the output field
            return Math.Abs(sdf.Value(pos)) - t.Value(pos)/2;
        }
    }
}
