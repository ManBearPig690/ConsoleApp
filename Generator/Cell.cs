using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Cell
    {
        private bool _visited;
        private SideType _northSide = SideType.Wall;
        private SideType _southSide = SideType.Wall;
        private SideType _westSide = SideType.Wall;
        private SideType _eastSide= SideType.Wall;

        public bool Visited
        {
            get { return _visited; }
            set { _visited = value; }
        }

        public SideType NorthSide
        {
            get { return _northSide; }
            set { _northSide = value; }
        }

        public SideType SouthSide
        {
            get{return _southSide;} 
            set { _southSide = value; }
        }

        public SideType WestSide
        {
            get { return _westSide; }
            set { _westSide = value; }
        }

        public SideType EastSide
        {
            get { return _eastSide; }
            set { _eastSide = value; }
        }

        public bool IsDeadEnd
        {
            get { return WallCount == 3; }
        }

        public int WallCount
        {
            get
            {
                int wallCount = 0;
                if (_northSide == SideType.Wall) wallCount++;
                if (_southSide == SideType.Wall) wallCount++;
                if (_westSide == SideType.Wall) wallCount++;
                if (_eastSide == SideType.Wall) wallCount++;
                return wallCount;

            }
        }

        public Cell()
        {
            
        }

        public DirectionType CalculateDeadEndCorridorDirection()
        {
            if(!IsDeadEnd) throw new InvalidOperationException();

            if(_northSide == SideType.Empty) return DirectionType.North;
            if(_southSide == SideType.Empty) return DirectionType.South;
            if(_westSide == SideType.Empty) return DirectionType.West;
            if(_eastSide == SideType.Empty) return DirectionType.East;

            throw new InvalidOperationException();
        }
    }
}
