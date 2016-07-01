using System;
using Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLeafGeneration()
        {
            var map = new Map(40, 40);
            map.GenerateLeafs();
            
        }
    }
}
