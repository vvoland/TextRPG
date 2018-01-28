using System;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;
using TextRPG.Utils;

namespace TextRPG.Game.Views
{
    public class ViewCharacter : View
    {
        private PlayerEntity Entity;
        private Frame Frame;
        private LinearLayout Layout;
        private Label Header, CharacterName, Health, Gold, Stats, Exp;
        private Vector2 Size;
        private float Timer = 0.0f;

        public ViewCharacter(GameSystem game, RenderSystem renderer, PlayerEntity entity) : base(game, renderer)
        {
            int size = (int)(MathF.Min(Renderer.Size.X, Renderer.Size.Y) * 0.25f);
            if(size < 21)
                size = 21;
            Size = new Vector2(size, size);
            Entity = entity;
            Init();
        }

        private void Init()
        {
            Frame = new Frame(Vector2.Zero, Size);
            Frame.Pivot = Vector2f.Zero;
            Frame.Character = 'x';
            Frame.Color = Color.DarkGray;

            var layoutStart = new Vector2(Size.X / 2, 1);
            Layout = new LinearLayout(LayoutDirection.Vertical, layoutStart, new Vector2f(0.5f, 0.0f));
            Layout.Size = Size.Expand(-2, -2);

            Header = new Label(Pad("Character"));
            Header.Color = Color.DarkBlue;
            Layout.Add(new GUIAdapter(Header));

            CharacterName = new Label("");
            Layout.Add(new GUIAdapter(CharacterName));

            Health = new Label("");
            Layout.Add(new GUIAdapter(Health));

            Gold = new Label("");
            Gold.Color = Color.DarkYellow;
            Layout.Add(new GUIAdapter(Gold));

            Exp = new Label("");
            Exp.Color = Color.DarkCyan;
            Layout.Add(new GUIAdapter(Exp));

            Stats = new Label("");
            Layout.Add(new GUIAdapter(Stats));

            Refresh();
        }

        private string Pad(string str, char c = '=')
        {
            int size = Layout.Size.X;
            int total = size - str.Length;
            int left  = total / 2 + str.Length;

            return str.PadLeft(left, c).PadRight(size, c);
        }

        private void Refresh()
        {
            CharacterName.Text = Entity.Name;
            float hpRatio = Entity.Damageable.Health / (float)(Entity.Damageable.MaxHealth);
            Health.Text = string.Format("HP: {0}/{1}", Entity.Damageable.Health, Entity.Damageable.MaxHealth);
            if(hpRatio < 0.3f)
                Health.Color = Color.DarkRed;
            else if(hpRatio < 0.75f)
                Health.Color = Color.DarkYellow;
            else
                Health.Color = Color.DarkGreen;

            Gold.Text = string.Format("Gold: {0}", Entity.Inventory.Gold);
            Stats.Text = string.Format("\nLevel: {0}\nStrength: {1}\nAgility: {2}\nCharisma: {3}",
                Entity.Stats.Level, Entity.Stats.Strength, Entity.Stats.Agility, Entity.Stats.Charisma);
            Exp.Text = string.Format("EXP: {0}/{1}", Entity.Experience.Points, Entity.Experience.NextLevel);
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            if(ev.KeyChar == 'l')
            {
                Entity.Experience.Add(100);
                return true;
            }
            return false;
        }

        public override void Update(float dt)
        {
            Timer += dt;
            if(Timer > 0.25f)
            {
                Refresh();
                Timer = 0.0f;
            }
            Renderer.PushContext();
                Renderer.Render(Layout);
                Renderer.Render(Frame);
            Renderer.PopContext();
        }
    }
}