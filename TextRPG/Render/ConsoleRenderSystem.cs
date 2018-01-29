using System;
using System.Collections.Generic;
using System.Linq;

using TextRPG.Utils;

namespace TextRPG.Render
{
    public class ConsoleRenderSystem : RenderSystem
    {
        private ConsoleRenderBuffer[] Buffers;
        private int CurrentBuffer = 0;
        private int Width, Height;
        private IConsole Console;
        private const char ClearCharacter = ' ';
        private bool AllDirty = true;
        private const ConsoleColor ClearColor = ConsoleColor.White;

        public override Vector2f Size 
        { 
            get;
            protected set;
        }

        public ConsoleRenderSystem(IConsole console, int width, int height)
        {
            Console = console;
            Width = width;
            Height = height;
            Size = new Vector2f((float)width, (float)height);
            Buffers = new ConsoleRenderBuffer[]
            {
                new ConsoleRenderBuffer(width, height, ClearCharacter, ClearColor),
                new ConsoleRenderBuffer(width, height, ClearCharacter, ClearColor),
            };
            CurrentBuffer = 0;
        }

        public override void Render(IRenderable renderable)
        {
            renderable.Render(this);
        }

        public override void Render(Frame frame)
        {
            char c = frame.Character;
            var bounds = BoundsCalculator.Calculate(frame, frame.Size);
            ConsoleColor color = GetColor(frame);

            for(int x = bounds.XMin; x < bounds.XMax; x++)
            {
                SetPixel(x, bounds.YMin, c, color);
                SetPixel(x, bounds.YMax - 1, c, color);
            }
            for(int y = bounds.YMin; y < bounds.YMax; y++)
            {
                SetPixel(bounds.XMin, y, c, color);
                SetPixel(bounds.XMax - 1, y, c, color);
            }

            FinalizeRender(frame);
        }

        public override void Render(Rectangle rectangle)
        {
            var bounds = BoundsCalculator.Calculate(rectangle, rectangle.Size);
            ConsoleColor color = GetColor(rectangle);

            for(int y = bounds.YMin; y < bounds.YMax; y++)
            {
                for(int x = bounds.XMin; x < bounds.XMax; x++)
                {
                    SetPixel(x, y, rectangle.Character, color);
                }
            }
            FinalizeRender(rectangle);
        }

        public override void Render(Label label)
        {
            if(string.IsNullOrEmpty(label.Text))
                return;
            var lines = label.RenderLines;
            int maxLineSize = lines
                .Select(s => s.Count())
                .Max();

            Vector2 size = label.Size;
            size.X = Math.Min(maxLineSize, size.X);
            size.Y = Math.Min(lines.Count, size.Y);
            Vector2 lineSize = new Vector2(size.X, 1);
            var bounds = BoundsCalculator.Calculate(label, size);
            ConsoleColor color = GetColor(label);
            
            int lineI = 0;
            
            for(int y = bounds.YMin; y < bounds.YMax; y++)
            {
                lineSize.X = lines[lineI].Length;
                var lineBounds = BoundsCalculator.Calculate(new Vector2(label.Position.X, y), label.Pivot, lineSize);
                int i = 0;
                for(int x = lineBounds.XMin; x < lineBounds.XMax; x++)
                {
                    SetPixel(x, y, lines[lineI][i++], color);
                }
                lineI++;
            }

            FinalizeRender(label);
        }

        private void FinalizeRender(IRenderable renderable)
        {
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
                for(int idx = 0; idx < Width * Height; idx++)
                    FlushPixel(idx);
                AllDirty = false;
            }
            else
            {
                int prevBuffer = 1 - CurrentBuffer;
                List<int> diff = Buffers[CurrentBuffer].Diff(Buffers[prevBuffer]);
                diff.ForEach(idx => FlushPixel(idx));

                Buffers[prevBuffer].Clear();
                CurrentBuffer = prevBuffer;
            }
        }

        private void FlushPixel(int idx)
        {
            int x = idx % Width;
            int y = idx / Width;

            char value = Buffers[CurrentBuffer].Get(x, y);
            ConsoleColor color = Buffers[CurrentBuffer].GetColor(x, y);

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(value);
        }

        private void SetPixel(int x, int y, char value, ConsoleColor color)
        {
            if (!IsInClippingRange(x, y))
                return;
                
            x += Context.Translation.X;
            y += Context.Translation.Y;
            if(!IsValidPixel(x, y))
                return;
                
            Buffers[CurrentBuffer].Set(x, y, value, color);
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

        public override void Clear(Rect rect)
        {
            for(int y = rect.YMin; y < rect.YMax; y++)
            {
                for(int x = rect.XMin; x < rect.XMax; x++)
                {
                    SetPixel(x, y, ClearCharacter, ClearColor);
                }
            }
        }

        public override void Clear()
        {
            Buffers[CurrentBuffer].Clear();
            AllDirty = true;
        }
    }
}