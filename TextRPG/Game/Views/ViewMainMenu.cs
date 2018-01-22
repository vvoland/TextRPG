using System;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Views
{
    public class ViewMainMenu : View
    {
        private GUIButton NewGame, Quit;
        private GUISystem GUI;

        public ViewMainMenu(GameSystem game, RenderSystem renderer) : base(game, renderer)
        {
            Init();
        }

        private void Init()
        {
            GUI = new GUISystem();
            NewGame = new GUIButton(GetScreenPoint(0.25f, 0.45f), "New Game", () => ActionNewGame());
            Quit = new GUIButton(GetScreenPoint(0.75f, 0.45f), "Quit", () => Game.Running = false);

            GUI.Add(NewGame);
            GUI.Add(Quit);
        }

        private void ActionNewGame()
        {
            Logger.Log("New game!");
        }

        public override void Update(float dt)
        {
            GUI.Render(Renderer);
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

    }
}