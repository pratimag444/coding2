using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Exceptions;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;
using CommunityEventManagementSystem.Services.Strategies;

namespace CommunityEventManagementSystem.Services.Implementations;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(
        IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDetailsDto>>
        GetAllEventsAsync()
    {
        var events =
            await _eventRepository
                .GetAllWithDetailsAsync();

        return events.Select(MapToDto);
    }

    public async Task<EventDetailsDto?>
        GetEventByIdAsync(int id)
    {
        var eventEntity =
            await _eventRepository
                .GetEventWithDetailsAsync(id);

        return eventEntity is null
            ? null
            : MapToDto(eventEntity);
    }

    public async Task<IEnumerable<EventDetailsDto>>
        GetUpcomingEventsAsync()
    {
        var events =
            await _eventRepository
                .GetUpcomingEventsAsync();

        return events.Select(MapToDto);
    }

    public async Task<IEnumerable<EventDetailsDto>>
        GetEventsByDateAsync(DateOnly date)
    {
        var events =
            await _eventRepository
                .GetEventsByDateAsync(date);

        return events.Select(MapToDto);
    }

    public async Task<IEnumerable<EventDetailsDto>>
        SearchEventsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<EventDetailsDto>();
        }

        var events =
            await _eventRepository
                .SearchEventsAsync(searchTerm);

        return events.Select(MapToDto);
    }

    public async Task<IEnumerable<EventDetailsDto>>
        FilterEventsAsync(
            DateOnly? date,
            int? venueId,
            int? activityId,
            string? searchTerm,
            bool upcomingOnly = false)
    {
        var events =
            await _eventRepository
                .GetAllWithDetailsAsync();

        IEnumerable<Event> filtered = events;

        if (upcomingOnly)
        {
            filtered = filtered.Where(
                e => e.EventDate >= DateOnly.FromDateTime(DateTime.Today));
        }

        if (date.HasValue)
        {
            filtered = new DateEventFilterStrategy(date.Value)
                .Filter(filtered);
        }

        if (venueId is > 0)
        {
            filtered = new VenueEventFilterStrategy(venueId.Value)
                .Filter(filtered);
        }

        if (activityId is > 0)
        {
            filtered = new ActivityEventFilterStrategy(activityId.Value)
                .Filter(filtered);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filtered = filtered.Where(
                e => e.Name.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase)
                    || e.Description.Contains(
                        searchTerm,
                        StringComparison.OrdinalIgnoreCase));
        }

        return filtered.Select(MapToDto);
    }

    public async Task<EventDetailsDto?>
        GetMostPopularEventAsync()
    {
        var eventEntity =
            await _eventRepository
                .GetMostPopularEventAsync();

        return eventEntity is null
            ? null
            : MapToDto(eventEntity);
    }

    public async Task CreateEventAsync(
        CreateEventDto dto)
    {
        if (dto.EventDate <
            DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException(
                "Event date cannot be in the past.");
        }

        var eventEntity =
            new Event(
                dto.Name,
                dto.EventDate,
                dto.EventTime,
                dto.Description,
                dto.MaximumParticipants);

        eventEntity.SetVenues(dto.VenueIds);
        eventEntity.SetActivities(dto.ActivityIds);

        await _eventRepository.AddAsync(eventEntity);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task UpdateEventAsync(
        UpdateEventDto dto)
    {
        var eventEntity =
            await _eventRepository
                .GetEventWithDetailsAsync(dto.Id);

        if (eventEntity is null)
        {
            throw new EventNotFoundException(dto.Id);
        }

        eventEntity.UpdateDetails(
            dto.Name,
            dto.EventDate,
            dto.EventTime,
            dto.Description,
            dto.MaximumParticipants);

        eventEntity.SetVenues(dto.VenueIds);
        eventEntity.SetActivities(dto.ActivityIds);

        _eventRepository.Update(eventEntity);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        var eventEntity =
            await _eventRepository
                .GetEventWithDetailsAsync(id);

        if (eventEntity is null)
        {
            throw new EventNotFoundException(id);
        }

        _eventRepository.Delete(eventEntity);
        await _eventRepository.SaveChangesAsync();
    }

    private static EventDetailsDto MapToDto(Event e)
    {
        return new EventDetailsDto
        {
            Id = e.Id,
            Name = e.Name,
            EventDate = e.EventDate,
            EventTime = e.EventTime,
            Description = e.Description,
            RegistrationCount = e.GetRegistrationCount(),
            MaximumParticipants = e.MaximumParticipants,
            VenueIds = e.EventVenues
                .Select(ev => ev.VenueId)
                .ToList(),
            ActivityIds = e.EventActivities
                .Select(ea => ea.ActivityId)
                .ToList(),
            VenueNames = e.EventVenues
                .Where(ev => ev.Venue is not null)
                .Select(ev => ev.Venue!.Name)
                .ToList(),
            ActivityNames = e.EventActivities
                .Where(ea => ea.Activity is not null)
                .Select(ea => ea.Activity!.Name)
                .ToList()
        };
    }
}
