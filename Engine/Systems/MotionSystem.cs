using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Component;
using Engine.Entities;

namespace Engine.Systems
{
    public class MotionSystem : System
    {
        // might need to set frame rate/ update in secons some where get how many animation frames
        public override void Update(int dt, ref List<string> componentEntityList)
        {
            foreach (var entityId in componentEntityList)
            {
                EntityManager.Entities[entityId].GetComponent<PositionComponent>().PositionX += EntityManager.Entities[entityId].GetComponent<MotionComponent>().VelocityX * dt; 
                EntityManager.Entities[entityId].GetComponent<PositionComponent>().PositionY += EntityManager.Entities[entityId].GetComponent<MotionComponent>().VelocityY * dt;
            }
        }


    }
}
