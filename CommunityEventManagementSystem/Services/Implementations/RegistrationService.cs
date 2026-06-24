
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Exceptions;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class RegistrationService : IRegistrationService
{
    private readonly IEventRepository _eventRepository;
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IParticipantRepository _participantRepository;

    public RegistrationService(
        IEventRepository eventRepository,
        IRegistrationRepository registrationRepository,
        IParticipantRepository participantRepository)
    {
        _eventRepository = eventRepository;
        _registrationRepository = registrationRepository;
        _participantRepository = participantRepository;
    }

    public async Task RegisterAsync(
        int participantId,
        int eventId)
    {
        var participant =
            await _participantRepository
                .GetByIdAsync(participantId);

        if (participant is null)
            throw new Exception(
                "Participant not found.");

        var eventEntity =
            await _eventRepository
                .GetEventWithDetailsAsync(eventId);

        if (eventEntity is null)
            throw new EventNotFoundException(eventId);

        if (eventEntity.IsPastEvent())
            throw new PastEventRegistrationException();

        var alreadyRegistered =
            await _registrationRepository
                .RegistrationExistsAsync(
                    participantId,
                    eventId);

        if (alreadyRegistered)
            throw new DuplicateRegistrationException();

        if (eventEntity.HasReachedCapacity())
            throw new EventCapacityExceededException();

        var registration =
            new Registration(
                participantId,
                eventId);

        await _registrationRepository
            .AddAsync(registration);

        await _registrationRepository
            .SaveChangesAsync();
    }

    public async Task ConfirmRegistrationAsync(
        int registrationId)
    {
        var registration =
            await _registrationRepository
                .GetByIdAsync(registrationId);

        if (registration is null)
        {
            throw new ArgumentException(
                "Registration not found.");
        }

        registration.Confirm();

        _registrationRepository.Update(registration);
        await _registrationRepository.SaveChangesAsync();
    }

    public async Task CancelRegistrationAsync(
        int registrationId)
    {
        var registration =
            await _registrationRepository
                .GetByIdAsync(registrationId);

        if (registration is null)
            return;

        registration.Cancel();

        _registrationRepository.Update(
            registration);

        await _registrationRepository
            .SaveChangesAsync();
    }

    public async Task<IEnumerable<Registration>>
        GetParticipantRegistrationsAsync(
            int participantId)
    {
        return await _registrationRepository
            .GetRegistrationsForParticipantAsync(
                participantId);
    }

    public async Task<IEnumerable<RegistrationDetailsDto>>
        GetAllRegistrationsAsync()
    {
        var registrations =
            await _registrationRepository
                .GetAllRegistrationsWithDetailsAsync();

        return registrations.Select(r =>
            new RegistrationDetailsDto
            {
                Id = r.Id,

                ParticipantName =
                    r.Participant is null
                        ? "Unknown Participant"
                        : $"{r.Participant.FirstName} {r.Participant.LastName}",

                EventName =
                    r.Event?.Name
                    ?? "Unknown Event",

                RegistrationDate =
                    r.RegistrationDate,

                Status =
                    r.Status.ToString()
            });
    }

    public async Task<int>
        GetTotalRegistrationsAsync()
    {
        var registrations =
            await _registrationRepository
                .GetAllAsync();

        return registrations.Count();
    }

    public async Task<int>
        GetRegistrationCountForEventAsync(
            int eventId)
    {
        return await _registrationRepository
            .GetRegistrationCountAsync(
                eventId);
    }
}

