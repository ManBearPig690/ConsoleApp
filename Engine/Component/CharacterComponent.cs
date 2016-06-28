using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Component.Core;

namespace Engine.Component
{
    public class CharacterComponent : Component
    {
        public int Level { get; set; }
        public int MaxHitPoints { get; set; }
        public int HitPoints { get; set; }
        public int MaxMana { get; set; }
        public int Mana { get; set; }
        public int CurrentExp { get; set; }
        public string CharacterSymbol;
        public Race Race { get; set; }
        public Class FavoredClass { get; set; }
        public Dictionary<string, int> Skills { get; set; }  // <string> skill name <int> skill level
        public List<Feat> Feats { get; set; } 

        // Abilities
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }


        // Ability modifiers
        public int StrModifier 
        {
            get { return (Strength - 10)/2; }
        }

        public int DexModifier
        {
            get { return (Dexterity - 10)/2; }
        }

        public int ConModifier
        {
            get { return (Constitution - 10)/2; }
        }

        public int IntModifier
        {
            get { return (Intelligence - 10)/2; }
        }

        public int WisModifier
        {
            get { return (Wisdom - 10)/2; }
        }

        public int ChaModifier
        {
            get { return (Charisma - 10)/2; }
        }

        public CharacterComponent(string characterSymbol)
        {
            CharacterSymbol = characterSymbol;
            ComponentId = "CharacterComponent";
        }
    }
}
