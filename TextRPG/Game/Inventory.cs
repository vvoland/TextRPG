using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Game
{
    public class Inventory : IInventory, ICollection, IEnumerable<Item>
    {
        public int Size 
        {
            get 
            {
                return Items.Count;
            }
        }

        public int Gold { get; set; }

        int ICollection.Count => ((ICollection)Items).Count;

        public bool IsSynchronized => ((ICollection)Items).IsSynchronized;

        public object SyncRoot => ((ICollection)Items).SyncRoot;

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

        public void CopyTo(Array array, int index)
        {
            ((ICollection)Items).CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return ((ICollection)Items).GetEnumerator();
        }

        IEnumerator<Item> IEnumerable<Item>.GetEnumerator()
        {
            return ((IEnumerable<Item>)Items).GetEnumerator();
        }
    }
}