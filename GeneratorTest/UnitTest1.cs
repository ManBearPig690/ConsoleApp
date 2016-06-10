using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Drawing;
using System.Linq;


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

        //[TestMethod]
        //public void TestGeneratorInstantion()
        //{
        //    var generator = new Generator.Generator();

        //    var map = generator.Generate(10, 10, 100);

        //    var visitedCellCount = 0;
        //    for (var x = 0; x < map.Width; x++)
        //    {
        //        for (var y = 0; y < map.Height; y++)
        //        {
        //            if (map[x, y].Visited)
        //                visitedCellCount++;
        //            Console.WriteLine("{0}, {1}: Visited: {2}",x, y, map[x,y].Visited);
        //        }
        //    }
            
        //    Assert.IsTrue(visitedCellCount == (map.Width * map.Height)); // will be 100 becuase visistedCellCount returns 
        //}

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
            DirectionPicker directionPicker = new DirectionPicker(DirectionType.North, 0);
            Assert.IsFalse(directionPicker.MustChangeDirection);
        }

        [TestMethod]
        public void TestDirectionPickerChoosesPreviousDirection()
        {
            DirectionType previousDirection = DirectionType.West;
            DirectionPicker directionPicker = new DirectionPicker(previousDirection, 0);
            Assert.AreEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
        }

        [TestMethod]
        public void TestDirectionPickerChoosesDifferentDirection()
        {
            DirectionType previousDirection = DirectionType.West;
            DirectionPicker directionPicker = new DirectionPicker(previousDirection, 100);
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreNotEqual(previousDirection, directionPicker.GetNextDirection());
            Assert.AreEqual(previousDirection, directionPicker.GetNextDirection());
        }

        [TestMethod]
        public void TestGeneratorWithNeverChangeDirection()
        {
            var map = Generator.Generator.Generate(10, 10, 0);

            int visitedCellCount = 0;
            for(int x = 0; x < map.Width; x++)
                for(int y = 0; y < map.Height; y++)
                    if (map[x, y].Visited) visitedCellCount++;

            Assert.IsTrue(visitedCellCount == (map.Height * map.Width));
        }

        [TestMethod]
        public void TestGeneratorWithAlwaysChangeDirection()
        {
            var map = Generator.Generator.Generate(10, 10, 100);

            int visitedCellCount = 0;

            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map[x, y].Visited) visitedCellCount++;

            Assert.IsTrue(visitedCellCount == (map.Height * map.Width));
        }

        [TestMethod]
        public void TestIfCellIsDeadEnd()
        {
            Cell cell = new Cell();
            cell.NorthSide = SideType.Empty;
            Assert.IsTrue(cell.IsDeadEnd);

            cell = new Cell();
            cell.WestSide = SideType.Empty;
            Assert.IsTrue(cell.IsDeadEnd);

            cell = new Cell();
            cell.EastSide = SideType.Empty;
            Assert.IsTrue(cell.IsDeadEnd);

            cell = new Cell();
            cell.SouthSide = SideType.Empty;
            Assert.IsTrue(cell.IsDeadEnd);
        }

        [TestMethod]
        public void TestIfCellIsNotDeadEnd()
        {
            Cell cell = new Cell();
            Assert.IsFalse(cell.IsDeadEnd);

            cell = new Cell();
            cell.NorthSide = SideType.Empty;
            cell.EastSide = SideType.Empty;
            Assert.IsFalse(cell.IsDeadEnd);

            cell.WestSide = SideType.Empty;
            Assert.IsFalse(cell.IsDeadEnd);

            cell.SouthSide = SideType.Empty;
            Assert.IsFalse(cell.IsDeadEnd);
        }

        [TestMethod]
        public void TestCanRetrieveAllDeadEndCellsInMap()
        {
            var map = Generator.Generator.Generate(10, 10, 100);

            // loop through each cell and increment counter for each dead end
            int deadEndCellCount1 = 0;
            for (int x = 0; x < map.Width; x++)
                for (int y = 0; y < map.Height; y++)
                    if (map[x, y].IsDeadEnd)
                        deadEndCellCount1++;

            // loop through each dead end cell location in map
            int deadEndCellCount2 = 0;
            foreach (var location in map.DeadEndCellLocations)
            {
                // assert the location is a dead end
                Assert.IsTrue(map[location].IsDeadEnd);
                deadEndCellCount2++;
            }
            // assert that DeadEndCellLocations returns the same amout of 
            // dead end cell locations as manual process
            Assert.AreEqual(deadEndCellCount1, deadEndCellCount2);
        }

        [TestMethod]
        public void TestCanGetDeadEndCorridorDirection()
        {
            Cell cell = new Cell();
            cell.NorthSide = SideType.Empty;
            Assert.AreEqual(DirectionType.North, cell.CalculateDeadEndCorridorDirection());

            cell = new Cell();
            cell.SouthSide = SideType.Empty;
            Assert.AreEqual(DirectionType.South, cell.CalculateDeadEndCorridorDirection());

            cell = new Cell();
            cell.WestSide = SideType.Empty;
            Assert.AreEqual(DirectionType.West, cell.CalculateDeadEndCorridorDirection());

            cell = new Cell();
            cell.EastSide = SideType.Empty;
            Assert.AreEqual(DirectionType.East, cell.CalculateDeadEndCorridorDirection());
        }

        //[TestMethod]
        //public void TestCreateWallBetweenAdjacentCells()
        //{
        //    Map map = new Map(10, 10);
        //    map.MarkCellsUnvisited();

        //    // loop through each cell and make all the sides empty
        //    for (int x = 0; x < map.Width; x++)
        //    {
        //        for (int y = 0; y < map.Height; y++)
        //        {
        //            map[x, y].NorthSide = SideType.Empty;
        //            map[x, y].SouthSide = SideType.Empty;
        //            map[x, y].WestSide = SideType.Empty;
        //            map[x, y].EastSide = SideType.Empty;
        //        }
        //    }

        //    // we now have a map with no walls
        //    // test creating walls in each direction
        //    map.CreateWall(new Point(0, 0), DirectionType.South);

        //    Assert.IsTrue(map[0, 0].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].SouthSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 0].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].WestSide == SideType.Empty);

        //    Assert.IsTrue(map[0, 1].NorthSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 1].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].WestSide == SideType.Empty);

        //    map.CreateWall(new Point(0, 0), DirectionType.North);

        //    Assert.IsTrue(map[0, 0].NorthSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 0].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].WestSide == SideType.Empty);

        //    Assert.IsTrue(map[0, 1].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].SouthSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 1].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].WestSide == SideType.Empty);

        //    map.CreateWall(new Point(0, 0), DirectionType.East);

        //    Assert.IsTrue(map[0, 0].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].EastSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 0].WestSide == SideType.Empty);

        //    Assert.IsTrue(map[0, 1].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].WestSide == SideType.Wall);

        //    map.CreateWall(new Point(0, 0), DirectionType.West);

        //    Assert.IsTrue(map[0, 0].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].EastSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 0].WestSide == SideType.Wall);

        //    Assert.IsTrue(map[0, 1].NorthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].SouthSide == SideType.Empty);
        //    Assert.IsTrue(map[0, 1].EastSide == SideType.Wall);
        //    Assert.IsTrue(map[0, 1].WestSide == SideType.Empty);
        //}

        //[TestMethod]
        //public void TestSparsifyMazeRemovesDeadEnds()
        //{
        //    Map map = Generator.Generator.Generate(10, 10, 100);

        //    // Enumerate all the current dead-end locations
        //    // add them to the list
        //    List<Point> deadEndPoints = map.DeadEndCellLocations.ToList();

        //    Generator.Generator.SparsifyMaze(map);

        //    // enumerate all the new dead-end points
        //    // asser that they aren't in the original list
        //    foreach (var deadEndCellLocation in map.DeadEndCellLocations)
        //    {
        //        Assert.IsFalse(deadEndPoints.Contains(deadEndCellLocation));
        //    }
        //}

        //[TestMethod]
        //public void TestMinSparsenessFactor()
        //{
        //    Map map = new Map(10, 10);
        //    map.MarkCellsUnvisited();
        //    Generator.Generator.CreateDenseMaze(map, 50);
        //    Generator.Generator.SparsifyMaze(map, 0);

        //    // Assert that no cells were turned into rocks
        //    for (int x = 0; x < map.Width; x++)
        //    {
        //        for (int y = 0; y < map.Height; y++)
        //        {
        //            Assert.IsTrue(map[x, y].WallCount < 4);
        //        }
        //    }
        //}

        //[TestMethod]
        //public void TestMaxSparsenessFactor()
        //{
        //    Map map = new Map(10, 10);
        //    map.MarkCellsUnvisited();
        //    Generator.Generator.CreateDenseMaze(map, 50);
        //    Generator.Generator.SparsifyMaze(map, 100);

        //    for(int x = 0; x < map.Width; x++)
        //        for(int y = 0; y < map.Height; y++)
        //            Assert.IsTrue(map[x, y].WallCount == 4);
        //}

        //[TestMethod]
        //public void TestMidSparsenessFactor()
        //{
        //    Map map = new Map(10, 10);
        //    map.MarkCellsUnvisited();
        //    Generator.Generator.CreateDenseMaze(map, 50);
        //    Generator.SparsifyMaze(map, 50);

        //    int targetRockCellCount = (int)Math.Ceiling((decimal)50 / 100 * (map.Width * map.Height));
        //    int currentRockCellCount = 0;

        //    for (int x = 0; x < map.Width; x++)
        //        for (int y = 0; y < map.Height; y++)
        //            if (map[x, y].WallCount == 4) currentRockCellCount++;

        //    // Assert that 50% of the cells were turned into rock
        //    Assert.AreEqual(targetRockCellCount, currentRockCellCount);
        //}

        //[TestMethod]
        //public void TestShouldRemoveNotDeadend()
        //{
        //    Assert.IsFalse(Generator.Generator.ShouldRemoveDeadend(0));
        //}
    }
}
