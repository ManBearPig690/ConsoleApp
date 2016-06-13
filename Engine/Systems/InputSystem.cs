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
                int prevX = EntityManager.Entities[id].GetComponent<PositionComponent>().PositionX;
                int prevY = EntityManager.Entities[id].GetComponent<PositionComponent>().PositionY;
                bool moved = false;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        moved = SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 0, -1, prevX, prevY);
                        if (moved)
                            EntityManager.Entities[id].GetComponent<PositionComponent>().PositionY -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        moved = SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 0, 1, prevX, prevY);
                        if (moved)
                            EntityManager.Entities[id].GetComponent<PositionComponent>().PositionY += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        moved = SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, -1, 0, prevX, prevY);
                        if (moved)
                            EntityManager.Entities[id].GetComponent<PositionComponent>().PositionX -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                        moved = SystemManager.RenderSystem.MoveTo(EntityManager.Entities[id].GetComponent<CharacterComponent>().CharacterSymbol, 1, 0, prevX, prevY);
                        if (moved)
                            EntityManager.Entities[id].GetComponent<PositionComponent>().PositionX += 1;
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
