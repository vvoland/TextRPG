using System;
using System.Collections.Generic;
using System.Linq;

namespace TextRPG.Render
{
    public class ConsoleRenderBuffer
    {
        private int Width, Height;
        private char[] Buffer;
        private ConsoleColor[] ColorBuffer;
        private char ClearValue;
        private ConsoleColor ClearColor;

        public ConsoleRenderBuffer(int width, int height, char clearValue, ConsoleColor clearColor)
        {
            Width = width;
            Height = height;
            ClearValue = clearValue;
            ClearColor = clearColor;
            Clear();
        }

        public void Clear()
        {
            Buffer = Enumerable.Repeat(ClearValue, Width * Height).ToArray();
            ColorBuffer = Enumerable.Repeat(ClearColor, Width * Height).ToArray();
        }

        public char Get(int x, int y)
        {
            return Buffer[GetIndex(x, y)];
        }

        public ConsoleColor GetColor(int x, int y)
        {
            return ColorBuffer[GetIndex(x, y)];
        }

        public void Set(int x, int y, char value, ConsoleColor color)
        {
            int idx = GetIndex(x, y);
            Buffer[idx] = value;
            ColorBuffer[idx] = color;
        }

        public List<int> Diff(ConsoleRenderBuffer other)
        {
            if(other.Width != Width || other.Height != Height)
                throw new NotSupportedException("Diff for different sized buffers is not supported");
            List<int> result = new List<int>();

            for(int idx = 0; idx <= GetIndex(Width - 1, Height - 1); idx++)
            {
                bool valueDiffers = Buffer[idx] != other.Buffer[idx];
                bool colorDiffers = ColorBuffer[idx] != other.ColorBuffer[idx];
                if(valueDiffers || colorDiffers)
                    result.Add(idx);
            }

            return result;
        }

        private int GetIndex(int x, int y)
        {
            return y * Width + x;
        }

        public void CopyFrom(ConsoleRenderBuffer other)
        {
            other.Buffer.CopyTo(Buffer, 0);
            other.ColorBuffer.CopyTo(ColorBuffer, 0);
        }
    }
}