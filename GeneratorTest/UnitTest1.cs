using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Drawing;
using System.IO;
using System.Linq;


namespace GeneratorTest
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void VisualizeMap()
        {
            Dungeon _dungeon = new Dungeon(80, 45);

        using(StreamWriter sw = new StreamWriter(@"C:\temp\map.txt"))
        {
            string lineText;
            for (int x = 0; x < _dungeon.Width; x++)
            {
                lineText = "";
                for (int y = 0; y < _dungeon.Height; y++)
                {
                    lineText += _dungeon.Map[x, y].Blocked ? " " : ".";
                }
                sw.WriteLine(lineText);
            }
        }    
        }
        
    }
}
