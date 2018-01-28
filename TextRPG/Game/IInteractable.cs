namespace TextRPG.Game
{
    public interface IInteractable
    {
        string MenuText { get; }
        void Interact(GameSystem game);
    }
}