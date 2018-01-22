using TextRPG.Render;

namespace TextRPG.GUI
{
    public abstract class GUIWidget : IRenderable, ISizeable
    {
        public abstract Vector2 Position { get; set; }
        public abstract Vector2f Pivot { get; set; }
        public virtual Vector2 Size { get; set; }

        public abstract void Render(RenderSystem system);
    }
}