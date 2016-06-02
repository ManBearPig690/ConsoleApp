using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Component;
using Engine.Entities;
using Engine.Systems;
using System = Engine.Systems.System;

namespace Engine
{
    public static class SystemManager
    {
        public static List<string> PlayerComponentEntities;
        public static List<string> CharacterComponentEntities;
        public static List<string> InputComponentEntities;
        public static List<string> MotionComponentEntities;
        public static List<string> PositionComponentEntities;
        public static InputSystem InputSystem;
        public static MotionSystem MotionSystem;
        public static CollisionSystem CollisionSystem;
        public static RenderSystem RenderSystem;

        static SystemManager()
        {
            PlayerComponentEntities = new List<string>();
            CharacterComponentEntities = new List<string>();
            InputComponentEntities = new List<string>();
            MotionComponentEntities = new List<string>();
            PositionComponentEntities = new List<string>();
            MotionSystem = new MotionSystem();
            CollisionSystem = new CollisionSystem();
            InputSystem = new InputSystem();
            RenderSystem = new RenderSystem();
        }

        public static void CreateComponentLists(ref Dictionary<string, Entity> entities)
        {
            foreach (var entity in entities.Values)
            {
                
                var playerComp = entity.GetComponent<PlayerComponent>();
                var charComp = entity.GetComponent<CharacterComponent>();
                var inputComp = entity.GetComponent<InputComponent>();
                var motionComp = entity.GetComponent<MotionComponent>();
                var positionComp = entity.GetComponent<PositionComponent>();

                if (playerComp != null)
                    PlayerComponentEntities.Add(entity.EntityId);
                if (charComp != null)
                    CharacterComponentEntities.Add(entity.EntityId);
                if (inputComp != null)
                    InputComponentEntities.Add(entity.EntityId);
                if (motionComp != null)
                    MotionComponentEntities.Add(entity.EntityId);
                if (positionComp != null)
                    PositionComponentEntities.Add(entity.EntityId);
            }
        }

        /// <summary>
        /// removes the entity id from each of hte system's id list
        /// </summary>
        /// <param name="entitIds"></param>
        public static void RemoveEntity(ref List<string> entitIds)
        {
            foreach (var id in entitIds)
            {
                var itemToRemove = PlayerComponentEntities.SingleOrDefault(c => c == id);
                if (itemToRemove != null)
                    PlayerComponentEntities.Remove(itemToRemove);
            }
            
        }

    }
}
