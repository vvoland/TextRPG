using System.Collections.Generic;

namespace TextRPG.Game
{
    public static class Professions
    {

        public static readonly Profession Warrior = new Profession("Warrior",
            new BasicStats
            {
                Level = 1,

                Strength = 8,
                Agility = 4,
                Charisma = 3,
            }, 1, 0, 1);
        public static readonly Profession Rogue = new Profession("Rogue",
            new BasicStats
            {
                Level = 1,
                
                Strength = 3,
                Agility = 8,
                Charisma = 5,
            }, 0, 1, 1);


        public static readonly List<Profession> All = new List<Profession>
        {
            Warrior,
            Rogue
        };

    }
}