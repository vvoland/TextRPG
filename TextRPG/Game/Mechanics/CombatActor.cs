namespace TextRPG.Game.Mechanics
{
    public class CombatActor
    {
        public CombatActor Target;
        public IStats Stats { get; private set; }
        public INameable Name { get; private set; }
        public IDamageable Damageable { get; private set; }
        public IDamageDealer DamageDealer { get; private set; }

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
    }
}