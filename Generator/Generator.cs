using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Generator
    {
        public Map Generate()
        {
            var map = new Map(10, 10);
            map.MarkCellsUnvisited();
            var currentLocation = map.PickRandomCellAndMarkItVisited();

            return map;
        }
    }
}
