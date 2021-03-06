using System;
using System.Collections.Generic;

namespace TextRPG.Game
{
    public interface IInventory : IEnumerable<Item>
    {
        int Gold { get; set; }
        int Size { get; }
        void Add(Item item);
        bool Remove(Item item);
        bool Remove(Predicate<Item> predicate);
        int RemoveAll(Predicate<Item> predicate);
        bool Has(Item item);
        bool Has(Predicate<Item> predicate);
        int Count(Item item);
        int Count(Predicate<Item> predicate);
    }
}