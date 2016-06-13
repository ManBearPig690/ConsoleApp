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
    }
}
