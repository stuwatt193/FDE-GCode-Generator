namespace FDE.Quadtrees
{
    public class QuadTreeRecorder
    {
        List<string> dataSet;
        List<Vec2> surfPointSet;
        List<Vec2> qtPointSet;

        public QuadTreeRecorder()
        {
            dataSet = new List<string>();
            surfPointSet = new List<Vec2>();
            qtPointSet = new List<Vec2>();
        }

        public void AddData(string data)
        {
            dataSet.Add(data);
        }

        public void AddSurfPoint(Vec2 point)
        {
            surfPointSet.Add(point);
        }

        public void AddQtPoint(Vec2 point)
        {
            qtPointSet.Add(point);
        }


        public void PrintDataToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            foreach (string data in dataSet)
            {
                sw.WriteLine(data);
            }
            sw.Close();
        }

        public void PrintSurfPointsToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            foreach (Vec2 pt in surfPointSet)
            {
                sw.WriteLine(pt.ToCsvString());
            }
            sw.Close();
        }
        public void PrintQtPointsToFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            foreach (Vec2 pt in qtPointSet)
            {
                sw.WriteLine(pt.ToCsvString());
            }
            sw.Close();
        }




    }
}
