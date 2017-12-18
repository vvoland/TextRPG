namespace TextRPG.Game
{
    public interface IStats
    {
        StatsVisitorResult Visit(IStatsVisitor visitor);
    }
}