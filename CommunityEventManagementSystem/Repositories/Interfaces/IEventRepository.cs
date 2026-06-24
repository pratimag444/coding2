

using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

public interface IEventRepository
    : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();

    Task<Event?> GetEventWithDetailsAsync(int eventId);

    Task<IEnumerable<Event>> GetEventsByDateAsync(
        DateOnly date);

    Task<IEnumerable<Event>> SearchEventsAsync(
        string searchTerm);

    Task<Event?> GetMostPopularEventAsync();

    Task<IEnumerable<Event>> GetAllWithDetailsAsync();
}

