using System;
using System.Linq;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemConsole console = new SystemConsole();
            int width = console.WindowWidth;
            int height = console.WindowHeight;

            ConsoleRenderSystem renderer = new ConsoleRenderSystem(console, width, height);
            Frame rect = new Frame(new Vector2(width/2, height/2), new Vector2(30, 30));
            rect.Pivot = new Vector2f(0.5f, 0.5f);
            console.BackgroundColor = ConsoleColor.Cyan;
            console.ForegroundColor = ConsoleColor.Red;
            console.Clear();

            renderer.Render(rect);
            renderer.Flush();

            Console.ReadLine();
        }
    }
}