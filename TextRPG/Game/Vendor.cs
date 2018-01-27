using System.Collections;
using System.Collections.Generic;

namespace TextRPG.Game
{
    public class Vendor
    {
        public ICollection Products
        {
            get
            {
                return Inventory;
            }
        }

        private Inventory Inventory = new Inventory();
        private HashSet<Item> UnlimitedItems = new HashSet<Item>();

        public Vendor()
        {
        }

        public Vendor Add(Item item, int count = 1)
        {
            for(int i = 0; i < count; i++)
                Inventory.Add(item.Clone());
            return this;
        }

        public Vendor AddUnlimited(Item item)
        {
            UnlimitedItems.Add(item);
            return this;
        }

        public bool Sell(Item item, IInventory seller)
        {
            int cost = item.Cost;
            if(!seller.Has(item))
                return false;
            if(Inventory.Gold < cost)
                return false;

            Inventory.Add(item);
            seller.Remove(item);
            
            seller.Gold += cost;
            Inventory.Gold -= cost;
            return true;
        }

        public bool Buy(Item item, IInventory buyer)
        {
            if(buyer.Gold < item.Cost)
                return false;
            
            if(!UnlimitedItems.Contains(item))
            {
                Inventory.Remove(item);
                buyer.Add(item);
            }
            else
            {
                buyer.Add(item.Clone());
            }
            buyer.Gold -= item.Cost;
            Inventory.Gold += item.Cost;

            return true;
        }

    }
}