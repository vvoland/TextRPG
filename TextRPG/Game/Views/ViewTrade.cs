using System;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewTrade : View
    {
        private string Text;
        private Vendor Vendor;

        private GUISystem GUI;
        private Label Greeting;
        private LinearLayout ProductsLayout;
        

        public ViewTrade(GameSystem game, RenderSystem renderer, string text, Vendor vendor) : base(game, renderer)
        {
            Text = text;
            Vendor = vendor;
            InitUI();
        }

        private void InitUI()
        {
            GUI = new GUISystem();
            Greeting = new Label(Text, GetScreenPoint(0.5f, 0.25f));
            ProductsLayout = new LinearLayout(LayoutDirection.Vertical, GetScreenPoint(0.5f, 0.75f), Vector2f.Center);
            ProductsLayout.Size = GetScreenPoint(0.4f, 0.4f);

            foreach(var group in Vendor.GroupedProducts)
            {
                Item item = group.Item1;
                int count = group.Item2;
                string text = GetItemLabel(item, count);
                var button = new GUIButton(Vector2.Zero, text, () => TryBuy(item));
                ProductsLayout.Add(button);
                GUI.Add(button);
            }

            var leave = new GUIButton(Vector2.Zero, "Back", () => Game.PopView());
            ProductsLayout.Add(leave);
            GUI.Add(leave);
        }

        private void TryBuy(Item item)
        {
            string fmt;
            if(Vendor.Buy(item, Game.Player.Inventory))
            {
                fmt = "You bought {0} for {1} gold!";
            }
            else
            {
                fmt = "You dont have that much gold!";
            }
            string text = string.Format(fmt, item.Name, item.Cost);
            InitUI();
            Game.PushView(new ViewMessageInfo(Game, Renderer, text, () => {}));
        }

        private string GetItemLabel(Item item, int count)
        {
            string fmt;
            if(count == -1 || count == 1)
                fmt = "{0} - {2} gold";
            else
                fmt = "{0} - {2} gold ({3} left)";
            return string.Format(fmt, item.Name, item.PluralName, item.Cost, count);
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(Greeting);
            Renderer.Render(ProductsLayout);
        }
    }
}