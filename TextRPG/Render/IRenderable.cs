namespace TextRPG.Render
{
    public interface IRenderable
    {
        Vector2 Position { get; set; }
        Vector2f Pivot { get; set; }
        void Render(RenderSystem system);
    }
}