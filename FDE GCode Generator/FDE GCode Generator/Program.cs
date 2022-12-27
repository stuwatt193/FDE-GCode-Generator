using FDE;
using FDE.Primatives;
using FDE.Operations;
using FDE_GCode_Generator;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using FDE.Quadtrees;
using System.Diagnostics;

internal class Program
{
    static Field DefinedGeometry()
    {
        Field geom;

        double x = 125;
        double y = 125;
        double r = 30;
        Field cyl = new Cylinder(new Vec3(x, y, -500), new Vec3(x, y, 500), r);
        Field rotCyl = new Rotate(cyl, new Vec3(125, 125, 0), Vec3.yAxis(), 45);

        //Field c = new Cylinder(new Vec3(x, y, -500), new Vec3(x, y, 500), r);
        //Field f = c;
        //for (int i = 0; i < 500; i++)
        //{
        //    r = r - 0.1;
        //    c = new Cylinder(new Vec3(x, y, -500), new Vec3(x, y, 500), r);
        //    f = new BooleanUnion(f, c);
        //}

        BoundingBox3D buildVol = new BoundingBox3D(new Vec3(125, 125, 150), 250, 250, 300);
        BoundingBox2D sliceVol = new BoundingBox2D(buildVol, 0.2);
        Field slice = new PlanarSliceInZ(rotCyl, 0.2, sliceVol, 0.5);


        geom = slice;
        return geom;
    }

    private static void Main(string[] args)
    {
        Stopwatch stpw = new Stopwatch();
        MachineParameters mParams = new MachineParameters();

        double z = mParams.initialLayerHeight;
               

        stpw.Start();
        Field geom = DefinedGeometry();
        stpw.Stop();
        Console.WriteLine("Time to create field: {0} ms",stpw.ElapsedMilliseconds);
       

        GCodeBuilder gcb = new GCodeBuilder();
        
        PathPlanner pathPlanner = new PathPlanner(gcb, mParams);
        stpw.Restart();
        pathPlanner.PlanLayerBorderInfill(geom, z);
        z += mParams.layerHeight;
        //pathPlanner.PlanLayerInfillBorder(geom, z);
        pathPlanner.PlanLayerBorderInfill(geom, z);
        stpw.Stop();
        Console.WriteLine("Time to plan build: {0} ms", stpw.ElapsedMilliseconds);
        Console.WriteLine("Calc speed: {0} mm/s", pathPlanner.totalPathLength/(stpw.ElapsedMilliseconds/1000));
        Console.WriteLine("Print speed: {0} mm/s", mParams.printSpeed/60);

        stpw.Restart();
        gcb.ComplileAndSaveGcode(@"F:\OneDrive\FDE\Cyl_D10_H10_FDE_QT2.gcode");
        stpw.Stop();
        Console.WriteLine("Time to write gcode file: {0} ms", stpw.ElapsedMilliseconds);


        // ***** QUADTREE DEVELOPMENT *****
        //QuadTreeRecorder qtr = new QuadTreeRecorder();
        //Quadtree qt = new QuadtreeDev1(geom, z, sliceVol, 0, 5, "TOP",qtr);
        //qtr.PrintDataToFile(@"F:\OneDrive\FDE\QT_DATA1.csv");
        //qtr.PrintSurfPointsToFile(@"F:\OneDrive\FDE\QT_SURFPOINTS1.csv");
        //qtr.PrintQtPointsToFile(@"F:\OneDrive\FDE\QT_QTPOINTS1.csv");
    }

    
}