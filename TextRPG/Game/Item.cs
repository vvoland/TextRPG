namespace TextRPG.Game
{
    public class Item : INameable, IUsable
    {
        public string Name { get; set; }
        public string PluralName { get; set; }

        public Item(string name, string plural)
        {
            Name = name;
            PluralName = plural;
        }

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