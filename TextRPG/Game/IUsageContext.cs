namespace TextRPG.Game
{
    public interface IUsageContext
    {
        bool CanUse(Item item);
        bool CanUse(ItemWeapon weapon);

        void Use(Item item);
        void Use(ItemWeapon weapon);
    }
}