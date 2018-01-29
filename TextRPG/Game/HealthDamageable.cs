namespace TextRPG.Game
{
    public class HealthDamageable : IDamageable
    {
        public int Health { get; protected set; }
        public int MaxHealth
        {
            get
            {
                return Stats.CalculateHealth();
            }
        }

        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        private IStats Stats;

        public HealthDamageable(IStats stats)
        {
            Stats = stats;
            Health = stats.CalculateHealth();
        }

        public void Heal(int amount)
        {
            Health += amount;
            if(Health > MaxHealth)
                Health = MaxHealth;
        }

        public void ReceiveDamage(ref Damage damage)
        {
            Health -= damage.Strength;
        }

        public string DescribeDamageable()
        {
            return string.Format("{0}/{1}", Health, MaxHealth);
        }

        public void Kill()
        {
            Health = 0;
        }
    }
}