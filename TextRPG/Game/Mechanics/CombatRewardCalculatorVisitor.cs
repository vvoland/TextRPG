using System;

namespace TextRPG.Game.Mechanics
{
    public class CombatRewardCalculatorVisitorResult : StatsVisitorResult
    {
        public CombatRewardCalculatorVisitorResult(int gold)
        {
            this.Gold = gold;

        }
        public int Gold { get; private set; }
    }

    public class CombatRewardCalculatorVisitor : IStatsVisitor
    {
        public static Random Rng = new Random();
        public StatsVisitorResult Visit(IStats stats)
        {
            return stats.Visit(this);
        }

        public StatsVisitorResult Visit(BasicStats stats)
        {
            int statSum = stats.Level + stats.Strength + stats.Agility + stats.Charisma;
            float mul = (float)(Rng.NextDouble() + 1.0);
            return new CombatRewardCalculatorVisitorResult((int)(5 + statSum * mul));
        }
    }
}