using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Component
{
    public class MotionComponent : Component
    {
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }

        public MotionComponent(int vX, int vY)
        {
            VelocityX = vX;
            VelocityY = vY;
        }
    }
}
