using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TextRPG.Event;
using TextRPG.Game.Views;
using TextRPG.GUI;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game
{
    public class GameSystem
    {
        public bool Running = true;
        ConsoleRenderSystem Renderer;
        
        Queue<InputKeyEvent> KeyEvents = new Queue<InputKeyEvent>();
        object KeyEventsLock = false;
        Thread InputThread;
        private TextWriter ConsoleOut;
        private SystemConsole Console;
        private View CurrentView;

        public GameSystem()
        {
            InitConsole();
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Logger.Log("Starting game!");

            InputThread = new Thread(HandleInput);
            InputThread.Start();

            CurrentView = new ViewMainMenu(this, Renderer);
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
            while(Running)
            {
                if(eventSystem.Get(out ev, false))
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
            long lastTicks = DateTime.Now.Ticks;

            while(Running)
            {
                ProcessEvents();

                float dt = CalculateDeltaTime(ref lastTicks);
                CurrentView.Update(dt);

                System.Console.SetOut(ConsoleOut);
                Renderer.Flush();
                System.Console.SetOut(TextWriter.Null);
                Thread.Sleep(1);
            }
            System.Console.CursorVisible = true;
            System.Console.SetOut(ConsoleOut);
            InputThread.Join();
        }

        private void ProcessEvents()
        {
            lock (KeyEventsLock)
            {
                while (KeyEvents.Count > 0)
                {
                    CurrentView.OnEvent(KeyEvents.Dequeue());
                }
            }
        }

        private float CalculateDeltaTime(ref long lastTicks)
        {
            long curTicks = DateTime.Now.Ticks;
            float dt = (curTicks - lastTicks) / 10000.0f;
            lastTicks = curTicks;
            return dt;
        }

    }
}