
namespace CommunityEventManagementSystem.DTOs;

public class EventDetailsDto
{
   
    // Core Properties
 

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateOnly EventDate { get; set; }

    public TimeOnly EventTime { get; set; }

    public string Description { get; set; } = string.Empty;

    public int RegistrationCount { get; set; }

    public int MaximumParticipants { get; set; }

    public List<int> VenueIds { get; set; } = new();

    public List<int> ActivityIds { get; set; } = new();

    public List<string> VenueNames { get; set; } = new();

    public List<string> ActivityNames { get; set; } = new();

    public string VenuesDisplay =>
        VenueNames.Count > 0
            ? string.Join(", ", VenueNames)
            : "Not assigned";

    public string ActivitiesDisplay =>
        ActivityNames.Count > 0
            ? string.Join(", ", ActivityNames)
            : "Not assigned";


    // Capacity Analytics
 

    public int RemainingSeats =>
        MaximumParticipants - RegistrationCount;

    public bool IsFull =>
        RegistrationCount >= MaximumParticipants;

    public bool IsAlmostFull =>
        OccupancyPercentage >= 80 &&
        OccupancyPercentage < 100;

    public bool HasAvailableSeats =>
        RemainingSeats > 0;

    public double OccupancyPercentage =>
        MaximumParticipants == 0
            ? 0
            : (double)RegistrationCount /
              MaximumParticipants * 100;

   
    // Event Status
   

    public bool IsUpcoming =>
        EventDate >=
        DateOnly.FromDateTime(DateTime.Today);

    public bool IsToday =>
        EventDate ==
        DateOnly.FromDateTime(DateTime.Today);

    public bool IsPastEvent =>
        EventDate <
        DateOnly.FromDateTime(DateTime.Today);

    public string Status =>
        IsFull
            ? "Full"
            : IsAlmostFull
                ? "Almost Full"
                : "Open";

 
    // Event Category
  

    public bool IsLargeEvent =>
        MaximumParticipants >= 500;

    public bool IsMediumEvent =>
        MaximumParticipants >= 100 &&
        MaximumParticipants < 500;

    public bool IsSmallEvent =>
        MaximumParticipants < 100;

    public string EventCategory =>
        IsLargeEvent
            ? "Large Event"
            : IsMediumEvent
                ? "Medium Event"
                : "Small Event";

    
    // Display Helpers
  

    public string EventDateDisplay =>
        EventDate.ToString("dd MMM yyyy");

    public string EventTimeDisplay =>
        EventTime.ToString("HH:mm");

    public string OccupancyDisplay =>
        $"{OccupancyPercentage:F0}%";

    public string CapacitySummary =>
        $"{RegistrationCount}/{MaximumParticipants}";


    // Dashboard Indicators
  

    public string StatusBadgeColor =>
        IsFull
            ? "danger"
            : IsAlmostFull
                ? "warning"
                : "success";

    public string OccupancyColor =>
        OccupancyPercentage >= 90
            ? "danger"
            : OccupancyPercentage >= 70
                ? "warning"
                : "success";
}

