namespace TextRPG.Event
{
    public interface IEventListener<EventType>
    {
        // Should return true if event has been consumed
        // if not, then it will be passed to the next listener
        bool OnEvent(EventType even);
    }
}