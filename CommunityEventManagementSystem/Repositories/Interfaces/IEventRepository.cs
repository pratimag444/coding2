using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Specialized repository for Event entity.
/// Provides domain-specific queries for event filtering and retrieval.
/// </summary>
public interface IEventRepository : IGenericRepository<Event>
{
    /// <summary>Gets upcoming events asynchronously.</summary>
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();

    /// <summary>Gets events by venue ID asynchronously.</summary>
    Task<IEnumerable<Event>> GetEventsByVenueAsync(int venueId);

    /// <summary>Gets events by activity type asynchronously.</summary>
    Task<IEnumerable<Event>> GetEventsByActivityAsync(int activityId);

    /// <summary>Gets events by date range asynchronously.</summary>
    Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateOnly startDate, DateOnly endDate);

    /// <summary>Gets event with all related data (venues, activities, registrations).</summary>
    Task<Event?> GetEventWithDetailsAsync(int eventId);
}
