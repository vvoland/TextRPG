namespace TextRPG.Game.Mechanics
{
    public class CombatActor : IStats, IDamageable, IDamageDealer
    {
        public IStats Stats { get; private set; }
        IDamageable Damageable;
        IDamageDealer DamageDealer;

        public CombatActor(IStats stats, IDamageable damageable, IDamageDealer dealer)
        {
            Stats = stats;
            Damageable = damageable;
            DamageDealer = dealer;
        }

        public bool IsAlive => Damageable.IsAlive;

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
    }
}