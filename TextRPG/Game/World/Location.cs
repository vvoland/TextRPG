using System.Collections.Generic;

namespace TextRPG.Game
{
    public class Location : INameable
    {
        public string Name { get; set; }
        public string PluralName
        {
            get => Name;
            set => Name = value;
        }

        private List<Entity> Entities = new List<Entity>();

        public Location(string name)
        {
            Name = name;
        }


    }
}