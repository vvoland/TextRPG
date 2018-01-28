using System;
using System.Collections.Generic;
using System.Linq;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewLocation : View
    {
        private Location Location;
        private PlayerEntity Player;
        private GUISystem GUI;
        private Label CityText;
        private LinearLayout ButtonsLayout;

        public ViewLocation(GameSystem game, RenderSystem renderer, PlayerEntity player, Location location) : base(game, renderer)
        {
            Location = location;
            Player = player;
            InitUI();
        }

        private void InitUI()
        {
            GUI = new GUISystem();
            string text = string.Format("You are in the {0}\nWhat do you want to do?", Location.DisplayName);
            CityText = new Label(text, GetScreenPoint(0.5f, 0.25f));
            ButtonsLayout = new LinearLayout(LayoutDirection.Vertical, GetScreenPoint(0.5f, 0.75f), Vector2f.Center);
            ButtonsLayout.Size = GetScreenPoint(0.5f, 0.35f);

            foreach(var interactable in Location.Interactables)
            {
                var button = new GUIButton(Vector2.Zero, interactable.MenuText, () => interactable.Interact(Game));
                ButtonsLayout.Add(button);
                GUI.Add(button);
            }
            var travel = new GUIButton(Vector2.Zero, "Travel", () => Travel());
            ButtonsLayout.Add(travel);
            GUI.Add(travel);
        }

        private void Travel()
        {
            List<MenuOption> options = new List<MenuOption>();
            foreach(var link in Location.Links)
            {
                options.Add(new MenuOption
                {
                    Text = link.DisplayName,
                    Callback = () => Game.TravelTo(link)
                });
            }

            options.Add(new MenuOption
                {
                    Text = "None",
                    Callback = () => Game.PopView()
                });
            
            var view = new ViewMessage(Game, Renderer, "Choose you destination", options);
            Game.PushView(view);
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(CityText);
            Renderer.Render(ButtonsLayout);
        }
    }
}