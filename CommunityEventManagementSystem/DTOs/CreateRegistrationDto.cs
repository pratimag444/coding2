
using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class CreateRegistrationDto
{
    [Required]
    [Range(1, int.MaxValue,
        ErrorMessage = "A valid participant must be selected.")]
    public int ParticipantId { get; set; }

    [Required]
    [Range(1, int.MaxValue,
        ErrorMessage = "A valid event must be selected.")]
    public int EventId { get; set; }

    public DateTime RegistrationDate =>
        DateTime.UtcNow;
}

