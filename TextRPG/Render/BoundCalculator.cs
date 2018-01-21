using TextRPG;

namespace TextRPG.Render
{
    public static class BoundsCalculator
    {
        public static Rect Calculate(IRenderable renderable, Vector2 size)
        {
            return Calculate(renderable.Position, renderable.Pivot, size);
        }

        public static Rect Calculate(Vector2 pos, Vector2f pivot, Vector2 size)
        {
            int dx = (int)(size.X * pivot.X);
            int dy = (int)(size.Y * pivot.Y);

            if(dx == size.X)
                dx -= 1;
            if(dy == size.Y)
                dy -= 1;

            Vector2 start = new Vector2(pos.X - dx, pos.Y - dy);
            return new Rect(start, size);
        }
    }
}