using TextRPG.Render;

namespace TextRPG.GUI
{
    public abstract class GUIWidget : IRenderable
    {
        public abstract Vector2 Position { get; set; }
        public abstract Vector2f Pivot { get; set; }

        public abstract void Render(RenderSystem system);
    }
}