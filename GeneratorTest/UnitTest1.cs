using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;

namespace GeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var grid = new byte[40, 30];
            for (var i = 0; i < 30; i++)
                for (var j = 0; j < 40; j++)
                    grid[j, i] = 0;
            var bsp = new BSP();
            bsp.UpdateParam(40, 30, (40*30)/100, 5);
            bsp.GeneratePcgBsp(grid);


            for (var i = 0; i < 30; i++)
                for (var j = 0; j < 40; j++)
                    Console.WriteLine(bsp.PcGrid[i, j].ToString());
            
        }
    }
}
