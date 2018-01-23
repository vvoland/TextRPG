using TextRPG.Event;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewGame : View
    {
        private Label Label;

        public ViewGame(GameSystem game, RenderSystem renderer) : base(game, renderer)
        {
            Label = new Label("TODO: Make some actual game here", GetScreenPoint(0.5f, 0.5f));
        }

        public override bool OnEvent(InputKeyEvent ev)
        {
            return true;
        }

        public override void Update(float dt)
        {
            Renderer.Render(Label);
        }
    }
}