namespace TextRPG.Game
{
    public class Profession
    {
        public string Name
        {
            get;
            protected set;
        }

        public Profession(string name, BasicStats stats)
        {
            Name = name;
            BaseStats = stats;
        }

        public BasicStats BaseStats
        {
            get;
            protected set;
        }
    }
}