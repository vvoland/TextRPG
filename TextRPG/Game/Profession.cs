namespace TextRPG.Game
{
    public class Profession
    {
        public int StrengthLevelBonus { get; set; }
        public int AgilityLevelBonus { get; internal set; }
        public int CharismaLevelBonus { get; internal set; }

        public string Name
        {
            get;
            protected set;
        }

        public Profession(string name, BasicStats stats, int strLevel, int agiLevel, int chaLevel)
        {
            Name = name;
            BaseStats = stats;
            StrengthLevelBonus = strLevel;
            AgilityLevelBonus = agiLevel;
            CharismaLevelBonus = chaLevel;
        }

        public BasicStats BaseStats
        {
            get;
            protected set;
        }

    }
}