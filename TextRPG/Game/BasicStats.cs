namespace TextRPG.Game
{
    public class BasicStats : IStats
    {
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }

        public BasicStats()
        {
            Level = Strength = Agility = Intelligence = Charisma = 1;
        }

        public virtual StatsVisitorResult Visit(IStatsVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}