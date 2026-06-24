
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IRegistrationService
{
    Task RegisterAsync(
        int participantId,
        int eventId);

    Task CancelRegistrationAsync(
        int registrationId);

    Task ConfirmRegistrationAsync(
        int registrationId);

    Task<IEnumerable<Registration>>
        GetParticipantRegistrationsAsync(
            int participantId);

    Task<IEnumerable<RegistrationDetailsDto>>
        GetAllRegistrationsAsync();

    Task<int>
        GetTotalRegistrationsAsync();

    Task<int>
        GetRegistrationCountForEventAsync(
            int eventId);
}

