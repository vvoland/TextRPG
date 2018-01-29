namespace TextRPG.Game.Mechanics
{
    public class ExperienceCalculatorVisitorResult : StatsVisitorResult
    {
        public ExperienceCalculatorVisitorResult(int experience)
        {
            this.Experience = experience;

        }
        public int Experience { get; private set; }
    }

    public class ExperienceCalculatorVisitor : IStatsVisitor
    {
        public StatsVisitorResult Visit(IStats stats)
        {
            return stats.Visit(this);
        }

        public StatsVisitorResult Visit(BasicStats stats)
        {
            return new ExperienceCalculatorVisitorResult(stats.Level + stats.Strength + stats.Agility + stats.Charisma);
        }
    }
}