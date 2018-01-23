using System;
using TextRPG.Event;
using TextRPG.Game.Views;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Mechanics
{
    public class CharacterCreator
    {
        public Action<PlayerEntity> OnFinish;
        private ViewNameCharacter NameView;
        private ViewChooseProfession ProfessionView;
        private GameSystem Game;
        private RenderSystem Renderer;

        private string Name;
        private Profession Profession;

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
            Name = name;
            NameView = null;
            ProfessionView = new ViewChooseProfession(Game, Renderer);
            ProfessionView.OnFinish += OnProfessionChoosen;
            Game.SetView(ProfessionView);
        }

        private void OnProfessionChoosen(Profession profession)
        {
            Profession = profession;
            var player = new PlayerEntity(Name, Profession);
            OnFinish?.Invoke(player);
        }
    }
}