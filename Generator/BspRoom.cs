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
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _roomX1;
        private int _roomY1;
        private int _roomX2;
        private int _roomY2;
        private bool _isFilled = false;
        private bool _horizontal;
        private BspRoom _leftChild;
        private BspRoom _rightChiled;

        public BspRoom(int x, int y, int w, int h, int size)
        {
            _x = x;
            _y = y;
            _width = w;
            _height = h;
            _minSize = size;
        }

        public bool SplitBsp()
        {
            var r = new Random((int)DateTime.Now.Ticks);
            if(_leftChild != null) return false; // already split

            _horizontal = r.Next(100) > 50;

            var maxSize = (_horizontal ? _height : _width) - _minSize;
            if (maxSize <= _minSize) return false; // area to small

            //var split

            return true;
        }

    }
}
