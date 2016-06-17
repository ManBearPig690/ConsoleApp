using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Component.Core
{
    public class Race
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public int Speed { get; set; }
        public Dictionary<string, int> AbilityBonus { get; set; }
        public Dictionary<string, int> AbilityPenalty { get; set; }
        public Dictionary<RaceSenses, int> Senses { get; set; } // int spcifies the distance / duration
        public List<Traits> DefensieTraits { get; set; }
        public List<Traits> OffensiveTraits { get; set; }
        public List<Skills> SkillBonuses { get; set; }
        public List<Feat> BonusFeats { get; set; }
        public List<Spell> SpellLikeAbility { get; set; } 
    }
}
