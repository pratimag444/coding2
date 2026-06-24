
namespace CommunityEventManagementSystem.Exceptions;

public sealed class EventNotFoundException
    : ApplicationExceptionBase
{
    public EventNotFoundException()
        : base("Event could not be found.")
    {
    }

    public EventNotFoundException(int eventId)
        : base($"Event with ID {eventId} could not be found.")
    {
    }
}

