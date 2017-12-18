using System;

namespace TextRPG.Game.Mechanics
{
    public class CombatInitiativeStatsVisitorResult : StatsVisitorResult
    {
        public int Initiative;
    }

    public class CombatInitiativeStatsVisitor : IStatsVisitor
    {
        public StatsVisitorResult Visit(IStats stats)
        {
            return stats.Visit(this);
        }

        public StatsVisitorResult Visit(BasicStats stats)
        {
            return new CombatInitiativeStatsVisitorResult
            {
                Initiative = stats.Agility + stats.Level
            };
        }

        public int Calculate(IStats stats)
        {
            var result = Visit(stats) as CombatInitiativeStatsVisitorResult;
            if(result == null)
            {
                throw new NullReferenceException(
                    "[CombatInitiativeStatsVisitor] Visiting " 
                    + stats.ToString()  + 
                    " resulted in null result");
            }

            return result.Initiative;
        }
    }
}