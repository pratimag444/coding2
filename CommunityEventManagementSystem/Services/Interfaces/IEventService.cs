using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDetailsDto>>
        GetAllEventsAsync();

    Task<EventDetailsDto?>
        GetEventByIdAsync(
            int id);

    Task<IEnumerable<EventDetailsDto>>
        GetUpcomingEventsAsync();

    Task<IEnumerable<EventDetailsDto>>
        GetEventsByDateAsync(
            DateOnly date);

    Task<IEnumerable<EventDetailsDto>>
        SearchEventsAsync(
            string searchTerm);

    Task<IEnumerable<EventDetailsDto>>
        FilterEventsAsync(
            DateOnly? date,
            int? venueId,
            int? activityId,
            string? searchTerm,
            bool upcomingOnly = false);

    Task<EventDetailsDto?>
        GetMostPopularEventAsync();

    Task CreateEventAsync(
        CreateEventDto dto);

    Task UpdateEventAsync(
        UpdateEventDto dto);

    Task DeleteEventAsync(
        int id);
}
