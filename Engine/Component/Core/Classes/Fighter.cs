using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Component.Core.Classes
{
    class Fighter : Class
    {
        public Fighter()
        {
            HitDie = Dice.D10;
        }
    }
}
