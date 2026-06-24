using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class UpdateEventDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly EventDate { get; set; }

    [Required]
    public TimeOnly EventTime { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int MaximumParticipants { get; set; }

    public List<int> VenueIds { get; set; } = new();

    public List<int> ActivityIds { get; set; } = new();
}