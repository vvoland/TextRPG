namespace TextRPG
{
    public interface IUsable
    {
        bool CanUse(IUsageContext context);
        void Use(IUsageContext context);
    }
}