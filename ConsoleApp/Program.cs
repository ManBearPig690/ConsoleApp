using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static int origX;
        private static int origY;
        private static bool running = true;
        private static ConsoleKeyInfo keyInfo;
        static void Main(string[] args)
        {
            Console.Clear();
            origX = Console.CursorLeft;
            origY = Console.CursorTop;
            Console.Write("@");
            Console.CursorVisible = false;
            while (running)
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveTo("@", 0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        MoveTo("@", 0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveTo("@", -1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        MoveTo("@", 1, 0);
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                    default:
                        break;
                }
            }

        }

        protected static void MoveTo(string s, int x, int y)
        {
            try
            {
                if (y == -1 && Console.CursorTop == 0) return;
                if (x == -1 && Console.CursorLeft == 0) return;
                Console.Clear();
                Console.SetCursorPosition(origX += x, origY += y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        protected static void WriteAt(string s, int x, int y)
        {
            // for writing anyting to anywhere

        }
    }
}
