using FDE;
using FDE.Primatives;
using FDE.Operations;
using System.Text;

Field cyl = new Cylinder(new Vec3(-20,0,0), new Vec3(20, 0, 0), 5);

double[,] c = new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };

Mat4x4 m = new Mat4x4(c);
Vec4 v = new Vec4(1, 2, 3, 4);

Vec4 n = m * v;


WriteToConsole(new Vec3(11, 0, 0));
WriteToConsole(new Vec3(19, 0, 0));
WriteToConsole(new Vec3(21, 0, 0));
WriteToConsole(new Vec3(21, 1, 0));
WriteToConsole(new Vec3(21, 5, 0));
WriteToConsole(new Vec3(21, 6, 0));
WriteToConsole(new Vec3(21, 7, 0));


Console.ReadLine();


void WriteToConsole(Vec3 pos)
{
    Console.WriteLine(pos.ToString());
    Console.WriteLine(cyl.Value(pos));
}
