using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IEventService
{
    Task<EventDetailsDto?> GetEventByIdAsync(int id);
    Task<IEnumerable<EventDetailsDto>> GetAllEventsAsync();
    Task<IEnumerable<EventDetailsDto>> GetUpcomingEventsAsync();
    Task<IEnumerable<EventDetailsDto>> GetEventsByVenueAsync(int venueId);
    Task<IEnumerable<EventDetailsDto>> GetEventsByActivityAsync(int activityId);
    Task<IEnumerable<EventDetailsDto>> GetEventsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
    Task<EventDetailsDto> CreateEventAsync(CreateEventDto dto);
    Task<EventDetailsDto> UpdateEventAsync(int id, UpdateEventDto dto);
    Task<bool> DeleteEventAsync(int id);
}