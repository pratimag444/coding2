namespace CommunityEventManagementSystem.Exceptions;

public class EventCapacityExceededException
    : Exception
{
    public EventCapacityExceededException()
        : base("Event has reached maximum capacity.")
    {
    }
}