
namespace CommunityEventManagementSystem.Exceptions;

public sealed class DuplicateRegistrationException
    : ApplicationExceptionBase
{
    public DuplicateRegistrationException()
        : base("Participant is already registered for this event.")
    {
    }
}

