using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Generator
    {
        public Map Generate(int width, int height, int changeDirectionModifier)
        {
            var map = new Map(width, height);
            map.MarkCellsUnvisited();
            var currentLocation = map.PickRandomCellAndMarkItVisited();
            var previousDirection = DirectionType.North;

            while (!map.AllCellsVisited)
            {
                var directionPicker = new DirectionPicker(previousDirection, changeDirectionModifier);
                var direction = directionPicker.GetNextDirection();

                while (!map.HasAdjacentCellInDirection(currentLocation, direction) ||
                   map.AdjacentCellInDirectionIsVisited(currentLocation, direction))
                {
                    if (directionPicker.HasNextDirection)
                        direction = directionPicker.GetNextDirection();
                    else
                    {
                        currentLocation = map.GetRandomVisitedCell(currentLocation); // get new previously visied location
                        directionPicker = new DirectionPicker(previousDirection, changeDirectionModifier);
                        direction = directionPicker.GetNextDirection(); // get a new direction
                    }
                }

                currentLocation = map.CreateCorridor(currentLocation, direction);
                map.FlagCellAsVisited(currentLocation);
                previousDirection = direction;
            }
            return map;
        }
    }
}
