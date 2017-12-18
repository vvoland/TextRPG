namespace TextRPG.Game
{
    public interface IStatsVisitor
    {
        StatsVisitorResult Visit(IStats stats);
        StatsVisitorResult Visit(BasicStats stats);
    }
}