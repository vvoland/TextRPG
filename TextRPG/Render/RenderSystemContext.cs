namespace TextRPG.Render
{
    public class RenderSystemContext
    {
        public virtual Vector2 Clipping { get; set; }
        public virtual Vector2 Translation { get; set; }
        public bool ClippingEnabled
        {
            get
            {
                return Clipping.X == 0 && Clipping.Y == 0;
            }
        }

        public RenderSystemContext()
        {
            Translation = new Vector2(0, 0);
            Clipping = new Vector2(0, 0);
        }

        public virtual RenderSystemContext Clone()
        {
            RenderSystemContext ctx = new RenderSystemContext()
            {
                Clipping = this.Clipping,
                Translation = this.Translation
            };
            
            return ctx;
        }
    }
}