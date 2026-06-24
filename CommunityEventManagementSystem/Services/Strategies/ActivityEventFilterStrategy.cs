
using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Services.Strategies;

public class ActivityEventFilterStrategy
    : IEventFilterStrategy
{
    private readonly int _activityId;

    public ActivityEventFilterStrategy(
        int activityId)
    {
        _activityId = activityId;
    }

    public IEnumerable<Event> Filter(
        IEnumerable<Event> events)
    {
        return events.Where(
            e => e.EventActivities.Any(
                a => a.ActivityId == _activityId));
    }
}

