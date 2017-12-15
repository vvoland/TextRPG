using System;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();

            int w = Console.WindowWidth;
            int h = Console.WindowHeight;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(w / 2 - 10, h / 2);
            Console.Write("This is my RPG game!");

            Console.ReadLine();
        }
    }
}
