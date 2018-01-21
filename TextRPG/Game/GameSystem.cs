using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game
{
    public class GameSystem
    {
        ConsoleRenderSystem Renderer;
        GUISystem GUISystem;

        Queue<InputKeyEvent> KeyEvents = new Queue<InputKeyEvent>();
        object KeyEventsLock = false;
        Thread InputThread;
        private TextWriter ConsoleOut;
        private SystemConsole Console;

        public GameSystem()
        {
            InitConsole();
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            GUIButton btn = new GUIButton(new Vector2(width / 4, height / 2), "Test", () => 
            {
                Logger.Log("Button1!");
            });
            GUIButton btn2 = new GUIButton(new Vector2(width / 4 * 3, height / 2), "Test2", () => 
            {
                Logger.Log("Button2!");
            });


            GUISystem = new GUISystem();
            GUISystem.Add(btn);
            GUISystem.Add(btn2);

            Logger.Log("Starting game!");

            InputThread = new Thread(HandleInput);
            InputThread.Start();
        }

        private void InitConsole()
        {
            System.Console.CursorVisible = false;
            ConsoleOut = System.Console.Out;
            Console = new SystemConsole();

            Renderer = new ConsoleRenderSystem(Console, Console.WindowWidth, Console.WindowHeight);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        private void HandleInput(object obj)
        {
            ConsoleInputEventSystem eventSystem = new ConsoleInputEventSystem();
            InputKeyEvent ev;

            Logger.Log("Starting input thread");
            while(true)
            {
                if(eventSystem.Get(out ev, true))
                {
                    lock(KeyEventsLock)
                    {
                        KeyEvents.Enqueue(ev);
                    }
                }
            }
        }

        public void Run()
        {
            while(true)
            {
                lock(KeyEventsLock)
                {
                    while(KeyEvents.Count > 0)
                    {
                        GUISystem.OnEvent(KeyEvents.Dequeue());
                    }
                }
                Renderer.Render(GUISystem);
                System.Console.SetOut(ConsoleOut);
                Renderer.Flush();
                System.Console.SetOut(TextWriter.Null);
                Thread.Sleep(5);
            }
        }
    }
}