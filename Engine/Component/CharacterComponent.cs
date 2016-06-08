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

        /* todo: create teh following
         * these don't have to be components just data containers systems may access this data however for calculations and such
         * stats
         * skills
         * abilities
         * items / Equipment
         * class => if doing d&d or pathfinder
         * 
         */

        public CharacterComponent(string characterSymbol)
        {
            CharacterSymbol = characterSymbol;
            ComponentId = "CharacterComponent";
        }
    }
}
