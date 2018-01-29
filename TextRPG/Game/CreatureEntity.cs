using System;
using TextRPG.Game.Mechanics;

namespace TextRPG.Game
{
    public class CreatureEntity : Entity, IDamageable, IDamageDealer
    {
        public BasicStats Stats
        {
            get;
            protected set;
        }
        private HealthDamageable Damageable;

        public CreatureEntity(string name, BasicStats stats)
        {
            Name = name;
            Stats = stats;
            Damageable = new HealthDamageable(stats);
        }

        public bool IsAlive => Damageable.IsAlive;

        public CreatureEntity Clone()
        {
            CreatureEntity entity = new CreatureEntity(Name, Stats.Clone());
            return entity;
        }

        public Damage Damage()
        {
            return new Damage
            {
                Strength = Stats.Strength + Stats.Level
            };
        }

        public void Kill()
        {
            Damageable.Kill();
        }

        public void ReceiveDamage(ref Damage damage)
        {
            Damageable.ReceiveDamage(ref damage);
        }

        public string DescribeDamageable()
        {
            return Damageable.DescribeDamageable();
        }
    }
}