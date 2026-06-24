using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.Domain.Entities;

public class Registration : BaseEntity
{
    protected Registration()
    {
    }

    public Registration(
        int participantId,
        int eventId)
    {
        ParticipantId = participantId;
        EventId = eventId;
        RegistrationDate = DateTime.UtcNow;
        Status = RegistrationStatus.Pending;
    }

    public int ParticipantId { get; private set; }

    public int EventId { get; private set; }

    public DateTime RegistrationDate { get; private set; }

    public RegistrationStatus Status { get; private set; }

    public Participant? Participant { get; private set; }

    public Event? Event { get; private set; }

    public void Confirm()
    {
        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        Status = RegistrationStatus.Cancelled;
    }
}