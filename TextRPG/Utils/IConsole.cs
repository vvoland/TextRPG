using System;

namespace TextRPG.Utils
{
    public interface IConsole
    {
        int WindowWidth { get; }
        int WindowHeight { get; }
        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor ForegroundColor { get; set; }

        void Clear();
        void Write(string text);
        void Write(char character);
        void SetCursorPosition(int x, int y);
    }
}