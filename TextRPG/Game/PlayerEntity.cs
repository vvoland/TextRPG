namespace TextRPG.Game
{
    public class PlayerEntity : HumanEntity
    {
        public Profession Profession
        {
            get;
            private set;
        }

        public PlayerEntity(string name, Profession profession)
        {
            Name = name;
            Profession = profession;
        }
    }
}