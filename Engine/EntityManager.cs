using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Component;
using Engine.Entities;

namespace Engine
{
    /// <summary>
    /// creates and manages entities, could pass params so that they could in turn
    /// be passed to the corrisponding components depending on the entity
    /// </summary>
    public static class EntityManager
    {

        public static Dictionary<string, Entity> Entities;
        public static string BallEntity;

        public static void Initialize()
        {
            Entities = new Dictionary<string, Entity>();    
        }
        
        public static void CreatePlayerEntity(int pX, int pY, int vX, int vY, string characterSymbol)
        {
            var playerEntity = new Entities.Entity();
            playerEntity.AddComponent(new PlayerComponent());
            playerEntity.AddComponent(new CharacterComponent(characterSymbol));
            playerEntity.AddComponent(new PositionComponent(pX, pY));
            playerEntity.AddComponent(new MotionComponent(vX, vY));
            playerEntity.AddComponent(new InputComponent());
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
