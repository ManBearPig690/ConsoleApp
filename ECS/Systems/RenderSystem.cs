﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Entities;
using ECS.Component;

namespace ECS.Systems
{
    /// <summary>
    /// draws sprites
    /// </summary>
    public class RenderSystem : System
    {
        public override void Update(float dt, ref List<string> componentEntityList)
        {
            //// uses sprite and position component
            //// will update
            foreach (var entityId in componentEntityList)
            {
                // may not be needed
            }
        }
    }
}
