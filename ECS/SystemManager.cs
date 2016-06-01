using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Component;
using ECS.Entities;
using ECS.Systems;
using System = ECS.Systems.System;

namespace ECS
{
    public class SystemManager
    {
        public List<string> PlayerComponentEntities;

        public SystemManager()
        {
            PlayerComponentEntities = new List<string>();
        }

        public void CreateComponentLists(ref Dictionary<string, Entity> entities)
        {
            foreach (var entity in entities.Values)
            {
                
                var playerComp = entity.GetComponent<PlayerComponent>();
                

                if (playerComp != null)
                    PlayerComponentEntities.Add(entity.EntityId);

            }
        }

        /// <summary>
        /// removes the entity id from each of hte system's id list
        /// </summary>
        /// <param name="entitIds"></param>
        public void RemoveEntity(ref List<string> entitIds)
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
