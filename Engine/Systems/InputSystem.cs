using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Component;

namespace Engine.Systems
{
    public class InputSystem : System
    {
        public ConsoleKeyInfo keyInfo;
        public override void Update(int dt, ref List<string> componentEntityList)
        {
            foreach (var id in componentEntityList)
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 0, 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, -1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 1, 0);
                        break;
                    case ConsoleKey.Escape:
                        Engine.running = false;
                        break;
                    default:
                        break;
                }    
            }
            

        }
    }
}
