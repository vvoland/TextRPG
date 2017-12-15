namespace TextRPG
{
    public class ItemWeapon : Item
    {

        public override bool CanUse(IUsageContext context)
        {
            return context.CanUse(this);
        }

        public override void Use(IUsageContext context)
        {
            context.Use(this);
        }
    }

}