using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public static class Generator
    {
        public static Map Generate(int width, int height, int changeDirectionModifier)
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

        public static void SparsifyMaze(Map map, int sparsenessModifier)
        {
            // Calculate the number of cells to remove as a percentage
            // of the total number of cells in the map
            int noOfDeadEndCellsToRemove = (int) Math.Ceiling((decimal) sparsenessModifier/100*(map.Width*map.Height));
            IEnumerator<Point> enumerator = map.DeadEndCellLocations.GetEnumerator();

            for (int i = 0; i < noOfDeadEndCellsToRemove; i++)
            {    if (!enumerator.MoveNext())
                {
                    enumerator = map.DeadEndCellLocations.GetEnumerator();
                    if (!enumerator.MoveNext()) break;
                }
                Point point = enumerator.Current;
                //map.CreatWall(point, map[point].CalculateDeadEndCorridorDirection());
            }
        }

    }
}
