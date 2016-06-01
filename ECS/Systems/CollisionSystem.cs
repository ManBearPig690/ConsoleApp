using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Entities;
using ECS.Component;


namespace ECS.Systems
{
    /// <summary>
    /// gets enties with collision component 
    /// checks for bounding box rules
    /// does collsion logic
    /// 
    /// </summary>
    public class CollisionSystem : System
    {
        private const float MinXVelocity = -300;
        private const float MaxXVelocity = 300;

        public Tuple<bool, float, float> Update(float dt, ref List<string> componentEntityList, float screenLeft, float screenRight, float minY)
        {
            // item 1 - bool => indicates collsion
            // item 2 - int => X coordinate
            // item 3 - int => Y coordinate
            var collision = new Tuple<bool, float, float>(false, 0f, 0f);

            // logic to see if occupied or not

            return collision;

        }
    }
}
