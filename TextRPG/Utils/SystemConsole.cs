using System;

namespace TextRPG.Utils
{
    public class SystemConsole : IConsole
    {
        public ConsoleColor BackgroundColor
        {
            get
            {
                return Console.BackgroundColor;
            }
            set
            {
                Console.BackgroundColor = value;
            }
        }

        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }

        public int WindowWidth
        {
            get
            {
                return Console.WindowWidth;
            }
        }

        public int WindowHeight
        {
            get
            {
                return Console.WindowHeight;
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void SetCursorPosition(int x, int y)
        {
            System.Console.SetCursorPosition(x, y);
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public void Write(char character)
        {
            System.Console.Write(character);
        }
    }
}