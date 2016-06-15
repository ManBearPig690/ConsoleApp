using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Rect
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }

        public Rect(int x, int y, int w, int h)
        {
            X1 = x;
            Y1 = y;
            X2 = x + w;
            Y2 = y + h;
        }

        public Tuple<int, int> Center()
        {
            var centerX = (X1 + X2)/2;
            var centerY = (Y1 + Y2)/2;
            return new Tuple<int, int>(centerX, centerY);
        }

        public bool Intersect( Rect other)
        {
            return (X1 <= other.X2 && X2 >= other.X1 && Y1 <= other.Y2 && Y2 >= other.Y1);
        }
    }
}
