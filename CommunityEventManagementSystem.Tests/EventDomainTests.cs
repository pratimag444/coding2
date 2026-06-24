using CommunityEventManagementSystem.Domain.Entities;
using Xunit;

namespace CommunityEventManagementSystem.Tests;

public class EventDomainTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        var eventEntity = new Event(
            "Community Fair",
            new DateOnly(2026, 8, 15),
            new TimeOnly(14, 30),
            "A local community fair event.",
            200);

        Assert.Equal("Community Fair", eventEntity.Name);
        Assert.Equal(200, eventEntity.MaximumParticipants);
        Assert.False(eventEntity.HasReachedCapacity());
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Constructor_EmptyName_ThrowsArgumentException(string name)
    {
        Assert.Throws<ArgumentException>(() =>
            new Event(
                name,
                DateOnly.FromDateTime(DateTime.Today),
                new TimeOnly(9, 0),
                "Valid description here.",
                50));
    }

    [Fact]
    public void GetRegistrationCount_ExcludesCancelledRegistrations()
    {
        var eventEntity = new Event(
            "Test Event",
            DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
            new TimeOnly(10, 0),
            "Description for registration test.",
            10);

        var registrationsField = typeof(Event)
            .GetField("_registrations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;

        var registrations = (List<Registration>)registrationsField.GetValue(eventEntity)!;
        registrations.Add(new Registration(1, 1));

        var cancelled = new Registration(2, 1);
        cancelled.Cancel();
        registrations.Add(cancelled);

        Assert.Equal(1, eventEntity.GetRegistrationCount());
    }

    [Fact]
    public void SetVenues_AssignsDistinctVenueIds()
    {
        var eventEntity = new Event(
            "Venue Test",
            DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
            new TimeOnly(11, 0),
            "Venue assignment test event.",
            80);

        eventEntity.SetVenues(new[] { 1, 1, 2 });

        Assert.Equal(2, eventEntity.EventVenues.Count);
    }
}
