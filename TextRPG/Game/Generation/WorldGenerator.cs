using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Game.Generation
{
    public class WorldGenerator
    {
        private World World;
        private WorldDescription Description;
        private Random Random = new Random();
        private List<City> Cities;
        private List<Item> Items;
        private List<CreatureEntity> Creatures;

        public World Generate(WorldDescription desc, PlayerEntity player)
        {
            Description = desc;
            World = new World(desc);
            World.Add(player);

            GenerateItems();
            GenerateCreatures();
            GenerateCities();

            World.Items = Items;
            World.Cities = Cities;
            World.Creatures = Creatures;

            return World;
        }

        private void GenerateItems()
        {
            Items = new List<Item>
            {
                new ItemHealthy("Minor Health Potion").SetPower(5).SetCost(10),
                new ItemHealthy("Health Potion").SetPower(30).SetCost(20),
                new ItemHealthy("Large Health Potion").SetPower(100).SetCost(70),

                new ItemWeapon("Wooden Stick").SetStrength(1).SetSpeed(1.0f).SetCost(2),
                new ItemWeapon("Dagger").SetStrength(2).SetSpeed(2.0f).SetCost(10),
                new ItemWeapon("Staff").SetStrength(6).SetSpeed(0.15f).SetCost(25),
                new ItemWeapon("Short Sword").SetStrength(4).SetSpeed(1.0f).SetCost(30),
                new ItemWeapon("Axe").SetStrength(8).SetSpeed(0.9f).SetCost(50),
                new ItemWeapon("Sword").SetStrength(10).SetSpeed(0.9f).SetCost(70)
            };
        }

        private void GenerateCreatures()
        {
            Creatures = new List<CreatureEntity>
            {
                CreateCreature("Rat", level: 1, strength: 1, agility: 1),
                CreateCreature("Big Moth", level: 1, strength: 1, agility: 1),

                CreateCreature("Goblin", level: 2, strength: 2, agility: 2),
                CreateCreature("Spider", level: 2, strength: 1, agility: 3),

                CreateCreature("Wolf", level: 3, strength: 3, agility: 3),

                CreateCreature("Skeleton", level: 4, strength: 4, agility: 4),

                CreateCreature("Zombie", level: 5, strength: 7, agility: 2),

                CreateCreature("Wild Boar", level: 6, strength: 8, agility: 4),

                CreateCreature("Bear", level: 7, strength: 10, agility: 4),
                
                CreateCreature("Werewolf", level: 15, strength: 15, agility: 15),

                CreateCreature("Vampire", level: 30, strength: 30, agility: 30),
            };
        }

        private CreatureEntity CreateCreature(string name, int level, int strength, int agility, int charisma = 1)
        {
            var stats = new BasicStats
            {
                Level = level,
                Strength = strength,
                Agility = agility,
                Charisma = charisma
            };

            return new CreatureEntity(name, stats);
        }
    

        private void GenerateCities()
        {
            Cities = new List<City>();
            for (int i = 0; i < Description.CitiesCount; i++)
            {
                int nameId = Random.Next(Description.CitiesNames.Count);
                var name = Description.CitiesNames[nameId];
                var city = new City(name);

                Cities.Add(city);
            }

            LinkCities();
            PopulateCities();

            Cities.ForEach(c => World.Add(c));
        }

        private void PopulateCities()
        {
            foreach(var city in Cities)
            {
                city.Add(new Mayor());
                int vendors = Random.Next(Description.CitiesMaxVendors + 1);
                for(int i = 0; i < vendors; i++)
                    city.Add(CreateCityVendor());
                city.Add(new Tavern());
            }
        }

        private Vendor CreateCityVendor()
        {
            var vendor = new Vendor(Description.PeopleNames.Random());
            var weapons = Items.OfType<ItemWeapon>();
            for(int i = 0; i < 2; i++)
            {
                var weapon = weapons.ElementAt(Random.Next(weapons.Count()));
                vendor.Add(weapon);
            }
            return vendor;
        }

        private void LinkCities()
        {
            foreach (var city in Cities)
            {
                int desiredLinks = Random.Next(Description.CitiesMaxLinks) + 1;

                for (int i = city.Links.Count; i < desiredLinks; i++)
                {
                    var available = Cities
                        .Where(c => c.Links.Count < desiredLinks);
                    var link = available.ElementAt(Random.Next(available.Count()));

                    city.Link(link);
                }
            }
        }
    }
}