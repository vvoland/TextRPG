namespace TextRPG.Game.Mechanics
{
    public class CombatUsageContext : IUsageContext
    {
        public Damage Damage { get; private set; }

        public bool CanUse(Item item)
        {
            return false;
        }

        public bool CanUse(ItemWeapon weapon)
        {
            return true;
        }

        public bool CanUse(ItemHealthy healthy)
        {
            return false;
        }

        public void Use(Item item)
        {
        }

        public void Use(ItemWeapon weapon)
        {
            Damage = new Damage
            {
                Strength = weapon.Strength
            };
        }

        public void Use(ItemHealthy healthy)
        {
        }
    }
}