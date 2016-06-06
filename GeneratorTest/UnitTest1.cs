using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Collections.Generic;

namespace GeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MarkCellsUnVisited()
        {
            Map map = new Map(10, 10);
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
            Map map = new Map(10, 10);
            map.MarkCellsUnvisited();
            map.PickRandomCellAndMarkItVisited();

            int visistedCellCount = 0;
            for(int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map[x, y])
                        visistedCellCount++;
                }
            }

            Assert.IsTrue(visistedCellCount == 1);
        }
    }
}
