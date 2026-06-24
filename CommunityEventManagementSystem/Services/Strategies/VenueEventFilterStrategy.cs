
using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Services.Strategies;

public class VenueEventFilterStrategy
    : IEventFilterStrategy
{
    private readonly int _venueId;

    public VenueEventFilterStrategy(
        int venueId)
    {
        _venueId = venueId;
    }

    public IEnumerable<Event> Filter(
        IEnumerable<Event> events)
    {
        return events.Where(
            e => e.EventVenues.Any(
                v => v.VenueId == _venueId));
    }
}

