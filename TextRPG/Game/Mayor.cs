using TextRPG.Game.Views;

namespace TextRPG.Game
{
    public class Mayor : IInteractable
    {
        public string MenuText => "Visit Mayor";

        public void Interact(GameSystem game)
        {
            var view = new ViewMessageInfo(game, game.Renderer, "I do not have any quests for you... Go away vagrant!", () => {});
            game.PushView(view);
        }
    }
}