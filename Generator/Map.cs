using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Remoting.Channels;

namespace Generator
{
    public class Map
    {
        private bool[,] _cells;

        public int Width
        {
            get { return _cells.GetUpperBound(0) + 1; }
        }

        public int Height
        {
            get { return _cells.GetUpperBound(1) + 1; }
        }

        public bool this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }

        public bool this[Point location]
        {
            get { return _cells[location.X, location.Y]; }
            set { _cells[location.X, location.Y] = value; }
        }

        public Map(int width, int height)
        {
            _cells = new bool[width, height];
        }

        public void MarkCellsUnvisited()
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    _cells[x, y] = false;
        }

        public Point PickRandomCellAndMarkItVisited()
        {

            var randomLocation = new Point(new Random().Next(Width - 1), new Random().Next(Height - 1));
            this[randomLocation] = true;
            return randomLocation;
        }

        public bool HasAdjacentCellInDirection(Point location, DirectionType direction)
        {
            // Check if out of bounds
            if ((location.X < 0) || (location.X >= Width) || (location.Y < 0) || (location.Y >= Width))
                return false;

            // check if there is an adjecent cell in direction
            switch (direction)
            {
                case DirectionType.North:
                    return location.Y > 0;
                case DirectionType.South:
                    return location.Y < (Height - 1);
                case DirectionType.West:
                    return location.X > 0;
                case DirectionType.East:
                    return location.X < (Width - 1);
                default:
                    return false;
            }

        }
    }
}
