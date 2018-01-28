using System.Collections.Generic;
using TextRPG.Game.Mechanics;

namespace TextRPG.Game
{
    public class HumanEntity : Entity, IDamageable, IDamageDealer
    {
        public BasicStats Stats;
        public Inventory Inventory { get; set; }
        public HumanEquipment Equipment;

        public HealthDamageable Damageable
        {
            get;
            private set;
        }

        public bool IsAlive => Damageable.IsAlive;

        public HumanEntity()
        {
            Stats = new BasicStats();
            Inventory = new Inventory();
            Damageable = new HealthDamageable(Stats);
            Equipment = new HumanEquipment(Inventory);
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