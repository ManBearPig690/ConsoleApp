using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;


namespace GeneratorTest
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void VisualizeMap()
        {
            var dungeon = new Dungeon(80, 45, 6, 10, 30);

            using(var sw = new StreamWriter(@"C:\temp\map.txt"))
            {
                for (var y = 0; y < dungeon.Height; y++)
                {
                    var lineText = "";
                    for (var x = 0; x < dungeon.Width; x++)
                    {
                        lineText += dungeon.Map[x, y].Blocked ? " " : ".";
                    }
                    sw.WriteLine(lineText);
                }
            }    
        }
    }
}
