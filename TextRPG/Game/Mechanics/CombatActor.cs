namespace TextRPG.Game.Mechanics
{
    public class CombatActor : IStats, IDamageable, IDamageDealer
    {
        public CombatActor Target;
        public IStats Stats { get; private set; }
        public INameable Name { get; private set; }
        public IDamageable Damageable { get; private set; }
        IDamageDealer DamageDealer;

        public CombatActor(INameable name, IStats stats, IDamageable damageable, IDamageDealer dealer)
        {
            Name = name;
            Target = null;
            Stats = stats;
            Damageable = damageable;
            DamageDealer = dealer;
        }

        public bool IsAlive => Damageable.IsAlive;
        public bool AttemptRetreat { get; set; }

        public Damage Damage()
        {
            return DamageDealer.Damage();
        }

        public void Kill()
        {
            Damageable.Kill();
        }

        public void ReceiveDamage(ref Damage damage)
        {
            Damageable.ReceiveDamage(ref damage);
        }

        public StatsVisitorResult Visit(IStatsVisitor visitor)
        {
            return Stats.Visit(visitor);
        }

        public int CalculateHealth()
        {
            return Stats.CalculateHealth();
        }

        public string DescribeDamageable()
        {
            return Damageable.DescribeDamageable();
        }
    }
}