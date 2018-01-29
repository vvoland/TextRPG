using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextRPG.Event;
using TextRPG.Game.Mechanics;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewCombat : View
    {
        private Combat Combat;
        private Action OnFinish;

        private GUISystem GUI;
        private Label Description;
        private Label Log;
        private List<CombatActor> Enemies;
        private int Turn = 0;
        private GUIButton NextTurn, Retreat, Quit;
        private LinearLayout Actions;

        public ViewCombat(GameSystem game, ConsoleRenderSystem renderer, Combat combat, Action onFinishCallback)
            : base(game, renderer)
        {
            Combat = combat;
            Combat.OnAction += OnCombatAction;
            OnFinish = onFinishCallback;
            Init();
        }

        private void Init()
        {
            GUI = new GUISystem();

            Description = new Label("");
            Description.Position = GetScreenPoint(0.5f, 0.05f);
            Description.Pivot = new Vector2f(0.5f, 0);

            Log = new Label("");
            Log.Position = GetScreenPoint(0.5f, 0.75f);
            Log.Size = GetScreenPoint(0.5f, 0.25f);
            Log.Pivot = new Vector2f(0.5f, 1.0f);

            Actions = new LinearLayout(LayoutDirection.Horizontal, GetScreenPoint(0.5f, 0.25f), Vector2f.Center);
            NextTurn = new GUIButton(Vector2.Zero, "Next Turn", () => DoAttack());
            Retreat = new GUIButton(Vector2.Zero, "Retreat", () => DoRetreat());
            Quit = new GUIButton(Vector2.Zero, "Close", () => DoClose());
            Actions.Add(NextTurn);
            Actions.Add(Retreat);
            GUI.Add(NextTurn);
            GUI.Add(Retreat);

            Refresh();
        }

        private void DoClose()
        {
            if(Combat.PlayerDead)
            {
                Game.SetView(new ViewMessageInfo(Game, Renderer, "You die", () => Game.End()));
            }
            else
            {
                OnFinish();
            }
        }

        private void Refresh()
        {
            Description.Text = string.Format("You are in combat\nEnemies:\n{0}", BuildEnemiesText());
        }

        private void DoAttack()
        {
            ++Turn;
            Combat.Tick();
            Refresh();
            if(Combat.IsFinished)
            {
                GUI.Remove(NextTurn);
                GUI.Remove(Retreat);
                Actions.Remove(NextTurn);
                Actions.Remove(Retreat);
                Actions.Add(Quit);
                GUI.Add(Quit);
            }
        }

        private void DoRetreat()
        {
            Combat.PlayerActor.AttemptRetreat = true;
        }

        private void OnCombatAction(CombatAction action)
        {
            switch(action.Type)
            {
                case CombatActionType.Attack:
                    AddLog(string.Format("{0} attacks {1} and deals {2} damage!",
                        action.Actor.Name.Name,
                        action.Target.Name.Name,
                        action.Damage.Strength));
                    if(!action.Target.IsAlive)
                        AddLog(string.Format("{0} dies!", action.Target.Name.Name));
                    break;
                case CombatActionType.RetreatFail:
                case CombatActionType.RetreatSuccess:
                    bool success = action.Type == CombatActionType.RetreatSuccess;
                    AddLog(string.Format("{0} tries to retreat... And {1}!", 
                        action.Actor.Name.Name,
                        success ? "does it!" : "fails!"
                        ));
                    break;
            }
        }

        private void AddLog(string msg)
        {
            Log.Text += string.Format("[Turn {0}] {1}\n", Turn, msg);
        }

        private string BuildEnemiesText()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var enemy in Combat.Enemies)
            {
                var damageable = enemy.Damageable;
                sb.AppendLine(string.Format("{0} ({1})", enemy.Name.Name, damageable.DescribeDamageable()));
            }

            var targetName = Combat.PlayerActor.Target?.Name.Name;
            sb.AppendLine(string.Format("Your attack target: {0}", targetName));
            var weapon = Combat.Player.Equipment.Weapon == null ? "None" : Combat.Player.Equipment.Weapon.Name;
            sb.Append(string.Format("Your weapon: {0}", weapon));
            return sb.ToString();
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(Description);
            Renderer.Render(Log);
            Renderer.Render(Actions);
        }
    }
}