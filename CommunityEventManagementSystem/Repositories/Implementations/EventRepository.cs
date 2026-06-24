using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

/// <summary>
/// Event repository implementation with specialized queries.
/// Demonstrates inheritance from generic repository.
/// </summary>
public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context) : base(context) { }

    /// <summary>Gets all upcoming events (future dates).</summary>
    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        return await _dbSet
            .AsNoTracking()
            .Where(e => e.EventDate >= today)
            .OrderBy(e => e.EventDate)
            .ThenBy(e => e.EventTime)
            .ToListAsync();
    }

    /// <summary>Gets events at a specific venue.</summary>
    public async Task<IEnumerable<Event>> GetEventsByVenueAsync(int venueId)
    {
        if (venueId <= 0)
            throw new ArgumentException("Venue ID must be greater than zero.", nameof(venueId));

        return await _dbSet
            .AsNoTracking()
            .Where(e => e.EventVenues.Any(ev => ev.VenueId == venueId))
            .ToListAsync();
    }

    /// <summary>Gets events with a specific activity.</summary>
    public async Task<IEnumerable<Event>> GetEventsByActivityAsync(int activityId)
    {
        if (activityId <= 0)
            throw new ArgumentException("Activity ID must be greater than zero.", nameof(activityId));

        return await _dbSet
            .AsNoTracking()
            .Where(e => e.EventActivities.Any(ea => ea.ActivityId == activityId))
            .ToListAsync();
    }

    /// <summary>Gets events within a date range.</summary>
    public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date must be before end date.");

        return await _dbSet
            .AsNoTracking()
            .Where(e => e.EventDate >= startDate && e.EventDate <= endDate)
            .OrderBy(e => e.EventDate)
            .ToListAsync();
    }

    /// <summary>Gets event with all related data loaded.</summary>
    public async Task<Event?> GetEventWithDetailsAsync(int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Event ID must be greater than zero.", nameof(eventId));

        return await _dbSet
            .AsNoTracking()
            .Include(e => e.EventVenues)
            .Include(e => e.EventActivities)
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.Id == eventId);
    }
}
