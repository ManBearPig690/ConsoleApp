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


        public static void Initialize()
        {
            OrigX = Console.CursorLeft;
            OrigY = Console.CursorTop;
            Console.CursorVisible = false;
            EntityManager.Initialize();
            EntityManager.CreatePlayerEntity(0, 0, 0, 0, "@");

            //SystemManager.CreateComponentLists(ref EntityManager.Entities);\
            SystemManager.Initialize();
            SystemManager.CreateComponentLists();
        }

        public static void Run()
        {
            SystemManager.RenderSystem.WriteAt("@", 0, 0);
            while(running)
            {
                SystemManager.InputSystem.Update(1, ref SystemManager.InputComponentEntities);
            }
        }
    }
}
