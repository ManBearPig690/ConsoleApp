using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Map
    {
        private bool[,] cells;

        public int Width
        {
            get { return cells.GetUpperBound(0) + 1; }
        }

        public int Height
        {
            get { return cells.GetUpperBound(1) + 1; }
        }

        public bool this[int x, int y]
        {
            get { return cells[x, y]; }
            set { cells[x, y] = value; }
        }

        public Map(int width, int height)
        {
            cells = new bool[width, height];
        }

        public void MarkCellsUnvisited()
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    cells[x, y] = false;
        }

        public void PickRandomCellAndMarkItVisited()
        {
            int locatoinX = new Random().Next(Width - 1);
            int locationY = new Random().Next(Height - 1);
            this[locatoinX, locationY] = true;
        }
    }
}
