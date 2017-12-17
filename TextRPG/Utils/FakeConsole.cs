using System;
using System.Linq;

namespace TextRPG.Utils
{
    class FakeConsole : IConsole
    {
        public char[] Buffer;

        private int X, Y;        
        public int Width, Height;

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public int WindowWidth
        {
            get
            {
                return Width;
            }
        }

        public int WindowHeight
        {
            get
            {
                return Height;
            }
        }

        public FakeConsole(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new char[width * height];
        }

        public void SetCursorPosition(int x, int y)
        {
            CheckCoordinates(x, y);
            X = x;
            Y = y;
        }

        public void Write(string text)
        {
            foreach(char c in text)
            {
                Write(c);
                X++;
                if(X >= Width)
                {
                    X = 0;
                    Y++;
                }
            }
        }

        public void Write(char character)
        {
            Buffer[GetIndex(X, Y)] = character;
        }

        private int GetIndex(int x, int y)
        {
            CheckCoordinates(x, y);
            return y * Width + x;
        }

        private void CheckCoordinates(int x, int y)
        {
            if(x >= Width || x < 0)
                throw new ArgumentOutOfRangeException("X coordinate out of range. Must be 0 <= x < " + Width);
            if(y >= Height || y < 0)
                throw new ArgumentOutOfRangeException("Y coordinate out of range. Must be 0 <= y < " + Width);
        }

        public bool BufferEquals(char[] buf)
        {
            return Enumerable.SequenceEqual(Buffer, buf);
        }

        public bool BufferEquals(string str)
        {
            return BufferEquals(str.ToCharArray());
        }

        public char BufferAt(int x, int y)
        {
            return Buffer[GetIndex(x, y)];
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}