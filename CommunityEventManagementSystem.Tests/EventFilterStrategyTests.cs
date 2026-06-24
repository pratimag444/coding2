using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;
using CommunityEventManagementSystem.Services.Strategies;

namespace CommunityEventManagementSystem.Tests;

public class EventFilterStrategyTests
{
    private static Event CreateEvent(
        string name,
        DateOnly date,
        int venueId = 0,
        int activityId = 0)
    {
        var eventEntity = new Event(
            name,
            date,
            new TimeOnly(10, 0),
            "Test description for filtering.",
            100);

        if (venueId > 0)
        {
            eventEntity.SetVenues(new[] { venueId });
        }

        if (activityId > 0)
        {
            eventEntity.SetActivities(new[] { activityId });
        }

        return eventEntity;
    }

    [Fact]
    public void DateFilter_ReturnsEventsOnSelectedDate()
    {
        var targetDate = new DateOnly(2026, 7, 1);
        var events = new[]
        {
            CreateEvent("Event A", targetDate),
            CreateEvent("Event B", new DateOnly(2026, 7, 2))
        };

        var strategy = new DateEventFilterStrategy(targetDate);
        var result = strategy.Filter(events).ToList();

        Assert.Single(result);
        Assert.Equal("Event A", result[0].Name);
    }

    [Fact]
    public void VenueFilter_ReturnsEventsAtVenue()
    {
        var events = new[]
        {
            CreateEvent("Hall Event", DateOnly.FromDateTime(DateTime.Today), venueId: 1),
            CreateEvent("Park Event", DateOnly.FromDateTime(DateTime.Today), venueId: 2)
        };

        var strategy = new VenueEventFilterStrategy(1);
        var result = strategy.Filter(events).ToList();

        Assert.Single(result);
        Assert.Equal("Hall Event", result[0].Name);
    }

    [Fact]
    public void ActivityFilter_ReturnsEventsWithActivity()
    {
        var events = new[]
        {
            CreateEvent("Workshop Day", DateOnly.FromDateTime(DateTime.Today), activityId: 5),
            CreateEvent("Games Day", DateOnly.FromDateTime(DateTime.Today), activityId: 6)
        };

        var strategy = new ActivityEventFilterStrategy(5);
        var result = strategy.Filter(events).ToList();

        Assert.Single(result);
        Assert.Equal("Workshop Day", result[0].Name);
    }
}
