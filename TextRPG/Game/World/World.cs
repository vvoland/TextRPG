using System.Collections.Generic;

namespace TextRPG.Game
{
    public class World
    {
        private List<Entity> Entities { get; set; }
        private List<Location> Locations { get; set; }

        public World()
        {
            Entities = new List<Entity>();
            Locations = new List<Location>();
        }

        public void Add(Entity entity)
        {
            Entities.Add(entity);
        }

        public void Add(Location location)
        {
            Locations.Add(location);
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