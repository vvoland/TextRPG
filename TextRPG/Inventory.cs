using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG
{
    public class Inventory : IInventory
    {
        public int Size 
        {
            get 
            {
                return Items.Count;
            }
        }

        private List<Item> Items = new List<Item>();

        public Inventory()
        {
        }

        public void Add(Item item)
        {
            Items.Add(item);
        }

        public int Count(Item item)
        {
            return Count(i => i.Equals(item));
        }

        public int Count(Predicate<Item> predicate)
        {
            return Items.Count(i => predicate(i));
        }

        public bool Has(Item item)
        {
            return Has(i => i.Equals(item));
        }

        public bool Has(Predicate<Item> predicate)
        {
            return Items.Any(i => predicate(i));
        }

        public bool Remove(Item item)
        {
            return Items.Remove(item);
        }

        public bool Remove(Predicate<Item> predicate)
        {
            var toRemove = Items
                .Where(i => predicate(i))
                .FirstOrDefault();

            if(toRemove == null)
                return false;

            return Items.Remove(toRemove);
        }

        public int RemoveAll(Predicate<Item> predicate)
        {
            return Items.RemoveAll(predicate);
        }
    }
}