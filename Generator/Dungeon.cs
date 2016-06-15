using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Generator
{
    public class Dungeon
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int RoomMaxSize { get; set; }
        public int RoomMinSize { get; set; }
        public int MaxRooms { get; set; }
        public int PlayerStartX { get; set; }
        public int PlayerStartY { get; set; }

        public const string Wall = "#";
        public const string Ground = ".";
        

        public Tile[,] Map;
        public List<Rect> Rooms;

        public Dungeon(int width, int height, int minRoomSize, int maxRoomSize, int maxRooms)
        {
            Width = width;
            Height = height;
            RoomMinSize = minRoomSize;
            RoomMaxSize = maxRoomSize;
            MaxRooms = maxRooms;
            Map = new Tile[Width, Height];
            InitMap();
        }

        public void InitMap()
        {
            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    Map[x, y] = new Tile(true);

            Rooms = new List<Rect>();
            int numRooms = 0;
            bool failed;
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < MaxRooms; i++)
            {
                // Random width and height
                
                var w = r.Next(RoomMinSize, RoomMaxSize); // +1 to include max...possibly
                var h = r.Next(RoomMinSize, RoomMaxSize);
                // Random position with out going out of bounds
                var x = r.Next(0, Width - w - 1);
                var y = r.Next(0, Height - h - 1);

                // Rect class makes rectagles easier to work with
                var newRoom = new Rect(x, y, w, h);
                // run through the other rooms and see if they intersect with this one
                
                failed = false;
                foreach (var otherRoom in Rooms)
                {
                    if (newRoom.Intersect(otherRoom))
                    {
                        failed = true;
                        break;
                    }
                }
                //failed = Rooms.Any(otherRoom => newRoom.Intersect(otherRoom));
                if (!failed)
                    {
                        // meas there are no intersections so room is valid

                        // "paint" it to the maps tiles
                        CreateRoom(newRoom);
                        var newXy = newRoom.Center();

                        if (numRooms == 0)
                        {
                            // this is the first room where the player starts
                            PlayerStartX = newXy.Item1;
                            PlayerStartY = newXy.Item2;
                        }
                        else
                        {
                            // all rooms after the first
                            // connect it to the previsous room 
                            var prevXy = Rooms[numRooms - 1].Center();

                            // flip a coin
                            if (r.Next(1) == 1)
                            {
                                // first move horizontally then vertically
                                CreateXTunnel(prevXy.Item1, newXy.Item1, prevXy.Item2);
                                CreateYTunnel(prevXy.Item2, newXy.Item2, newXy.Item1);
                            }
                            else
                            {
                                // first vertically then horizontally
                                CreateYTunnel(prevXy.Item2, newXy.Item2, prevXy.Item1);
                                CreateXTunnel(prevXy.Item1, newXy.Item1, newXy.Item2);
                            }

                        }
                    }
                Rooms.Add(newRoom);
                numRooms += 1;
            }

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
