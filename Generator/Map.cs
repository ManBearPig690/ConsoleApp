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
        public Cell[,] _cells;
        private readonly List<Point> _visitedCells = new List<Point>();
        private readonly int _changeDirectionModifier;
    #region Properties

        public int Width
        {
            get { return _cells.GetUpperBound(0) + 1; }
        }

        public int Height
        {
            get { return _cells.GetUpperBound(1) + 1; }
        }

        public Cell this[int x, int y]
        {
            get { return _cells[x, y]; }
            set { _cells[x, y] = value; }
        }

        public Cell this[Point location]
        {
            get { return _cells[location.X, location.Y]; }
            set { _cells[location.X, location.Y] = value; }
        }

        public bool AllCellsVisited
        {
            get { return _visitedCells.Count == (Width * Height); }
        }

    #endregion
        

        public Map(int width, int height)
        {
            _cells = new Cell[width, height];
        }

        public Map(int width, int height, int changeDirectionModifier)
        {
            _cells = new Cell[width, height];
            _changeDirectionModifier = changeDirectionModifier;

        }

        public void MarkCellsUnvisited()
        {
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    _cells[x, y] = new Cell {Visited = false};
                    
        }

        public Point PickRandomCellAndMarkItVisited()
        {

            var randomLocation = new Point(new Random().Next(Width - 1), new Random().Next(Height - 1));
            this[randomLocation].Visited = true;
            _visitedCells.Add(randomLocation);
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

        public bool AdjacentCellInDirectionIsVisited(Point location, DirectionType direction)
        {
            if(!HasAdjacentCellInDirection(location, direction))
                throw new InvalidOperationException("No adjacent cell exists for the location adn direction provided.");

            switch (direction)
            {
                case DirectionType.North:
                    return this[location.X, location.Y - 1].Visited;
                case DirectionType.West:
                    return this[location.X - 1, location.Y].Visited;
                case DirectionType.South:
                    return this[location.X, location.Y + 1].Visited;
                case DirectionType.East:
                    return this[location.X + 1, location.Y].Visited;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void FlagCellAsVisited(Point location)
        {
            if (LocationIsOutOfBounds(location)) throw new ArgumentException("Location is outside of Map bounds", "location");
            if (this[location].Visited) throw new ArgumentException("Location is already visisted", "location");

            this[location].Visited = true;
            _visitedCells.Add(location);
        }

        private bool LocationIsOutOfBounds(Point location)
        {
            return ((location.X < 0) || (location.X >= Width) || (location.Y < 0) || (location.Y >= Height));
        }

        public Point GetRandomVisitedCell(Point location)
        {
            if (_visitedCells.Count == 0) throw new InvalidOperationException("there are no visied cells to return.");
            int index = new Random().Next(_visitedCells.Count - 1);

            while (_visitedCells[index] == location)
            {
                index = new Random().Next(_visitedCells.Count - 1);
            }
            return _visitedCells[index];
        }

        public Point CreateCorridor(Point location, DirectionType direction)
        {
            Point target = GetTargetLocation(location, direction);

            switch (direction)
            {
                case DirectionType.North:
                    this[location].NorthSide = SideType.Empty;
                    this[target].SouthSide = SideType.Empty;
                    break;
                case DirectionType.South:
                    this[location].SouthSide = SideType.Empty;
                    this[target].NorthSide = SideType.Empty;
                    break;
                case DirectionType.West:
                    this[location].WestSide = SideType.Empty;
                    this[target].EastSide = SideType.Empty;
                    break;
                case DirectionType.East:
                    this[location].EastSide = SideType.Empty;
                    this[target].WestSide = SideType.Empty;
                    break;
            }

            return target;
        }

        private Point GetTargetLocation(Point location, DirectionType direction)
        {
            if(!HasAdjacentCellInDirection(location, direction)) throw new InvalidOperationException("No adjacent cell exists for the location and direction provided.");

            switch (direction)
            {
                case DirectionType.North:
                    return new Point(location.X, location.Y - 1);
                case DirectionType.South:
                    return new Point(location.X, location.Y + 1);
                case DirectionType.West:
                    return new Point(location.X - 1, location.Y);
                case DirectionType.East:
                    return new Point(location.X + 1, location.Y);
                default:
                    throw new InvalidOperationException("");
            }
        }
    }
}
