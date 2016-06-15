using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
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
        public static Dungeon Dungeon;
        
        public static void Initialize(int w, int h, int minRoomSize, int maxRoomSize, int maxRooms)
        {
            _gridWidth = w;
            _gridHeight = h;
            Dungeon = new Dungeon(width: w, height: h, minRoomSize: minRoomSize, maxRoomSize: maxRoomSize,
                maxRooms: maxRoomSize);


            OrigX = Console.CursorLeft;
            OrigY = Console.CursorTop;
            Console.CursorVisible = false;
            EntityManager.Initialize();
            EntityManager.CreatePlayerEntity(Dungeon.PlayerStartX, Dungeon.PlayerStartY, 0, 0, "@");

            //SystemManager.CreateComponentLists(ref EntityManager.Entities);
            SystemManager.Initialize();
            SystemManager.CreateComponentLists();
        }

        public static void Run()
        {
            //SystemManager.RenderSystem.WriteAt("@", _playerX, _playerY);
            OrigX = Dungeon.PlayerStartX;
            OrigY = Dungeon.PlayerStartY;
            SystemManager.RenderSystem.RenderMap(ref Dungeon);
            SystemManager.RenderSystem.RenderPlayer(ref SystemManager.PlayerComponentEntities);
            while(running)
            {
                Console.SetCursorPosition(_gridWidth, _gridHeight);
                //SystemManager.RenderSystem.RenderMap(ref _dungeon); // dont render everytime -> causes flashing just replace the tile when moving.
                SystemManager.InputSystem.Update(1, ref SystemManager.InputComponentEntities);
            }
        }

        //private static string[,] InitGrid()
        //{
        //    var grid = new string[_gridWidth, _gridHeight];
        //    for (var i = 0; i < _gridHeight; i++)
        //        for (var j = 0; j < _gridWidth; j++)
        //            grid[j, i] = "0";
        //    return grid;
        //}
    }
}
