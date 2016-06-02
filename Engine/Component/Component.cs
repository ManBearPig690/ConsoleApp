using System;

namespace Engine.Component
{
    public class Component
    {
        public string ComponentId; 

        public Component()
        {
            ComponentId = Guid.NewGuid().ToString();
        }
    }
}
