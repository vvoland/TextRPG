using System.Collections.Generic;

namespace TextRPG.Game
{
    public class Location : INameable
    {
        public virtual string DisplayName
        {
            get
            {
                return Name;
            }
        }

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

        public ICollection<IInteractable> Interactables
        {
            get
            {
                return _Interactables;
            }
        }
        private List<IInteractable> _Interactables = new List<IInteractable>();

        private List<Location> _Links;
        private List<Entity> Entities = new List<Entity>();

        public Location(string name)
        {
            _Links = new List<Location>();
            Name = name;
        }

        public Location Add(IInteractable interactable)
        {
            _Interactables.Add(interactable);
            return this;
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