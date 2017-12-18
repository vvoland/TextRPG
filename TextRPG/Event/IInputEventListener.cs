namespace TextRPG.Event
{
    public interface IInputEventListener
    {
        bool OnEvent(InputKeyEvent keyEvent);
    }
}