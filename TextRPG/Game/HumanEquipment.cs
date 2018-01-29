using System;
using TextRPG.Game.Mechanics;

namespace TextRPG.Game
{
    public class HumanEquipment : IDamageDealer
    {
        public ItemWeapon Weapon
        {
            get;
            protected set;
        }

        private IInventory Inventory;

        public HumanEquipment(IInventory inventory)
        {
            Inventory = inventory;
        }

        public void Equip(ItemWeapon weapon)
        {
            if(Inventory.Has(weapon))
                Weapon = weapon;
        }

        public void Unequip(ItemWeapon weapon)
        {
            if(Weapon == weapon)
                Weapon = null;
        }

        public Damage Damage()
        {
            if(Weapon == null)
                return new Damage { Strength = 0 };
            var ctx = new CombatUsageContext();
            Weapon.Use(ctx);
            return ctx.Damage;
        }
    }
}