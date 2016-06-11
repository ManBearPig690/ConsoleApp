﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class Tile
    {
        public bool Blocked;
        public bool BlockSight;

        public Tile(bool blocked, bool? blockSight = null)
        {
            Blocked = blocked;
            if (blockSight == null)
                blockSight = blocked;

            BlockSight = blockSight.Value;
        }
    }
}
