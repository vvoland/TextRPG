using System;
using System.Collections.Generic;
using TextRPG.Game.Views;

namespace TextRPG.Game
{
    public class Tavern : IInteractable
    {
        public int Cost { get; private set; }
        public int HealAmount { get; private set; }
        public string MenuText => "Visit Tavern";

        public Tavern()
        {
            Cost = 5;
            HealAmount = 10;
        }

        public void Interact(GameSystem game)
        {
            var options = new List<MenuOption>();
            options.Add(new MenuOption 
            {
                Text = "Yes",
                Callback = () => TryToRest(game)
            });
            options.Add(new MenuOption 
            {
                Text = "No",
                Callback = () => game.PopView()
            });

            var view = new ViewMessage(game, game.Renderer, $"Do you want to rest? ({Cost} gold)", options);
            game.PushView(view);
        }

        private void TryToRest(GameSystem game)
        {
            game.PopView();
            string msg;
            if(game.Player.Inventory.Gold < Cost)
            {
                msg = "You do not have that much gold!";
            }
            else
            {
                game.Player.Inventory.Gold -= Cost;
                game.Player.Damageable.Heal(HealAmount);
                msg = "You feel rested!";
            }
            game.PushView(new ViewMessageInfo(game, game.Renderer, msg, () => { }));
        }
    }
}