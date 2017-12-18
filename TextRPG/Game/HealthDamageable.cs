namespace TextRPG.Game
{
    public class HealthDamageable : IDamageable
    {
        public int Health { get; protected set; }
        public int MaxHealth { get; set; }

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        public void ReceiveDamage(ref Damage damage)
        {
            Health -= damage.Strength;
        }

        public void Kill()
        {
            Health = 0;
        }
    }
}