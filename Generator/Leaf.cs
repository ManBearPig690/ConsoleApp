using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Generator.RandomPlot;

namespace Generator
{
    public class Leaf
    {
        private const int MinLeafSize = 6;
        private readonly Random _random = new Random((int)DateTime.Now.Ticks);
        // position and size of leaf
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Leaf LeftChild;
        public Leaf RightChild;
        public Rect Room;
        public List<Rect> Halls;

        public Leaf(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public bool Split()
        {
            
            if (LeftChild != null || RightChild != null)
                return false; // already split

            bool splitH = _random.Next(1) == 0;
            if (Width > Height && (float)Width/Height >= 1.25)
                splitH = false;
            else if (Height > Width && (float) Height/Width >= 1.25)
                splitH = true;

            var max = (splitH ? Height : Width) - MinLeafSize;
            if (max <= MinLeafSize)
                return false; // to small to split

            var split = _random.Next(MinLeafSize);

            if (splitH)
            {
                LeftChild = new Leaf(X, Y, Width, split);
                RightChild = new Leaf(X, Y + split, Width, Height - split);
            }
            else
            {
                LeftChild = new Leaf(X, Y, split, Height);
                RightChild = new Leaf(X + split, Y, Width - split, Height);
            }
            return true;
        }

        public void CreateRooms()
        {
            // generates all rooms and hallways for this leaf and all of its children
            if (LeftChild != null || RightChild != null)
            {
                // its split so go into the child leaves
                if(LeftChild != null)
                    LeftChild.CreateRooms();
                if(RightChild != null)
                    RightChild.CreateRooms();
            }
            else
            {
                Point RoomSize;
                Point RoomPosition;
                // the room can be between 3 x3 tiles to the sizie of the leaf -2
                RoomSize = new Point(_random.Next(3, Width - 2), _random.Next(3, Height - 2));
                // place the romo within leaf, but not right against the side of the leaf (cause intersections)
                RoomPosition = new Point(_random.Next(1, Width - RoomSize.X - 1),
                    _random.Next(1, Height - RoomSize.Y - 1));
                Room = new Rect(X + RoomPosition.X, Y + RoomPosition.Y, RoomSize.X, RoomSize.Y);
            }
        }

        public Rect GetRoom()
        {
            if (Room != null) return Room;

            Rect leftRoom = null;
            Rect righttRoom = null;

            if (LeftChild != null)
                leftRoom = LeftChild.GetRoom();
            if (RightChild != null)
                righttRoom = RightChild.GetRoom();

            if (leftRoom == null && righttRoom == null)
                return null;
            else if (righttRoom == null)
                return leftRoom;

        }
    }
}
