using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TextRPG.Game.Views;
using TextRPG.GUI;
using System;

namespace TextRPG.Game
{
    public class Vendor : IInteractable, INameable
    {
        public IEnumerable<Item> Products
        {
            get
            {
                return UnlimitedItems
                    .Concat(Inventory);
            }
        }

        public IEnumerable<Tuple<Item, int>> GroupedProducts
        {
            get
            {
                var items = Inventory
                    .GroupBy(i => i.Name)
                    .Select(g => new Tuple<Item, int>(g.First(), g.Count()));
                return UnlimitedItems
                    .Select(i => new Tuple<Item, int>(i, -1))
                    .Concat(items);
            }
        }

        public bool AlreadyMet
        {
            get;
            private set;
        }

        public string MenuText => "Visit merchant " + Name;

        public string Name { get; set; }
        public string PluralName { get; set; }

        private Inventory Inventory = new Inventory();
        private HashSet<Item> UnlimitedItems = new HashSet<Item>();

        public Vendor(string name)
        {
            AlreadyMet = false;
            Name = name;
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

        public void Interact(GameSystem game)
        {
            string text;

            if(AlreadyMet)
                text = "Hello again {0}! Do you want to make a deal?";
            else
                text = "Hello stranger. My name is {1}. Have a look at my wares!";

            text = string.Format(text, game.Player.Name, Name);
            var view = new ViewTrade(game, game.Renderer, text, this);
            game.PushView(view);
        }
    }
}