using System.Collections.Generic;
using TextRPG.Render;

namespace TextRPG.GUI
{
    public class View : IRenderable
    {
        public Vector2 Size { get; set; }
        public Vector2 Camera { get; set; }
        public Vector2 Position { get; set; }
        public Vector2f Pivot { get; set; }

        List<IRenderable> Renderables = new List<IRenderable>();

        public View(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            Camera = new Vector2();
        }

        public void Add(IRenderable renderable)
        {
            Renderables.Add(renderable);
        }

        public void Remove(IRenderable renderable)
        {
            Renderables.Remove(renderable);
        }

        public virtual void Render(RenderSystem system)
        {
            system.PushContext();
            system.Translate(Position);
            system.SetClipping(Size);
            Renderables.ForEach(r => r.Render(system));
            system.PopContext();
        }
    }
}