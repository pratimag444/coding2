using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class CreateEventDto
{
    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "Event name must be between 3 and 100 characters.")]
    [RegularExpression(
        @"^[A-Za-z0-9\s\-\&]+$",
        ErrorMessage = "Event name can only contain letters, numbers, spaces, hyphens and '&'.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select an event date.")]
    public DateOnly EventDate { get; set; }

    [Required(ErrorMessage = "Please enter an event time.")]
    public TimeOnly EventTime { get; set; }

    [Required(ErrorMessage = "Event description is required.")]
    [StringLength(
        500,
        MinimumLength = 10,
        ErrorMessage = "Description must be between 10 and 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [Range(
        1,
        10000,
        ErrorMessage = "Maximum participants must be between 1 and 10,000.")]
    public int MaximumParticipants { get; set; }

    public List<int> VenueIds { get; set; } = new();

    public List<int> ActivityIds { get; set; } = new();

    // Business Information

    public bool IsSmallEvent =>
        MaximumParticipants < 100;

    public bool IsMediumEvent =>
        MaximumParticipants >= 100 &&
        MaximumParticipants < 500;

    public bool IsLargeEvent =>
        MaximumParticipants >= 500;

    public string EventCategory =>
        IsLargeEvent
            ? "Large Event"
            : IsMediumEvent
                ? "Medium Event"
                : "Small Event";

    public string CapacityDisplay =>
        $"{MaximumParticipants:N0} Participants";

    public bool RequiresLargeVenue =>
        MaximumParticipants >= 300;

    public string EventSummary =>
        $"{Name} - {EventCategory} ({MaximumParticipants} Participants)";
}