
using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Services.Strategies;

public class DateEventFilterStrategy
    : IEventFilterStrategy
{
    private readonly DateOnly _date;

    public DateEventFilterStrategy(
        DateOnly date)
    {
        _date = date;
    }

    public IEnumerable<Event> Filter(
        IEnumerable<Event> events)
    {
        return events.Where(
            e => e.EventDate == _date);
    }
}

