using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Implementations;
using CommunityEventManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Tests;

public class EventServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly EventService _eventService;

    public EventServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        var venue = new Venue("Test Hall", "1 Test Street", 100);
        var activity = new Activity("Test Workshop", Domain.Enums.ActivityType.Workshop);

        _context.Venues.Add(venue);
        _context.Activities.Add(activity);
        _context.SaveChanges();

        _eventService = new EventService(new EventRepository(_context));
    }

    [Fact]
    public async Task CreateEventAsync_PastDate_ThrowsArgumentException()
    {
        var dto = new CommunityEventManagementSystem.DTOs.CreateEventDto
        {
            Name = "Past Event",
            EventDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
            EventTime = new TimeOnly(9, 0),
            Description = "This event date is in the past.",
            MaximumParticipants = 50
        };

        await Assert.ThrowsAsync<ArgumentException>(
            () => _eventService.CreateEventAsync(dto));
    }

    [Fact]
    public async Task FilterEventsAsync_ByVenue_ReturnsMatchingEvents()
    {
        var venueId = _context.Venues.First().Id;

        var dto = new CommunityEventManagementSystem.DTOs.CreateEventDto
        {
            Name = "Venue Filter Event",
            EventDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            EventTime = new TimeOnly(10, 0),
            Description = "Event linked to a specific venue.",
            MaximumParticipants = 40,
            VenueIds = new List<int> { venueId }
        };

        await _eventService.CreateEventAsync(dto);

        var filtered = await _eventService.FilterEventsAsync(
            null,
            venueId,
            null,
            null);

        Assert.Single(filtered);
        Assert.Equal("Venue Filter Event", filtered.First().Name);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
