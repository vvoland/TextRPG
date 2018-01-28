namespace TextRPG.Game
{
    public class PlayerEntity : HumanEntity
    {
        public Experience Experience;
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
            Experience = new Experience(Stats, Profession);
            Inventory.Gold = 100;
        }
    }
}