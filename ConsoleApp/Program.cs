﻿using System;
using Engine;
using Generator.BSP;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Engine.Engine.Initialize(w: 45, h: 45, minRoomSize: 6, maxRoomSize: 10, maxRooms: 30);
            Engine.Engine.Run();
            //var grid = new string[60, 30];
            //for (var i = 0; i < 30; i++)
            //    for (var j = 0; j < 60; j++)
            //        grid[j, i] = "0";
            //var bsp = new BSP();
            //bsp.UpdateParam(60, 30, (60 * 30) / 60, 5);
            //bsp.GeneratePcgBsp(grid);


            //for (var i = 0; i < 30; i++)
            //{
            //    for (var j = 0; j < 60; j++)
            //    {
            //        Console.SetCursorPosition(j, i);
            //        switch (bsp.PcGrid[j, i])
            //        {
            //            case "2":
            //                Console.Write("#");
            //                break;
            //            case "1":
            //                Console.Write(".");
            //                break;
            //            case "4":
            //                Console.Write(".");
            //                break;
            //            default:
            //                break;
            //        }
            //        //if(bsp.PcGrid[j, i] != "5" && bsp.PcGrid[j, i] != "0")
            //        //    Console.Write(bsp.PcGrid[j, i]);
            //    }
            //}
        }

    }
}
