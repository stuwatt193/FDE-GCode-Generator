using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDE;
using FDE.Primatives;
using FDE.Operations;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FDE_GCode_Generator
{
    public class GCodeBuilder
    {
        List<string> gCodeLines;
        List<Vec3> gPoints = new List<Vec3>();

        public GCodeBuilder()
        {
            gCodeLines = new List<string>();
        }

        public void AddG0(Vec3 newPos, MachineParameters mp)
        {
            StringBuilder sb = new StringBuilder();
            // G0 F6000 X106.813 Y106.715
            sb.Append("G0");
            sb.AppendFormat(" F{0:0.#####}", mp.rapidSpeed);
            sb.AppendFormat(" X{0:0.#####}", newPos.x);
            sb.AppendFormat(" Y{0:0.#####}", newPos.y);
            sb.AppendFormat(" Z{0:0.#####}", newPos.z);
            gCodeLines.Add(sb.ToString());
            gPoints.Add(newPos);
        }

        public void AddG0(double x, double y, double z, MachineParameters mp)
        {
            Vec3 newPos = new Vec3(x,y,z);
            AddG0(newPos, mp);
        }

        public void AddG0WithRetraction(Vec3 newPos, MachineParameters mp)
        {
            double retractedZ = newPos.z + mp.retractionHeight;

            gCodeLines.Add(string.Format("G1 F{0} E-{1}", mp.filamentRetractionSpeed, mp.filamentRetractionLength)); // Retract Filament
            gCodeLines.Add(string.Format("G0 F{0} Z{1}", mp.retractionSpeed, retractedZ)); // Retract Head
            AddG0(new Vec3(newPos.XY(), retractedZ), mp);
            gCodeLines.Add(string.Format("G0 F{0} Z{1}", mp.retractionSpeed, newPos.z));  // Unretract Head
            gCodeLines.Add(string.Format("G1 F{0} E{1}", mp.filamentRetractionSpeed, mp.filamentRetractionLength)); // Unretract Filament
            gPoints.Add(newPos);
        }

        public void AddG1(Vec3 newPos, double e, MachineParameters mp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("G1");
            sb.AppendFormat(" F{0:0.#####}", mp.printSpeed);
            sb.AppendFormat(" X{0:0.#####}", newPos.x);
            sb.AppendFormat(" Y{0:0.#####}", newPos.y);
            sb.AppendFormat(" Z{0:0.#####}", newPos.z);
            sb.AppendFormat(" E{0:0.#####}", e);
            gCodeLines.Add(sb.ToString());
            gPoints.Add(newPos);
        }

        public void AddG1(double x, double y, double z, double e, MachineParameters mp)
        {
            Vec3 newPos = new Vec3(x, y, z);
            AddG1(newPos, e, mp);
        }

       

        public void AddComment(string comment)
        {
            gCodeLines.Add(";"+comment);
        }

        public List<string> CompileGCode()
        {
            List<string> gcode = new List<string>();
            gcode.AddRange(GetGCodeHeader());
            gcode.AddRange(gCodeLines);
            gcode.AddRange(GetGCodeFooter());
            return gcode;
        }

        public void PrintGCodeLines()
        {
            foreach (string line in gCodeLines)
            {
                Console.WriteLine(line);
            }
        }

        public void ComplileAndSaveGcode(string filepath)
        {
            List<string> fullGCode = new List<string>();
            fullGCode.AddRange(GetGCodeHeader());
            fullGCode.AddRange(gCodeLines);
            fullGCode.AddRange(GetGCodeFooter());

            StreamWriter sw = new StreamWriter(filepath);

            foreach (string line in fullGCode)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void WritePoints()
        {
            StreamWriter sw = new StreamWriter(@"F:\OneDrive\FDE\Cyl_D10_H10_FDE-Points.csv");

            foreach (Vec3 pt in gPoints)
            {
                sw.WriteLine(pt.ToCsvString());
            }
            sw.Close();
        }

        List<string> GetGCodeHeader()
        {
            List<string> header = new List<string>();
            header.Add("***** Set Temps *****");
            header.Add("M140 S60");
            header.Add("M104 S200");
            header.Add("M105");
            header.Add("M190 S60");
            header.Add("M109 S200");
            header.Add("M105");
            
            header.Add("G90 ;Absolute position mode");
            header.Add("");
            header.Add("***** Apply Settings *****");
            header.Add("M201 X500.00 Y500.00 Z100.00 E5000.00 ;Setup machine max acceleration");
            header.Add("M203 X500.00 Y500.00 Z10.00 E50.00 ;Setup machine max feedrate");
            header.Add("M204 P500.00 R1000.00 T500.00 ;Setup Print/Retract/Travel acceleration");
            header.Add("M205 X8.00 Y8.00 Z0.40 E5.00 ;Setup Jerk");
            header.Add("M220 S100 ;Reset Feedrate");
            header.Add("M221 S100 ;Reset Flowrate");
            header.Add("");
            header.Add("***** Test Line *****");
            header.Add("G28 ;Home");
            header.Add("G92 E0; Reset Extruder");
            header.Add("M83 ;Relative extrusion mode");
            header.Add("G1 Z2.0 F3000 ;Move Z Axis up");
            header.Add("G1 X10.1 Y20 Z0.28 F5000.0 ;Move to start position");
            header.Add("G1 X10.1 Y200.0 Z0.28 F1500.0 E15 ;Draw the first line");
            header.Add("G1 X10.4 Y200.0 Z0.28 F5000.0 ;Move to side a little");
            header.Add("G1 X10.4 Y20 Z0.28 F1500.0 E15 ;Draw the second line");
            header.Add("G1 Z2.0 F3000 ;Move Z Axis up");
            header.Add("");
            header.Add("***** Reset *****");
            header.Add("G92 E0; Reset Extruder");
            header.Add("M83 ;Relative extrusion mode");
            header.Add("");
            header.Add("***** Start Print *****");
            return header;
        }

        List<string> GetGCodeFooter()
        {
            List<string> footer = new List<string>();
            footer.Add("");
            footer.Add("***** End of Print *****");
            footer.Add("G91 ;Relative positioning");
            footer.Add("G1 E-2 F2700 ;Retract a bit");
            footer.Add("G1 E-2 Z0.2 F2400 ;Retract and raise Z");
            footer.Add("G1 X5 Y5 F3000 ;Wipe out");
            footer.Add("G1 Z10 ;Raise Z more");
            footer.Add("G90 ;Absolute positioning");
            footer.Add("");
            footer.Add("G1 X0 Y{machine_depth} ;Present print");
            footer.Add("M106 S0 ;Turn-off fan");
            footer.Add("M104 S0 ;Turn-off hotend");
            footer.Add("M140 S0 ;Turn-off bed");
            footer.Add("");
            footer.Add("M84 X Y E ;Disable all steppers but Z");
            return footer;
        }

    }
}
