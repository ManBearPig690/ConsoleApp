using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Generator;
using Generator.BSP;

namespace Engine
{
    public static class Engine
    {
        public static bool running = true;

        public static int OrigX;
        public static int OrigY;

        private static int _gridWidth;
        private static int _gridHeight;
        private static int _minRoomSize;
        private static int _minRoomNum;

        private static BSP _dungeon;
        
        public static void Initialize(int w, int h, int minRoomSize)
        {
            _gridWidth = w;
            _gridHeight = h;
            _minRoomNum = (_gridWidth*_gridHeight)/100;
            _minRoomSize = minRoomSize;
            _dungeon = new BSP();

            OrigX = Console.CursorLeft;
            OrigY = Console.CursorTop;
            Console.CursorVisible = false;
            EntityManager.Initialize();
            EntityManager.CreatePlayerEntity(0, 0, 0, 0, "@");

            //SystemManager.CreateComponentLists(ref EntityManager.Entities);
            SystemManager.Initialize();
            SystemManager.CreateComponentLists();

            _dungeon.UpdateParam(_gridWidth, _gridHeight, _minRoomNum, _minRoomSize);
        }

        public static void Run()
        {
            SystemManager.RenderSystem.WriteAt("@", 0, 0);
            while(running)
            {
                SystemManager.InputSystem.Update(1, ref SystemManager.InputComponentEntities);
            }
        }

        private static string[,] InitGrid()
        {
            var grid = new string[_gridWidth, _gridHeight];
            for (var i = 0; i < _gridHeight; i++)
                for (var j = 0; j < _gridWidth; j++)
                    grid[j, i] = "0";
            return grid;
        }
    }
}
