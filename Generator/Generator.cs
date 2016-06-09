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

            while (!map.AllCellsVisited)
            {
                var directionPicker = new DirectionPicker();
                var direction = directionPicker.GetNextDirection();

                while (!map.HasAdjacentCellInDirection(currentLocation, direction) ||
                   map.AdjacentCellInDirectionIsVisited(currentLocation, direction))
                {
                    if (directionPicker.HasNextDirection)
                        direction = directionPicker.GetNextDirection();
                    else
                    {
                        currentLocation = map.GetRandomVisitedCell(currentLocation); // get new previously visied location
                        directionPicker = new DirectionPicker();
                        direction = directionPicker.GetNextDirection(); // get a new direction
                    }
                }

                currentLocation = map.CreateCorridor(currentLocation, direction);
                map.FlagCellAsVisited(currentLocation);
            }
            return map;
        }
    }
}
