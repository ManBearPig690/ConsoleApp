using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Generator;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Generator.RandomPlot;


namespace GeneratorTest
{
    [TestClass]
    public class RandomPlotTest
    {
        [TestMethod]
        public void VisualizeMap()
        {
            var dungeon = new Dungeon(60, 30, 4, 12, 20);

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
             var dungeon = new Dungeon(60, 40, 4, 12, 20);
             using (var sw = new StreamWriter(@"C:\temp\map.txt"))
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
             int blockedCount = 0;
             int totalDirections = 3;
             for (int x = 0; x < dungeon.Width; x++)
             {
                 for (int y = 0; y < dungeon.Height; y++)
                 {
                     
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
 
                     if (x < dungeon.Width - 1)
                     {
                         if (dungeon.Map[x + 1, y].Blocked)
                             blockedCount++;
                     }
                     else
                         blockedCount++;
 
                     if (y < dungeon.Height - 1)
                     {
                         if (dungeon.Map[x, y + 1].Blocked)
                             blockedCount++;
                     }
                     else
                         blockedCount++;

                     if (blockedCount <= 3)
                     {
                         dungeon.Map[x, y] = new Tile(true);
                     }
                         
                 }
             }
 
             using (var sw = new StreamWriter(@"C:\temp\CleanMap.txt"))
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
