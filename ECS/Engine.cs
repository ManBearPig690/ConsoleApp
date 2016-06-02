using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class Engine
    {
        private static SystemManager _systemManager;
        private static bool running = true;

        public Engine()
        {
            _systemManager = new SystemManager();
            EntityManager.CreatePlayerEntity(0, 0, 0, 0, "@");
        }

        public void Run()
        {
            while(running)
            {

            }
        }
    }
}
