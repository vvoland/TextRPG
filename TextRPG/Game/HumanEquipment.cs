using TextRPG.Game.Mechanics;

namespace TextRPG.Game
{
    public class HumanEquipment
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

    }
}