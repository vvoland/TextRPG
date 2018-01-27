using System.Collections.Generic;

namespace TextRPG.Game
{
    public class World
    {
        private List<Entity> Entities { get; set; }

        public World()
        {
            Entities = new List<Entity>();
        }

        public void Add(Entity entity)
        {
            Entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            Entities.Remove(entity);
        }

        public IEnumerable<Entity> GetEntities()
        {
            return Entities;
        }
    }
}