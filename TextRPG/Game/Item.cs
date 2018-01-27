namespace TextRPG.Game
{
    public class Item : INameable, IUsable
    {
        public string Name { get; set; }
        public string PluralName { get; set; }
        public int Cost { get; protected set; }

        public Item(string name, string plural)
        {
            Name = name;
            PluralName = plural;
            Cost = 1;
        }

        public Item SetCost(int cost)
        {
            Cost = cost;
            return this;
        }

        public virtual bool CanUse(IUsageContext context)
        {
            return false;
        }

        public virtual void Use(IUsageContext context)
        {
            context.Use(this);
        }

        public virtual Item Clone()
        {
            return (Item)MemberwiseClone();
        }
    }
}