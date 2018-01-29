using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TextRPG.Event;
using TextRPG.Game.Generation;
using TextRPG.Game.Mechanics;
using TextRPG.Game.Views;
using TextRPG.GUI;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game
{
    public class GameSystem
    {
        public bool Running = true;
        public ConsoleRenderSystem Renderer
        {
            get;
            private set;
        }
        public PlayerEntity Player
        {
            get;
            private set;
        }

        Queue<InputKeyEvent> KeyEvents = new Queue<InputKeyEvent>();
        object KeyEventsLock = false;
        Thread InputThread;
        private TextWriter ConsoleOut;
        private SystemConsole Console;
        private View PlayerView;
        private Stack<View> Views = new Stack<View>();
        private World World;
        private Random Random = new Random();
        private View CurrentView
        {
            get
            {
                if(Views.Count == 0)
                    return null;
                return Views.Peek();
            }
        }

        public GameSystem()
        {
            InitConsole();
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            Logger.Log("Starting game!");

            InputThread = new Thread(HandleInput);
            InputThread.Start();

            SetView(new ViewMainMenu(this, Renderer));
        }

        public void StartNewGame()
        {
            var creator = new CharacterCreator(this, Renderer);
            creator.Start();
            creator.OnFinish += OnPlayerCreated;
        }

        private void OnPlayerCreated(PlayerEntity player)
        {
            CreateNewGame(player);
            SetView(new ViewGame(this, Renderer));
            PushView(new ViewMessageInfo(this, Renderer, "You wake up", () => 
            {
                var city = World.Cities.Random();
                TravelTo(city, false);
            }));
        }

        public void End()
        {
            Running = false;
        }

        public void TravelTo(Location location, bool allowFight = true)
        {
            int travelFightRoll = Random.Next(100);
            Logger.Log("Travel fight roll: {0}", travelFightRoll);
            if(allowFight && travelFightRoll < World.Description.TravelFightChance)
            {
                var opponent = World.Creatures
                    .OrderBy(c => Math.Abs(c.Stats.Level - Player.Stats.Level))
                    .Take(3)
                    .ToList()
                    .Random()
                    .Clone();
                CombatActor combatOpponent = new CombatActor(opponent, opponent.Stats, opponent, opponent);
                Combat combat = new Combat(Player, new []{ combatOpponent });
                SetView(new ViewCombat(this, Renderer, combat, () => TravelTo(location, false)));
            }
            else
            {
                Player.Location = location;
                SetView(new ViewLocation(this, Renderer, Player, location));
            }
        }

        private void CreateNewGame(PlayerEntity player)
        {
            Player = player;
            var generator = new WorldGenerator();
            var desc = WorldDescription.FromFile("world.json");
            World = generator.Generate(desc, player);
            PlayerView = new ViewCharacter(this, Renderer, Player);
        }

        public void PushView(View view)
        {
            Views.Push(view);
        }

        public void PopView()
        {
            Views.Pop();
            
        }

        public void SetView(View view)
        {
            foreach(var v in Views)
                v.Close();
            PushView(view);
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
                CurrentView?.Update(dt);
                PlayerView?.Update(dt);

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
            if(CurrentView == null)
                return;
                
            lock (KeyEventsLock)
            {
                while (KeyEvents.Count > 0)
                {
                    var even = KeyEvents.Dequeue();
                    if(PlayerView != null && PlayerView.OnEvent(even))
                        continue;
                    CurrentView.OnEvent(even);
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