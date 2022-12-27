using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE_GCode_Generator
{    public enum InfillType
    {
        Meander,
        Triangles
    }
        public enum BedAdhesionType
    {

        none,
        skirt,
        brim
    }

    public class MachineParameters
    {
        //MACHINE PHYSICALS
        public double nozzleDiameter;
        public double filamentDiameter;

        // QUALITY
        public double layerHeight;

        // BORDERS
        public int borderCount;
        public double borderLineWidth;
        public double borderExtrusionRate { get; private set; }
        // INFILL
        public InfillType infillType;
        public double infillLineWidth;
        public double infillExtrusionRate { get; private set; }
        public double borderInfillOffset;

        // BED ADHESION
        public double initialLayerHeight;
        public BedAdhesionType bedAdhesionType;
        public double bedAdhesionWidth;

        // TEMPERTURES
        public double bedTemp;
        public double materialTemp;

        // MOVEMENTS
        public double printSpeed;
        public double rapidSpeed;
        public double retractionSpeed;
        public double filamentRetractionSpeed;
        public double retractionHeight;
        public double filamentRetractionLength;

        public MachineParameters() 
        {
            //MACHINE PHYSICALS
            nozzleDiameter = 0.4;
            filamentDiameter = 1.75;

            // QUALITY
            layerHeight = 0.2;

            // BORDERS
            borderCount = 4;
            borderLineWidth = 0.4;
            borderExtrusionRate = CalculateExtrusionRate(borderLineWidth, layerHeight);

            // INFILL
            infillType = InfillType.meander;
            infillLineWidth = 0.4;
            infillExtrusionRate = CalculateExtrusionRate(infillLineWidth, layerHeight);
            borderInfillOffset = infillLineWidth * 0.7;

            // BED ADHESION
            initialLayerHeight = 0.2;
            bedAdhesionType = BedAdhesionType.none;
            bedAdhesionWidth = 8;

            // TEMPERTURES
            bedTemp = 50;
            materialTemp = 200;

            // MOVEMENTS
            printSpeed = 1200;
            rapidSpeed = 6000;
            retractionSpeed = 3000;
            filamentRetractionSpeed = 2700;
            retractionHeight = 2;
            filamentRetractionLength = 5;
        }  
        
        double CalculateExtrusionRate(double lineWidth, double lineHeight)
        {
           return lineWidth * lineHeight / (Math.PI * filamentDiameter * filamentDiameter / 4);
        }
    }
}
