using System;
using System.Collections.Generic;
using System.Linq;

using TextRPG.Utils;

namespace TextRPG.Render
{
    public class ConsoleRenderSystem : RenderSystem
    {
        private char[] Buffer;
        private ConsoleColor[] ColorBuffer;
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
            ColorBuffer = Enumerable.Repeat(ConsoleColor.White, width * height).ToArray();
            Dirty.Capacity = Width * Height;
        }

        public override void Render(IRenderable renderable)
        {
            renderable.Render(this);
        }

        public override void Render(Frame frame)
        {
            char c = frame.Character;
            int startX, startY, endX, endY;
            CalculateBounds(frame, frame.Size,
                out startX, out endX,
                out startY, out endY);
            ConsoleColor color = GetColor(frame);

            for(int x = startX; x < endX; x++)
            {
                SetPixel(x, startY, c, color);
                SetPixel(x, endY - 1, c, color);
            }
            for(int y = startY; y < endY; y++)
            {
                SetPixel(startX, y, c, color);
                SetPixel(endX - 1, y, c, color);
            }
        }

        public override void Render(Rectangle rectangle)
        {
            int startX, startY, endX, endY;
            CalculateBounds(rectangle, rectangle.Size,
                out startX, out endX,
                out startY, out endY);
            ConsoleColor color = GetColor(rectangle);

            for(int y = startY; y < endY; y++)
            {
                for(int x = startX; x < endX; x++)
                {
                    SetPixel(x, y, rectangle.Character, color);
                }
            }
        }

        public override void Render(Label label)
        {
            int startX, startY, endX, endY;
            var lines = label.RenderLines;
            int maxLineSize = lines
                .Select(s => s.Count())
                .Max();

            Vector2 size = label.Size;
            size.X = Math.Min(maxLineSize, size.X);
            size.Y = Math.Min(lines.Count, size.Y);
            Vector2 lineSize = new Vector2(size.X, 1);
            CalculateBounds(label, size,
                out startX, out endX,
                out startY, out endY);
            ConsoleColor color = GetColor(label);
            
            int lineI = 0;
            
            for(int y = startY; y < endY; y++)
            {
                int unusedY;
                lineSize.X = lines[lineI].Length;
                CalculateBounds(new Vector2(label.Position.X, y), label.Pivot, lineSize,
                    out startX, out endX,
                    out unusedY, out unusedY
                );
                int i = 0;
                for(int x = startX; x < endX; x++)
                {
                    SetPixel(x, y, lines[lineI][i++], color);
                }
                lineI++;
            }


        }

        private ConsoleColor GetColor(IRenderable renderable)
        {
            IColorable colorable = renderable as IColorable;
            Color color = Color.White;
            if(colorable != null)
            {
                color = colorable.Color;
            }
            return color.ToConsoleColor();
        }

        public override void Flush()
        {
            if(AllDirty)
            {
                for(int x = 0; x < Width; x++)
                for(int y = 0; y < Height; y++)
                {
                    FlushPixel(x, y);
                }
            }
            else
            {
                Dirty.ForEach(d => 
                {
                    FlushPixel(d.X, d.Y);
                });
            }
        }

        private void FlushPixel(int x, int y)
        {
            int index = GetBufferIndex(x, y);
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ColorBuffer[index];
            Console.Write(Buffer[index]);
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

            CalculateBounds(pos, pivot, size, out startX, out endX, out startY, out endY);
        }

        public void CalculateBounds(
            Vector2 pos, Vector2f pivot, Vector2 size,
            out int startX, out int endX,
            out int startY, out int endY)
        {
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

        private int GetBufferIndex(int x, int y)
        {
            return y * Width + x;
        }

        private void SetPixel(int x, int y, char value, ConsoleColor color)
        {
            if (!IsInClippingRange(x, y))
                return;
                
            x += Context.Translation.X;
            y += Context.Translation.Y;
            if(!IsValidPixel(x, y))
                return;
                
            int idx = GetBufferIndex(x, y);
            Buffer[idx] = value;
            ColorBuffer[idx] = color;
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

    }
}