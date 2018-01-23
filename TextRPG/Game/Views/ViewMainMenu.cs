using System;
using TextRPG.Event;
using TextRPG.Game.Mechanics;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Views
{
    public class ViewMainMenu : View
    {
        private Label Title, Author, Help;
        private GUIButton ContinueGame, NewGame, Quit;
        private GUISystem GUI;
        private LinearLayout ButtonsLayout, MenuLayout;

        public ViewMainMenu(GameSystem game, RenderSystem renderer) : base(game, renderer)
        {
            Init();
        }

        private void Init()
        {
            GUI = new GUISystem();

            Title = new Label(@" _____         _    ____________ _____
|_   _|       | |   | ___ \ ___ \  __ \
  | | _____  _| |_  | |_/ / |_/ / |  \/
  | |/ _ \ \/ / __| |    /|  __/| | __ 
  | |  __/>  <| |_  | |\ \| |   | |_\ \
  \_/\___/_/\_\\__| \_| \_\_|    \____/", GetScreenPoint(0.5f, 0.25f), Vector2f.Center, Color.DarkGray);

            Author = new Label("by Pawel Gronowski", Vector2.Zero, Vector2f.Center, Color.DarkBlue);
            Help = new Label("TAB: Switch button\nENTER: Activate", Vector2.Zero, Vector2f.Center, Color.DarkGreen);

            MenuLayout = new LinearLayout(LayoutDirection.Vertical, GetScreenPoint(0.5f, 0.5f), Vector2f.Center);
            MenuLayout.Size = new Vector2((int)Renderer.Size.X, (int)Renderer.Size.Y);
            MenuLayout.Spacing = 2;

            ButtonsLayout = new LinearLayout(LayoutDirection.Horizontal, GetScreenPoint(0.5f, 0.5f), Vector2f.Center);
            ButtonsLayout.Spacing = 5;
            ContinueGame = new GUIButton(Vector2.Zero, "Continue Game", () => ActionContinueGame());
            NewGame = new GUIButton(Vector2.Zero, "New Game", () => 
            {
                ActionNewGame();
                System.GC.Collect();
            });
            Quit = new GUIButton(Vector2.Zero, "Quit", () => Game.Running = false);

            ButtonsLayout.Add(ContinueGame);
            ButtonsLayout.Add(NewGame);
            ButtonsLayout.Add(Quit);
            ButtonsLayout.Size = new Vector2((int)Renderer.Size.X, (int)Renderer.Size.Y / 4);

            MenuLayout.Add(new GUIAdapter(Title));
            MenuLayout.Add(new GUIAdapter(Author));
            MenuLayout.Add(new GUIAdapter(Help));
            MenuLayout.Add(ButtonsLayout);

            GUI.Add(ContinueGame);
            GUI.Add(NewGame);
            GUI.Add(Quit);
        }

        private void ActionContinueGame()
        {
            Logger.Log("TODO");
        }

        private void ActionNewGame()
        {
            var creator = new CharacterCreator(Game, Renderer);
            creator.Start();
        }

        public override void Update(float dt)
        {
            Renderer.Render(MenuLayout);
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

    }
}