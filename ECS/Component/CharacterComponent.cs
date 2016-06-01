using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Component
{
    public class CharacterComponent : Component
    {
        public int Level;
        public int HitPoints;
        public int Mana;
        public int Exp;
        // stats
        // skills
        // abilities
        // items / Equipment

        public CharacterComponent()
        {
            ComponentId = "CharacterComponent";
        }
    }
}
