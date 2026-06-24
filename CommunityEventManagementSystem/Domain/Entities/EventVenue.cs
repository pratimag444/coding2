namespace CommunityEventManagementSystem.Domain.Entities;

public class EventVenue
{
    public int EventId { get; set; }

    public Event? Event { get; set; }

    public int VenueId { get; set; }

    public Venue? Venue { get; set; }
}