namespace TextRPG.Game
{
    public interface IUsable
    {
        bool CanUse(IUsageContext context);
        void Use(IUsageContext context);
    }
}