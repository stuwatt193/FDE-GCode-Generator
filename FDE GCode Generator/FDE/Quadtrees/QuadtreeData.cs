using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDE.Quadtrees
{
    public class QuadtreeData
    {
        public List<Vec2> surfPts;

        public QuadtreeData()
        {
            surfPts= new List<Vec2>();
        }

        public void RemoveDuplicatesFromSurfPoints()
        {
            List<Vec2> temp = new List<Vec2>(surfPts);
            List<Vec2> newPts = new List<Vec2>();

            newPts.Add(surfPts[0]);
            Vec2 pt1, pt2;
            for (int i = 1; i < surfPts.Count; i++)
            {
                bool addPt = true;
                pt1 = surfPts[i];
                for (int j = 0; j < newPts.Count; j++)
                {
                    pt2= newPts[j];
                    if (pt1.x == pt2.x && pt1.y == pt2.y)
                    {
                        addPt = false;
                    }
                }

                if (addPt) { newPts.Add(pt1); }
            }
            surfPts = newPts;
        }
    }
}
