using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDE_GCode_Generator;
using FDE;
using FDE.Primatives;
using FDE.Operations;

namespace FDE_GCode_Generator
{
    public class PathPlanner
    {
        GCodeBuilder gcb;
        MachineParameters mp;
        public double totalPathLength;

        public PathPlanner(GCodeBuilder _gcb, MachineParameters _mp)
        {
            gcb = _gcb;
            mp = _mp;
            totalPathLength = 0;
        }

        public void PlanLayerBorderInfill(Field geom, double z)
        {
            double surfOffset = -mp.borderLineWidth / 2; // Offsets outwards so that, in loop, offset can be done before contour. Makes code easier for infill.
            for (int i = 0; i < mp.borderCount; i++)
            {
                surfOffset += mp.borderLineWidth;
                PlanContour(new Offset(geom, -surfOffset), z);                
            }

            surfOffset += mp.borderInfillOffset;
            PlanMeanderInfill(new Offset(geom, -surfOffset), z, 45);
        }
        public void PlanLayerInfillBorder(Field geom, double z)
        {
            double surfOffset = (mp.borderCount-0.5) * mp.borderLineWidth + mp.borderInfillOffset; 
            PlanMeanderInfill(new Offset(geom, -surfOffset), z, 90);

            //surfOffset = -mp.borderLineWidth / 2;
            //for (int i = 0; i < mp.borderCount; i++)
            //{
            //    surfOffset += mp.borderLineWidth;
            //    PlanContour(new Offset(geom, -surfOffset), z);
            //}
        }

        public void PlanContour(Field geom, double z)
        {
            // ***** Build Parameters ****
            double stepSize = 1;

            Vec3 _B = new Vec3(0, 0, 1); // Build Direction Vector
            Vec3 _T; // Surface Tangent Vector
            double d; // Distance to Surface
            Vec3 _G; // Field Gradient Vector
            Vec3 _Gd; // Vector to surface 
            Vec3 _M; // Movement Vector
            Vec3 pos;
            Vec3 samplePos;
            Vec3 contourStartPos;

            Vec3 startPos = new Vec3(0.0, 0.0, z);

            // Get first position on surface
            d = geom.Value(startPos); // Distance to Surface
            _G = geom.Gradient(startPos); // Field Gradient Vector
            _Gd = -1 * (_G * d); // Assumes gradient is 1
            pos = startPos + _Gd;
            contourStartPos = pos;
            gcb.AddG0WithRetraction(pos, mp);

            bool backToStart = false;
            int moveCount = 0;
            while (!backToStart)
            {
                _T = Vec3.CrossProduct(_B, _G).Normalise();
                samplePos = pos + _T * stepSize;
                d = geom.Value(samplePos);
                _G = geom.Gradient(samplePos);
                _Gd = -1 * (_G * d);

                Vec3 newPos = samplePos + _Gd;

                Vec3 vecToStart = newPos - contourStartPos;
                if (vecToStart.Length() > stepSize * 0.5 || moveCount < 1)
                {
                    _M = newPos - pos;
                    pos = newPos;
                }
                else
                {
                    _M = vecToStart;
                    backToStart = true;
                    pos = contourStartPos;
                }
                //Console.WriteLine(pos);
                gcb.AddG1(pos, _M.Length() * mp.borderExtrusionRate, mp);
                totalPathLength += _M.Length();
                moveCount++;
            }
        }

        public void PlanMeanderInfill(Field geom, double z, double orientationAngleDeg)
        {
            BoundingBox2D bb = new BoundingBox2D(geom.boundingBox3D,z);

            Vec3 meanderStartPt = new Vec3(bb.BL(), z);
            Vec3 meanderEndPt = new Vec3(bb.BR(), z);
            Vec3 progressionStartPt = meanderStartPt;
            Vec3 progressionEndPt = new Vec3(bb.TL(), z);

            Vec3 meanderStartRotPt = Vec3.PointBetweenPoints(meanderStartPt, meanderEndPt);
            Vec3 meanderEndRotPt = Vec3.PointBetweenPoints(meanderEndPt, new Vec3(bb.TR(), z));
            Vec3 progessionEndRotPt = Vec3.PointBetweenPoints(progressionStartPt, progressionEndPt);

            meanderStartPt = meanderStartPt.RotateAroundPointInZ(meanderStartRotPt, orientationAngleDeg);
            meanderEndPt = meanderEndPt.RotateAroundPointInZ(meanderEndRotPt, orientationAngleDeg);
            progressionStartPt = meanderStartPt;
            progressionEndPt = progressionEndPt.RotateAroundPointInZ(progessionEndRotPt, orientationAngleDeg);

            Vec3 meanderDirection = new Vec3(meanderStartPt, meanderEndPt);
            double meanderLimit = meanderDirection.Length();
            meanderDirection = meanderDirection.Normalise();
                
            Vec3 progressionDirection = new Vec3(progressionStartPt, progressionEndPt);
            double progressionLimit = progressionDirection.Length();
            progressionDirection = progressionDirection.Normalise();


            gcb.AddG0WithRetraction(meanderStartPt, mp);
            int count = 0;
            while (Vec3.DistanceBetweenPoints(meanderStartPt, progressionStartPt) < progressionLimit)
            {
                if (count%2 ==0)
                {
                    PlanInfillLine(geom, meanderStartPt, meanderDirection, meanderLimit);
                }
                else
                { 
                    PlanInfillLine(geom, meanderEndPt, meanderDirection.Inv(), meanderLimit); 
                }
                
                meanderStartPt = meanderStartPt + progressionDirection * mp.infillLineWidth;
                meanderEndPt = meanderEndPt + progressionDirection * mp.infillLineWidth;
                count++;
            }
        }

        public void PlanInfillLine(Field geom, Vec3 startPt, Vec3 dir, double travelLimit)
        {
            //Console.WriteLine("Next Line");
            double minStep = 0.1;
            double d;
            Vec3 _M; // Movement Vector

            Vec3 pos = startPt;
            Vec3 trackStartPos = new Vec3();
            Vec3 trackEndPos = new Vec3();
            bool depositing = false;
            while (Vec3.DistanceBetweenPoints(pos, startPt) < travelLimit)
            {
                d = geom.Value(pos);
                //Console.WriteLine("   pos: {0} : {1}", pos,d);

                if (d < 0 && !depositing)
                {
                    //Console.WriteLine("Start Depositing");
                    trackStartPos = pos;
                    depositing = true;
                    gcb.AddG0(pos, mp);
                    d = minStep;
                }
                else if (d > 0 && depositing)
                {
                    //Console.WriteLine("Stop Depositing");
                    trackEndPos = pos;
                    depositing = false;
                    _M = trackEndPos - trackStartPos;
                    gcb.AddG1(trackEndPos, _M.Length() * mp.infillExtrusionRate, mp);
                    totalPathLength += _M.Length();
                    d = minStep;
                }
                else if (Math.Abs(d)<minStep)
                {
                    d = minStep;
                }
                
                pos = pos + dir * Math.Abs(d);
            }   
        }
    }
}
