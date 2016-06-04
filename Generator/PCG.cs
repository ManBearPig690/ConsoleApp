using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class PCG
    {
        public string[,] PcGrid;
        public int PcGridWidth;
        public int PcGridHeight;
        public int MinRoomNum;

        public void UpdateParam(int width, int height)
        {
            PcGridWidth = width;
            PcGridHeight = height;
            MinRoomNum = (PcGridHeight*PcGridHeight)/100;
        }

        public void GeneratePcg(string[,] g)
        {
            PcGrid = g;
        }

        public bool Bounded(int x, int y)
        {
            if (x < 0 || x >= PcGridWidth || y < 0 || y >= PcGridHeight) return false;
            return true;
        }

        bool Blocked(int x, int y, string type)
        {
            return Bounded(x, y) && PcGrid[x, y] == type;
        }
    }
}
