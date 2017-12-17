namespace TextRPG.Render
{
    public class RenderSystemContext
    {
        public virtual Vector2 Viewport { get; set; }
        public virtual Vector2 Translation { get; set; }

        public virtual RenderSystemContext Clone()
        {
            RenderSystemContext ctx = new RenderSystemContext()
            {
                Viewport = this.Viewport,
                Translation = this.Translation
            };
            
            return ctx;
        }
    }
}