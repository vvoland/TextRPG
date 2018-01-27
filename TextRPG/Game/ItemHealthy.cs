namespace TextRPG.Game
{
    public class ItemHealthy : Item, IUsable
    {
        public int Power { get; private set; }

        public ItemHealthy(string name) : base(name, name+"s")
        {
        }

        public ItemHealthy SetPower(int power)
        {
            Power = power;
            return this;
        }

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