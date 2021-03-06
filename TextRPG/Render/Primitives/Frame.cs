namespace TextRPG.Render
{
    public class Frame : IRenderable, ISizeable, IColorable
    {
        public char Character { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2f Pivot { get; set; }
        public Color Color { get; set; }

        public Frame(Vector2 position, Vector2 size)
        {
            Color = Color.White;
            Character = '█';
            Position = position;
            Size = size;
            Pivot = new Vector2f(0.5f, 0.5f);
        }

        public void Render(RenderSystem system)
        {
            system.Render(this);
        }
    }
}