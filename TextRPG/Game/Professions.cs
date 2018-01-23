using System.Collections.Generic;

namespace TextRPG.Game
{
    public static class Professions
    {

        public static readonly Profession Warrior = new Profession("Warrior",
            new BasicStats
            {
                Level = 1,

                Agility = 4,
                Strength = 8,
                Charisma = 3,
                Intelligence = 3,
            });
        public static readonly Profession Mage = new Profession("Mage",
            new BasicStats
            {
                Level = 1,

                Agility = 4,
                Strength = 3,
                Charisma = 5,
                Intelligence = 8,
            });
        public static readonly Profession Rogue = new Profession("Rogue",
            new BasicStats
            {
                Level = 1,

                Agility = 8,
                Strength = 3,
                Charisma = 5,
                Intelligence = 4,
            });


        public static readonly List<Profession> All = new List<Profession>
        {
            Warrior,
            Mage,
            Rogue
        };

    }
}