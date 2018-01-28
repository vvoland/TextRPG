using System;

namespace TextRPG.Game
{
    public class BasicStats : IStats
    {
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Charisma { get; set; }
        public int BaseHealth = 10;

        public BasicStats()
        {
            Level = Strength = Agility = Charisma = 1;
        }

        public BasicStats Clone()
        {
            return (BasicStats)MemberwiseClone();
        }

        public virtual StatsVisitorResult Visit(IStatsVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public int CalculateHealth()
        {
            return BaseHealth + Level * Strength;
        }
    }
}