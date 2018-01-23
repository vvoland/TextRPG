using System;
using TextRPG.Game.Views;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Mechanics
{
    public class CharacterCreator
    {
        private ViewNameCharacter NameView;
        private GameSystem Game;
        private RenderSystem Renderer;

        public CharacterCreator(GameSystem game, RenderSystem renderer)
        {
            Game = game;
            Renderer = renderer;
        }

        public void Start()
        {
            NameView = new ViewNameCharacter(Game, Renderer);
            NameView.OnFinish += OnNameChoosen;
            Game.SetView(NameView);
        }

        private void OnNameChoosen(string name)
        {
            Logger.Log("Name: {0}", name);
            Game.SetView(new ViewMainMenu(Game, Renderer));
        }
    }
}