namespace TextRPG.Event
{
    public interface IEventSystem<EventType>
    {
        // Get next event, 
        // this method should return immediately if block = false and return false if no event is available
        bool Get(out EventType even, bool block = false);
    }
}