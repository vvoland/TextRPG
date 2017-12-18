using System.Collections.Generic;
using TextRPG.Game.Mechanics;

namespace TextRPG.Game
{
    public class HumanEntity : Entity, IDamageable, IDamageDealer
    {
        public BasicStats Stats;
        public List<Item> Inventory { get; set; }

        private HealthDamageable Damageable;

        public bool IsAlive => Damageable.IsAlive;

        public HumanEntity()
        {
            Stats = new BasicStats();
            Inventory = new List<Item>();
            Damageable = new HealthDamageable();
        }

        public void Kill()
        {
            Damageable.Kill();
        }

        public void ReceiveDamage(ref Damage damage)
        {
            Damageable.ReceiveDamage(ref damage);
        }

        public Damage Damage()
        {
            throw new System.NotImplementedException();
        }
    }
}