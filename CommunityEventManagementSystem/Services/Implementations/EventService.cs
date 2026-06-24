using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CommunityEventManagementSystem.Services.Implementations;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public async Task<EventDetailsDto?> GetEventByIdAsync(int id)
    {
        var evt = await _eventRepository.GetEventWithDetailsAsync(id);
        return evt == null ? null : MapToDetailsDto(evt);
    }

    public async Task<IEnumerable<EventDetailsDto>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return events.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<EventDetailsDto>> GetUpcomingEventsAsync()
    {
        var events = await _eventRepository.GetUpcomingEventsAsync();
        return events.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<EventDetailsDto>> GetEventsByVenueAsync(int venueId)
    {\n var events = await _eventRepository.GetEventsByVenueAsync(venueId);
        return events.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<EventDetailsDto>> GetEventsByActivityAsync(int activityId)
    {
        var events = await _eventRepository.GetEventsByActivityAsync(activityId);
        return events.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<EventDetailsDto>> GetEventsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        var events = await _eventRepository.GetEventsByDateRangeAsync(startDate, endDate);
        return events.Select(MapToDetailsDto);
    }

    public async Task<EventDetailsDto> CreateEventAsync(CreateEventDto dto)
    {
        var evt = new Event(dto.Name, dto.EventDate, dto.EventTime, dto.Description, dto.MaximumParticipants);
        await _eventRepository.AddAsync(evt);
        return MapToDetailsDto(evt);
    }

    public async Task<EventDetailsDto> UpdateEventAsync(int id, UpdateEventDto dto)
    {
        var evt = await _eventRepository.GetByIdAsync(id);
        if (evt == null)
            throw new KeyNotFoundException($\"Event {id} not found.\");

        evt.UpdateDetails(dto.Name, dto.EventDate, dto.EventTime, dto.Description, dto.MaximumParticipants);
        await _eventRepository.UpdateAsync(evt);
        return MapToDetailsDto(evt);
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        return await _eventRepository.DeleteAsync(id);
    }

    private EventDetailsDto MapToDetailsDto(Event evt) => new()
    {
        Id = evt.Id,
        Name = evt.Name,
        EventDate = evt.EventDate,
        EventTime = evt.EventTime,
        Description = evt.Description,
        MaximumParticipants = evt.MaximumParticipants,
        RegisteredCount = evt.GetRegistrationCount(),
        CreatedDate = evt.CreatedDate
    };
}