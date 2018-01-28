using System.Collections.Generic;

namespace TextRPG.Game
{
    public class City : Location
    {
        public override string DisplayName
        {
            get
            {
                return "City " + Name;
            }
        }

        public City(string name) : base(name)
        {
        }

    }
}