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

        public ICollection<Location> Links
        {
            get
            {
                return _Links;
            }
        }

        private List<Location> _Links;
        private List<Entity> Entities = new List<Entity>();

        public Location(string name)
        {
            _Links = new List<Location>();
            Name = name;
        }

        public void Link(Location other)
        {
            if(!_Links.Contains(other))
            {
                _Links.Add(other);
                other.Link(this);
            }
        }


    }
}