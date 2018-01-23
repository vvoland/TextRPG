using System;
using TextRPG.Event;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public abstract class View
    {
        public bool Finished
        {
            get;
            private set;
        }
        protected GameSystem Game;
        protected RenderSystem Renderer;

        public View(GameSystem game, RenderSystem renderer)
        {
            Game = game;
            Renderer = renderer;
        }

        public abstract void Update(float dt);
        public abstract bool OnEvent(InputKeyEvent ev);

        protected Vector2 GetScreenPoint(float normalizedX, float normalizedY)
        {
            return new Vector2((int)(Renderer.Size.X * normalizedX), (int)(Renderer.Size.Y * normalizedY));
        }

        public virtual void Close()
        {
            Finished = true;
        }
    }
}