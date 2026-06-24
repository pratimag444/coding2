namespace CommunityEventManagementSystem.DTOs;

public class RegistrationDetailsDto
{
    public int Id { get; set; }

    public int ParticipantId { get; set; }

    public int EventId { get; set; }

    public string ParticipantName { get; set; }
        = string.Empty;

    public string EventName { get; set; }
        = string.Empty;

    public DateTime RegistrationDate { get; set; }

    public string Status { get; set; }
        = string.Empty;

    public string RegistrationDateDisplay =>
        RegistrationDate.ToString("dd MMM yyyy");

    public string RegistrationDateTimeDisplay =>
        RegistrationDate.ToString("dd MMM yyyy hh:mm tt");

    public bool IsConfirmed =>
        Status.Equals(
            "Confirmed",
            StringComparison.OrdinalIgnoreCase);

    public bool IsPending =>
        Status.Equals(
            "Pending",
            StringComparison.OrdinalIgnoreCase);

    public bool IsCancelled =>
        Status.Equals(
            "Cancelled",
            StringComparison.OrdinalIgnoreCase);

    public string StatusBadgeClass =>
        Status switch
        {
            "Confirmed" => "bg-success",
            "Pending" => "bg-warning text-dark",
            "Cancelled" => "bg-danger",
            _ => "bg-secondary"
        };

    public string StatusIcon =>
        Status switch
        {
            "Confirmed" => "bi-check-circle-fill",
            "Pending" => "bi-hourglass-split",
            "Cancelled" => "bi-x-circle-fill",
            _ => "bi-question-circle-fill"
        };

    public string StatusDescription =>
        Status switch
        {
            "Confirmed" => "Participant successfully enrolled.",
            "Pending" => "Awaiting confirmation.",
            "Cancelled" => "Registration has been cancelled.",
            _ => "Unknown status."
        };
}
