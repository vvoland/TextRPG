using System.Collections.Generic;

namespace TextRPG.Game
{
    public class World
    {
        private List<WorldEntity> Entities { get; set; }

        public void Add(WorldEntity entity)
        {
            entity.World = this;
            Entities.Add(entity);
        }

        public void Remove(WorldEntity entity)
        {
            if(entity.World == this)
                entity.World = null;
            Entities.Remove(entity);
        }

        public IEnumerable<Entity> GetEntities()
        {
            return Entities;
        }
    }
}