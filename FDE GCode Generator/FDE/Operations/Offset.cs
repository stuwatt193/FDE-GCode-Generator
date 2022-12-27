namespace FDE.Operations
{
    public class Offset:Field
    {
        Field sdf;
        Field o;

        /// <summary>
        /// Offsets the isosurfaces of the given field by the offset field
        /// To comply with standard convention positive offset moves the surfaces outward         
        /// </summary>
        //public Offset(Field field, Field variableOffset)
        //{
        //    this.sdf = field;
        //    this.o = variableOffset;
        //    this.boundingBox3D = field.boundingBox3D; // Need to be altered;
        //}

        /// <summary>
        /// Offsets the isosurfaces of the given field by a uniform value
        /// To comply with standard convention positive offset moves the surfaces outward         
        /// </summary>
        public Offset(Field field, double UniformOffset)
        {
            this.sdf = field;
            this.o = new UniformField(UniformOffset);
            BoundingBox3D bb = field.boundingBox3D;
            this.boundingBox3D = new BoundingBox3D(bb.c, bb.span.x + UniformOffset, bb.span.y + UniformOffset, bb.span.z + UniformOffset);
        }

        public override double Value(Vec3 pos)
        {
            // To move surface outward, must subtract the offset
            return sdf.Value(pos) - o.Value(pos);
        }
    }
}
