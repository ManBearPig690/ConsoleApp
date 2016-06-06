using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class BspRoom
    {
        private int _minSize;
        
        

        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int RoomX1;
        public int RoomY1;
        public int RoomX2;
        public int RoomY2;
        public bool Horizontal;
        public BspRoom LeftChild;
        public BspRoom RightChiled;
        public bool IsFilled = false;

        public BspRoom(int x, int y, int w, int h, int size)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            _minSize = size;
        }

        /// <summary>
        /// splits the grid into chuncks
        /// </summary>
        /// <returns></returns>
        public bool SplitBsp()
        {
            var random = new Random((int)DateTime.Now.Ticks); // helps to crate more psudo random numbers

            if (LeftChild != null) return false; // already split

            Horizontal = random.Next(100) > 50;

            var maxSize = (Horizontal ? Height : Width) - _minSize; // max height/widht that can be split off
            if (maxSize <= _minSize) return false; // area to small

            var splitPoint = random.Next(maxSize); // generate random split point
            if (splitPoint < _minSize) splitPoint = _minSize; // adjust point so they are both at least the min size

            if (Horizontal)
            {
                LeftChild = new BspRoom(X, Y, Width, splitPoint, _minSize);
                RightChiled = new BspRoom(X, Y + splitPoint, Width, Height - splitPoint, _minSize);
            }
            else
            {
                LeftChild = new BspRoom(X, Y, splitPoint, Height, _minSize);
                RightChiled = new BspRoom(X + splitPoint, Y, Width - splitPoint, Height, _minSize);
            }

            return true; // successfull split
        }

        // Generates a room inside the area
        public void GenerateBspRoom()
        {
            if (LeftChild != null) return;

            var random = new Random((int) DateTime.Now.Ticks);
            var minX = (Width * 3 / 4) > _minSize ? (Width * 3 / 4) : _minSize;
            var minY = (Height * 3 / 4) > _minSize ? (Height * 3 / 4) : _minSize;
            RoomX1 = X + ((Width - _minSize) <= 0 ? 0 : random.Next((Width - minX)));
            RoomY1 = Y + ((Height - _minSize) <= 0 ? 0 : random.Next((Height - minY)));

            //var randomWidth = (Width - _minSize <=0 ) ? 0 : random.Next(Width - RoomX1);
            //var randomHeight = (Height - _minSize <= 0) ? 0 : random.Next(Height - RoomY1);
            //RoomX2 = RoomX1 + (randomWidth > minX ? randomWidth : minX) - 1;
            //RoomY2 = RoomY1 + (randomHeight > minY ? randomHeight : minY) - 1;

            var randomWidth = (Width - RoomX1 <= 0) ? 0 : random.Next(Width - RoomX1);
            var randomHeight = (Height - RoomY1 <= 0) ? 0 : random.Next(Height - RoomY1);
            RoomX2 = RoomX1 + (randomWidth > minX ? randomWidth : minX) - 1;
            RoomY2 = RoomY1 + (randomHeight > minY ? randomHeight : minY) - 1;

            IsFilled = true;
        }

    }
}
