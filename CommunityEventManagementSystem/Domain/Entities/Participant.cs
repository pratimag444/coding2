namespace CommunityEventManagementSystem.Domain.Entities;

public class Participant : User
{
    private readonly List<Registration> _registrations = new();

    public IReadOnlyCollection<Registration> Registrations =>
        _registrations.AsReadOnly();

    protected Participant()
    {
    }

    public Participant(
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
        : base(
            firstName,
            lastName,
            email,
            phoneNumber)
    {
    }

    public void UpdateDetails(
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;

        UpdateModifiedDate();
    }

    public override bool CanManageEvents()
    {
        return false;
    }
}