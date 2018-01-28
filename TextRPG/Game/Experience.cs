using System;

namespace TextRPG.Game
{
    public class Experience
    {
        public int Points { get; protected set; }
        public int NextLevel { get; private set; }
        private BasicStats Stats;
        private Profession Profession;

        public Experience(BasicStats stats, Profession profession)
        {
            Stats = stats;
            Points = 0;
            NextLevel = 100;
            Profession = profession;
        }

        public void Add(int points)
        {
            Points += points;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if(Points >= NextLevel)
            {
                NextLevel = (int)(NextLevel * Math.Pow(1.05, Stats.Level));
                Points = 0;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Stats.Level++;
            Stats.Strength += Profession.StrengthLevelBonus;
            Stats.Agility += Profession.AgilityLevelBonus;
            Stats.Charisma += Profession.CharismaLevelBonus;
        }
    }
}