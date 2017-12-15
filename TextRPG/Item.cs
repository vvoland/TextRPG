namespace TextRPG
{
    public class Item : INameable, IUsable
    {
        public string Name { get; set; }
        public string PluralName { get; set; }

        public virtual bool CanUse(IUsageContext context)
        {
            return false;
        }

        public virtual void Use(IUsageContext context)
        {
            context.Use(this);
        }
    }
}