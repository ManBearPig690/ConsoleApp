using System;
using System.Collections.Generic;
using System.Linq;
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
            for(int x =0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Map[x, y] = new Tile(false);
        }
    }
}
