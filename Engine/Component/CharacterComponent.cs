using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Component
{
    public class CharacterComponent : Component
    {
        public int Level;
        public int HitPoints;
        public int Mana;
        public int Exp;
        public string CharacterSymbol;

        // stats
        // skills
        // abilities
        // items / Equipment

        public CharacterComponent(string characterSymbol)
        {
            CharacterSymbol = characterSymbol;
            ComponentId = "CharacterComponent";
        }
    }
}
