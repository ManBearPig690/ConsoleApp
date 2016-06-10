using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Drawing;

namespace GeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MarkCellsUnVisited()
        {
            var map = new Map(10, 10);
            map.MarkCellsUnvisited();

            for(var x = 0; x < map.Width; x++)
            {
                for(var y = 0; y < map.Height; y++)
                {
                    Assert.IsFalse(map[x, y].Visited);
                }
            }
        }

        [TestMethod]
        public void TestPickRandomCellAndMarkItVisited()
        {
            var map = new Map(10, 10);
            map.MarkCellsUnvisited();
            map.PickRandomCellAndMarkItVisited();

            var visistedCellCount = 0;
            for(var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    if (map[x, y].Visited)
                        visistedCellCount++;
                }
            }

            Assert.IsTrue(visistedCellCount == 1);
        }

        [TestMethod]
        public void TestGeneratorInstantion()
        {
            var generator = new Generator.Generator();

            var map = generator.Generate(10, 10, 100);

            var visitedCellCount = 0;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    if (map[x, y].Visited)
                        visitedCellCount++;
                    Console.WriteLine("{0}, {1}: Visited: {2}",x, y, map[x,y].Visited);
                }
            }
            
            Assert.IsTrue(visitedCellCount == (map.Width * map.Height)); // will be 100 becuase visistedCellCount returns 
        }

        [TestMethod]
        public void TestMapHasAdjacentCellInDirection()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();
            Assert.IsTrue(map.HasAdjacentCellInDirection(new Point(1, 1), DirectionType.North));
            Assert.IsTrue(map.HasAdjacentCellInDirection(new Point(1, 1), DirectionType.West));
            Assert.IsTrue(map.HasAdjacentCellInDirection(new Point(map.Width - 2, map.Height - 2), DirectionType.South));
            Assert.IsTrue(map.HasAdjacentCellInDirection(new Point(map.Width - 2, map.Height - 2), DirectionType.East));
        }

        [TestMethod]
        public void TestMapHasNoAdjacentCellsOnBoundries()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();
            Assert.IsFalse(map.HasAdjacentCellInDirection(new Point(0, 0), DirectionType.North));
            Assert.IsFalse(map.HasAdjacentCellInDirection(new Point(0, 0), DirectionType.West));
            Assert.IsFalse(map.HasAdjacentCellInDirection(new Point(map.Width - 1, map.Height - 1), DirectionType.South));
            Assert.IsFalse(map.HasAdjacentCellInDirection(new Point(map.Width - 1, map.Height - 1), DirectionType.East));
        }

        [TestMethod]
        public void TestMapHasNoAdjacentCellsOutOfBounds()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();
            Assert.IsFalse(map.HasAdjacentCellInDirection(new Point(-5 -5), DirectionType.North));
        }

        [TestMethod]
        public void TestVisitedAdjacentCellInDirectionIsVisited()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();

            // set cells around the location we're going to use as visited
            map[1, 0].Visited = true;
            map[0, 1].Visited = true;
            map[1, 2].Visited = true;
            map[2, 1].Visited = true;

            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.North));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.South));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.East));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.West));
        }

        [TestMethod]
        public void TestUnvisitedAdjacentCellInDrectionIsUnvisited()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();

            Assert.IsFalse(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.North));
            Assert.IsFalse(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.South));
            Assert.IsFalse(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.East));
            Assert.IsFalse(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.West));
        }

        [TestMethod]
        public void TestGetNextDirectionValidDirection()
        {
            DirectionPicker directionPicker = new DirectionPicker(DirectionType.North, 100);
            List<DirectionType> directionsPicked = new List<DirectionType>();

            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(directionPicker.HasNextDirection);
                DirectionType direction = directionPicker.GetNextDirection();
                Assert.IsTrue(!directionsPicked.Contains(direction));
                directionsPicked.Add(direction);
            }

            Assert.IsFalse((directionPicker.HasNextDirection));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetNextDirectionThrowsException()
        {
            DirectionPicker directionPicker = new DirectionPicker(DirectionType.North, 100);
            for (int i = 0; i < 5; i++)
                directionPicker.GetNextDirection();
        }

        [TestMethod]
        public void TestGetRandomVisitedCell()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();

            map.FlagCellAsVisited(new Point(1, 0));
            map.FlagCellAsVisited(new Point(0, 1));
            map.FlagCellAsVisited(new Point(1, 2));
            map.FlagCellAsVisited(new Point(2, 1));

            var currentCell = new Point(2, 1);
            var visitedCell = map.GetRandomVisitedCell(currentCell); // checks that teh visited cell is one of the first 3 cells visited and not the current cell.

            Assert.IsTrue(((visitedCell == new Point(1, 0)) || (visitedCell == new Point(0, 1)) || (visitedCell == new Point(1, 2))));
            Assert.AreNotEqual(visitedCell, currentCell);
        }

        [TestMethod]
        public void TestCreateCorridorBetweenAdjacentCells()
        {
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();

            map.CreateCorridor(new Point(0, 0), DirectionType.South);

            Assert.IsTrue(map[0, 0].NorthSide == SideType.Wall);
            Assert.IsTrue(map[0, 0].SouthSide == SideType.Empty);
            Assert.IsTrue(map[0, 0].WestSide == SideType.Wall);
            Assert.IsTrue(map[0, 0].EastSide == SideType.Wall);

            Assert.IsTrue(map[0, 1].NorthSide == SideType.Empty);
            Assert.IsTrue(map[0, 1].SouthSide == SideType.Wall);
            Assert.IsTrue(map[0, 1].WestSide == SideType.Wall);
            Assert.IsTrue(map[0, 1].EastSide == SideType.Wall);
        }

        [TestMethod]
        public void TestMustChangeDirectionsAlways()
        {
            DirectionPicker directionPicker = new DirectionPicker(DirectionType.North, 100);
            Assert.IsTrue(directionPicker.MustChangeDirection);
        }

        [TestMethod]
        public void TestMustChangeDirectionNever()
        {
            DirectionPicker directionPicker = new DirectionPicker(DirectionType.North, 100);
            Assert.IsFalse(directionPicker.MustChangeDirection);
        }

        [TestMethod]
        public void TestDirectionPickerChoosesPreviousDirection()
        {
            DirectionType previousDirection = DirectionType.West;
            DirectionPicker directionPicker = new DirectionPicker(previousDirection, 0);
            Assert.AreEqual(previousDirection, directionPicker.GetNextDirection());
        }

        [TestMethod]
        public void TestDirectionPickerChoosesDifferentDirection()
        {
            DirectionType previousDirection = DirectionType.West;
            DirectionPicker directionPicker = new DirectionPicker(previousDirection, 100);
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
        }

        [TestMethod]
        public void TestGeneratorWithNeverChangeDirection()
        {
            var generator = new Generator.Generator();
            var map = new Map(10, 10, 0);

            int visitedCellCount = 0;
            for(int x = 0; x < map.Width; x++)
                for(int y = 0; y < map.Height; y++)
                    if (map[x, y].Visited) visitedCellCount++;

            Assert.IsTrue(visitedCellCount == (map.Height * map.Width));
        }

        [TestMethod]
        public void TestGeneratorWithAlwaysChangeDirection()
        {
            var generator = new Generator.Generator();
            var map = new Map(10, 10, 100);

            int visitedCellCount = 0;

            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map[x, y].Visited) visitedCellCount++;

            Assert.IsTrue(visitedCellCount == (map.Height * map.Width));
        }


    }
}
