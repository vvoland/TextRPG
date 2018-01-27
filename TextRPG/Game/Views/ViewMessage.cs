using System;
using System.Collections.Generic;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class MenuOption
    {
        public string Text;
        public Action Callback;
    }

    public class ViewMessage : View
    {
        private GUISystem GUI;
        private Label Text;
        private LinearLayout ButtonsLayout;

        public ViewMessage(GameSystem game, RenderSystem renderer, 
            string text,
            IList<MenuOption> options)
            : base(game, renderer)
        {
            GUI = new GUISystem();
            Text = new Label(text, GetScreenPoint(0.5f, 0.25f));
            ButtonsLayout = new LinearLayout(LayoutDirection.Vertical, GetScreenPoint(0.5f, 0.6f), Vector2f.Center);

            foreach(var option in options)
            {
                var button = new GUIButton(Vector2.Zero, option.Text, option.Callback);
                ButtonsLayout.Add(button);
                GUI.Add(button);
            }
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(Text);
            Renderer.Render(ButtonsLayout);
        }
    }
}