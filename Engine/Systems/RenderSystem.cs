using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities;
using Engine.Component;

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

        public void MoveTo(string s, int x, int y)
        {
            try
            {
                if (y == -1 && Console.CursorTop == 0) return;
                if (x == -1 && Console.CursorLeft == 0) return;
                Console.Clear();
                Console.SetCursorPosition(Engine.OrigX += x, Engine.OrigY += y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public void WriteAt(string s, int x, int y)
        {
            // for writing anyting to anywhere

        }
    }
}
