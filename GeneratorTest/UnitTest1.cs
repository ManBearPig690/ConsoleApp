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

        [TestMethod]
        public void CleanUpMap()
        {
            var dungeon = new Dungeon(80, 25, 6, 10, 30);
            int blockedCount = 0;
            int totalDirections = 3;
            for (int x = 0; x < dungeon.Width; x++)
            {
                for (int y = 0; y < dungeon.Height; y++)
                {
                    
                    var tile = dungeon.Map[x, y];
                    if (tile.Blocked) continue;

                    if (x != 0)
                    {
                        if (dungeon.Map[x - 1, y].Blocked)
                            blockedCount++;
                    }
                    else
                        blockedCount++;

                    if (y != 0)
                    {
                        if (dungeon.Map[x, y - 1].Blocked)
                            blockedCount++;
                    }
                    else
                        blockedCount++;

                    if (x < dungeon.Width)
                    {
                        if (dungeon.Map[x + 1, y].Blocked)
                            blockedCount++;
                    }
                    else
                        blockedCount++;

                    if (y < dungeon.Height)
                    {
                        if (dungeon.Map[x, y + 1].Blocked)
                            blockedCount++;
                    }
                    else
                        blockedCount++;
                    
                    if(blockedCount <= 3)
                        dungeon.Map[x,y].Blocked = true;
                }
            }

            using (var sw = new StreamWriter(@"C:\temp\map.txt"))
            {
                for (var y = 0; y < dungeon.Height; y++)
                {
                    var lineText = "";
                    for (var x = 0; x < dungeon.Width; x++)
                    {
                        lineText += dungeon.Map[x, y].Blocked ? "#" : ".";
                    }
                    sw.WriteLine(lineText);
                }
            }
        }

    }
}
