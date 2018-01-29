using System.Collections.Generic;
using TextRPG.Game.Generation;

namespace TextRPG.Game
{
    public class World
    {
        public List<Item> Items;
        public List<City> Cities;
        public List<CreatureEntity> Creatures;
        public WorldDescription Description { get; }

        private List<Entity> Entities { get; set; }
        private List<Location> Locations { get; set; }

        public World(WorldDescription description)
        {
            Description = description;
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