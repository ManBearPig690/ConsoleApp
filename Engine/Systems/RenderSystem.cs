using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities;
using Engine.Component;
using Generator;

namespace Engine.Systems
{
    /// <summary>
    /// draws sprites
    /// </summary>
    public class RenderSystem : System
    {
        public override void Update(int dt, ref List<string> componentEntityList)
        {
            //// uses sprite and position component
            //// will update
            foreach (var entityId in componentEntityList)
            {
                // may not be needed
            }
        }

        public bool MoveTo(string s, int x, int y, int prevX, int prevY)
        {
            try
            {
                if (y == -1 && Engine.OrigY == 0) return false;
                if (x == -1 && Engine.OrigX == 0) return false;
                if (Engine.Dungeon.Map[Engine.OrigX + x, Engine.OrigY + y].Blocked) return false; // cant traverse

                Console.SetCursorPosition(Engine.OrigX += x, Engine.OrigY += y);
                Console.Write(s);
                Console.SetCursorPosition(prevX, prevY);
                Console.Write(".");
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void WriteAt(string s, int x, int y)
        {
            // for writing anyting to anywhere
            Console.SetCursorPosition(Engine.OrigX += x, Engine.OrigY += y);
            Console.Write(s);
        }

        public void RenderPlayer(ref List<string> componentEntityList)
        {
            foreach (var player in componentEntityList)
            {
                Console.SetCursorPosition(EntityManager.Entities[player].GetComponent<PositionComponent>().PositionX,
                    EntityManager.Entities[player].GetComponent<PositionComponent>().PositionY);
                Console.Write(EntityManager.Entities[player].GetComponent<CharacterComponent>().CharacterSymbol);
            }
        }

        public void RenderMap(ref Dungeon dungeon)
        {
            for (int x = 0; x < dungeon.Width; x++)
            {
                for (int y = 0; y < dungeon.Height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(dungeon.Map[x, y].Blocked == false ? "." : "");
                }
            }
        }
    }
}
