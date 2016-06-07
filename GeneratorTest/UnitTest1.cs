using System;
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
                    Assert.IsFalse(map[x, y]);
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
                    if (map[x, y])
                        visistedCellCount++;
                }
            }

            Assert.IsTrue(visistedCellCount == 1);
        }

        [TestMethod]
        public void TestGeneratorInstantion()
        {
            var generator = new Generator.Generator();

            var map = generator.Generate();

            var visitedCellCount = 0;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    if (map[x, y])
                        visitedCellCount++;
                }
            }

            Assert.IsTrue(visitedCellCount == 1);
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
            map[1, 0] = true;
            map[0, 1] = true;
            map[1, 2] = true;
            map[2, 1] = true;

            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.North));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.South));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.East));
            Assert.IsTrue(map.AdjacentCellInDirectionIsVisited(new Point(1, 1), DirectionType.West));
        }

        [TestMethod]
        public void TestUnvisitedAdjacentCellInDrectionIsUnvisited()
        {
            
        }
    }
}
