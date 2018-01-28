namespace TextRPG.Game
{
    public class PlayerEntity : HumanEntity
    {
        public Location Location { get; set; }
        public Profession Profession
        {
            get;
            private set;
        }

        public PlayerEntity(string name, Profession profession)
        {
            Name = name;
            Profession = profession;
            Stats = Profession.BaseStats.Clone();
            Inventory.Gold = 100;
        }
    }
}