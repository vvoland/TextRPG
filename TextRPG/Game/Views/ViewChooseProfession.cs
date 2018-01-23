using System;
using TextRPG.Event;
using TextRPG.GUI;
using TextRPG.GUI.Layout;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewChooseProfession : View
    {
        public Action<Profession> OnFinish;

        private GUISystem GUI;
        private Label Header;
        private LinearLayout ProfessionsLayout;
        private GUIButton Continue;

        public ViewChooseProfession(GameSystem game, RenderSystem renderer) : base(game, renderer)
        {
            GUI = new GUISystem();
            Header = new Label("What is your profession?", GetScreenPoint(0.5f, 0.25f));

            ProfessionsLayout = new LinearLayout(LayoutDirection.Horizontal, GetScreenPoint(0.5f, 0.5f), Vector2f.Center);
            ProfessionsLayout.Size = new Vector2((int)Renderer.Size.X, (int)Renderer.Size.Y / 3);
            ProfessionsLayout.Spacing = 3;
            foreach(var profession in Professions.All)
            {
                var button = new GUIButton(
                    Vector2.Zero,
                    profession.Name,
                    () => ChooseProfession(profession)
                );
                ProfessionsLayout.Add(button);
                GUI.Add(button);
            }

            GUI.Add(Continue);
        }

        private void ChooseProfession(Profession profession)
        {
            OnFinish?.Invoke(profession);
            Close();
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return GUI.OnEvent(ev);
        }

        public override void Update(float dt)
        {
            Renderer.Render(Header);
            Renderer.Render(ProfessionsLayout);
        }

    }
}