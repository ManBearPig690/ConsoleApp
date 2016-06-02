using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Engine
    {
        public static bool running = true;

        public static int OrigX;
        public static int OrigY;

        static Engine()
        {
            OrigX = Console.CursorLeft;
            OrigY = Console.CursorTop;
            Console.CursorVisible = false;
            EntityManager.CreatePlayerEntity(0, 0, 0, 0, "@");

            SystemManager.CreateComponentLists(ref EntityManager.Entities);
        }

        public static void Run()
        {
            while(running)
            {

            }
        }
    }
}
