using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Component;
using ECS.Entities;

namespace ECS
{
    /// <summary>
    /// creates and manages entities, could pass params so that they could in turn
    /// be passed to the corrisponding components depending on the entity
    /// </summary>
    public static class EntityManager
    {

        public static Dictionary<string, Entity> Entities;
        public static string BallEntity;

        static EntityManager()
        {
            Entities = new Dictionary<string, Entity>();
        }

        
        public static void CreatePlayerEntity(float pX, float pY, float vX, float vY, string fileName)
        {
            var playerEntity = new Entities.Entity();
            playerEntity.AddComponent(new PlayerComponent());
            Entities.Add(playerEntity.EntityId, playerEntity);
        }

        

        public static void DestroyEntity(ref List<string> entities)
        {
            foreach (var entityId in entities)
            {
                Entities.Remove(entityId);    
            }
            
        }

        public static void EntitesToDestroy(ref List<string> entityDestructionList)
        {
            entityDestructionList = (from entity in Entities.Values where entity.DestroyEntity select entity.EntityId).ToList();
            // return list  logic
            //return (from entity in Entities.Values where entity.DestroyEntity select entity.EntityId).ToList();
        }
    }
}
