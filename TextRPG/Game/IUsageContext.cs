namespace TextRPG.Game
{
    public interface IUsageContext
    {
        bool CanUse(Item item);
        bool CanUse(ItemWeapon weapon);
        bool CanUse(ItemHealthy healthy);

        void Use(Item item);
        void Use(ItemWeapon weapon);
        void Use(ItemHealthy healthy);
    }
}