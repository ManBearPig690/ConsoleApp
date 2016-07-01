using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Component.Core
{
	// all core classes will be derived from this class
	public class Class
	{
		public int Level { get; set; } // classes can again in level when the player levels -> if using 1 favored class it will be the same as the PC's level
		public int BaseAttackBonus { get; set; }
		public int TotalAttacks { get; set; }

	}
}
