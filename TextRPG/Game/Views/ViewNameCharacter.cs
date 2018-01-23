using System;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Views
{
    public class ViewNameCharacter : View
    {
        public Action<string> OnFinish;
        private GUISystem GUI;
        private Label Header;
        private GUITextInput Name;
        private GUIButton Continue;
        private const int MaxNameLength = 32;

        public ViewNameCharacter(GameSystem game, RenderSystem renderer) : base(game, renderer)
        {
            GUI = new GUISystem();
            Header = new Label("Tell me what is your name", GetScreenPoint(0.5f, 0.25f));
            Name = new GUITextInput(GetScreenPoint(0.5f, 0.5f), MaxNameLength);
            Continue = new GUIButton(GetScreenPoint(0.5f, 0.75f), "Continue", ActionContinue);

            GUI.Add(Name);
            GUI.Add(Continue);
        }

        private void ActionContinue()
        {
            OnFinish?.Invoke(Name.Text);
            Close();
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(Header);
            Renderer.Render(Name);
            Renderer.Render(Continue);
        }
    }
}