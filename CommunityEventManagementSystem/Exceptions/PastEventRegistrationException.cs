namespace CommunityEventManagementSystem.Exceptions;

public class PastEventRegistrationException
    : Exception
{
    public PastEventRegistrationException()
        : base("Cannot register for a past event.")
    {
    }
}