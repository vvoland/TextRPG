namespace TextRPG.Game
{
    public class Entity : INameable
    {
        public string Name { get; set; }
        public string PluralName
        {
            get
            {
                if (_PluralSet)
                    return _Plural;
                return Name;
            }
            set
            {
                _Plural = value;
                _PluralSet = true;
            }
        }

        private string _Plural;
        private bool _PluralSet = false;
    }
}