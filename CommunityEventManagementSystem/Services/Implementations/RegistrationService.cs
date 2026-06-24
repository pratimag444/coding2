using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEventRepository _eventRepository;

    public RegistrationService(IRegistrationRepository registrationRepository, IEventRepository eventRepository)
    {
        _registrationRepository = registrationRepository ?? throw new ArgumentNullException(nameof(registrationRepository));
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    public async Task<RegistrationDetailsDto?> GetRegistrationByIdAsync(int id)
    {
        var registration = await _registrationRepository.GetByIdAsync(id);
        return registration == null ? null : MapToDetailsDto(registration);
    }

    public async Task<RegistrationDetailsDto?> GetRegistrationAsync(int eventId, int participantId)
    {
        var registration = await _registrationRepository.GetRegistrationAsync(eventId, participantId);
        return registration == null ? null : MapToDetailsDto(registration);
    }

    public async Task<IEnumerable<RegistrationDetailsDto>> GetEventRegistrationsAsync(int eventId)
    {
        var registrations = await _registrationRepository.GetEventRegistrationsAsync(eventId);
        return registrations.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<RegistrationDetailsDto>> GetParticipantRegistrationsAsync(int participantId)
    {
        var registrations = await _registrationRepository.GetParticipantRegistrationsAsync(participantId);
        return registrations.Select(MapToDetailsDto);
    }

    public async Task<RegistrationDetailsDto> RegisterParticipantAsync(CreateRegistrationDto dto)
    {
        var evt = await _eventRepository.GetByIdAsync(dto.EventId);
        if (evt == null)
            throw new KeyNotFoundException($"Event {dto.EventId} not found.");

        if (evt.IsPastEvent())
            throw new InvalidOperationException("Cannot register for past events.");

        if (evt.HasReachedCapacity())
            throw new InvalidOperationException("Event has reached maximum capacity.");

        var existing = await _registrationRepository.IsParticipantRegisteredAsync(dto.EventId, dto.ParticipantId);
        if (existing)
            throw new InvalidOperationException("Participant is already registered for this event.");

        var registration = new Registration(dto.EventId, dto.ParticipantId);
        await _registrationRepository.AddAsync(registration);
        return MapToDetailsDto(registration);
    }

    public async Task<bool> CancelRegistrationAsync(int registrationId)
    {
        var registration = await _registrationRepository.GetByIdAsync(registrationId);
        if (registration == null)
            return false;

        registration.Cancel();
        await _registrationRepository.UpdateAsync(registration);
        return true;
    }

    public async Task<bool> ConfirmRegistrationAsync(int registrationId)
    {
        var registration = await _registrationRepository.GetByIdAsync(registrationId);
        if (registration == null)
            return false;

        registration.Confirm();
        await _registrationRepository.UpdateAsync(registration);
        return true;
    }

    private RegistrationDetailsDto MapToDetailsDto(Registration registration) => new()
    {
        Id = registration.Id,
        EventId = registration.EventId,
        ParticipantId = registration.ParticipantId,
        RegistrationDate = registration.RegistrationDate,
        Status = registration.Status.ToString()
    };
}
