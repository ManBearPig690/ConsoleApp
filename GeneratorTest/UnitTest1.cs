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
    }
}
