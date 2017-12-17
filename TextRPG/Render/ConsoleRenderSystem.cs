using System;
using System.Collections.Generic;
using System.Linq;

using TextRPG.Utils;

namespace TextRPG.Render
{
    public class ConsoleRenderSystem : RenderSystem
    {
        private char[] Buffer;
        private List<Vector2> Dirty = new List<Vector2>();
        private int Width, Height;
        private IConsole Console;
        private bool AllDirty = true;

        public ConsoleRenderSystem(IConsole console, int width, int height)
        {
            Console = console;
            Width = width;
            Height = height;
            Buffer = Enumerable.Repeat(' ', width * height).ToArray();
            Dirty.Capacity = Width * Height;
        }

        public override void Render(IRenderable renderable)
        {
            throw new NotImplementedException();
        }

        public override void Render(Rectangle rectangle)
        {
            int startX, startY, endX, endY;
            CalculateBounds(rectangle, rectangle.Size,
                out startX, out endX,
                out startY, out endY);

            for(int y = startY; y < endY; y++)
            {
                for(int x = startX; x < endX; x++)
                {
                    SetPixel(x, y, rectangle.Character);
                }
            }
        }

        public override void Flush()
        {
            if(AllDirty)
            {
                for(int x = 0; x < Width; x++)
                for(int y = 0; y < Height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(Buffer[GetBufferIndex(x, y)]);
                }
            }
            else
            {
                Dirty.ForEach(d => 
                {
                    Console.SetCursorPosition(d.X, d.Y);
                    Console.Write(Buffer[GetBufferIndex(d.X, d.Y)]);
                });
            }
        }

        private int GetBufferIndex(int x, int y)
        {
            return y * Width + x;
        }

        private void SetPixel(int x, int y, char value)
        {
            int idx = GetBufferIndex(x, y);
            Buffer[idx] = value;
            Dirty.Add(new Vector2(x, y));
        }

        // start is inclusive
        // end is exclusive
        public void CalculateBounds(
            IRenderable renderable, Vector2 size,
            out int startX, out int endX,
            out int startY, out int endY)
        {
            var pos = renderable.Position;
            var pivot = renderable.Pivot;

            int dx = (int)(size.X * pivot.X);
            int dy = (int)(size.Y * pivot.Y);

            if(dx == size.X)
                dx -= 1;
            if(dy == size.Y)
                dy -= 1;

            startX = pos.X - dx;
            startY = pos.Y - dy;
            endX = startX + size.X;
            endY = startY + size.Y;
        }
    }
}