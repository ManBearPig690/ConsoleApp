using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Dungeon
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public const string Wall = "#";
        public const string Ground = ".";
        public Tile[,] Map;

        public Dungeon(int width, int height)
        {
            Width = width;
            Height = height;
            Map = new Tile[Width, Height];
            InitMap();
        }

        public void InitMap()
        {
            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Map[x, y] = new Tile(true);

            var room1 = new Rect(20, 15, 10, 15);
            var room2 = new Rect(50, 15, 10, 15);
            CreateRoom(room1);
            CreateRoom(room2);
            CreateXTunnel(25, 55, 23);
        }

        public void CreateRoom(Rect room)
        {
            // the + 1 adds a buffer so no rects will overlapp....may not be needed but leave for now and test
            for (int x = room.X1 + 1; x < room.X2; x++)
            {
                for (int y = room.Y1 + 1; y < room.Y2; y++)
                {
                    Map[x, y].Blocked = false;
                    Map[x, y].BlockSight = false;
                }
            }
        }
        public void CreateXTunnel(int x1, int x2, int y)
        {
            var min = x1 < x2 ? x1 : x2;
            var max = x1 > x2 ? x1 : x2;
            for (var x = min; x < max + 1; x++)
            {
                Map[x, y].Blocked = false;
                Map[x, y].BlockSight = false;
            }
        }

        public void CreateYTunnel(int y1, int y2, int x)
        {
            var min = y1 < y2 ? y1 : y2;
            var max = y1 > y2 ? y1 : y2;
            for (var y = min; y < max + 1; y++)
            {
                Map[x, y].Blocked = false;
                Map[x, y].BlockSight = false;
            }
        }
    }
}
