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

        public Cell()
        {
            
        }
    }
}
