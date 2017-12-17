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

        public override void Render(Frame frame)
        {
            char c = frame.Character;
            int startX, startY, endX, endY;
            CalculateBounds(frame, frame.Size,
                out startX, out endX,
                out startY, out endY);

            for(int x = startX; x < endX; x++)
            {
                SetPixel(x, startY, c);
                SetPixel(x, endY - 1, c);
            }
            for(int y = startY; y < endY; y++)
            {
                SetPixel(startX, y, c);
                SetPixel(endX - 1, y, c);
            }
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
            if (!IsInClippingRange(x, y))
                return;
                
            x += Context.Translation.X;
            y += Context.Translation.Y;
            if(!IsValidPixel(x, y))
                return;
                
            int idx = GetBufferIndex(x, y);
            Buffer[idx] = value;
            Dirty.Add(new Vector2(x, y));
        }

        private bool IsValidPixel(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        private bool IsInClippingRange(int x, int y)
        {
            int maxX = Width;
            int maxY = Height;

            if(Context.Clipping.X != 0)
            {
                if(x >= Context.Clipping.X)
                    return false;
            }
            if(Context.Clipping.Y != 0)
            {
                if(y >= Context.Clipping.Y)
                    return false;
            }
            return true;
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