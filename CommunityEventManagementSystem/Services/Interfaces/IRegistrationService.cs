using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IRegistrationService
{
    Task<RegistrationDetailsDto?> GetRegistrationByIdAsync(int id);
    Task<RegistrationDetailsDto?> GetRegistrationAsync(int eventId, int participantId);
    Task<IEnumerable<RegistrationDetailsDto>> GetEventRegistrationsAsync(int eventId);
    Task<IEnumerable<RegistrationDetailsDto>> GetParticipantRegistrationsAsync(int participantId);
    Task<RegistrationDetailsDto> RegisterParticipantAsync(CreateRegistrationDto dto);
    Task<bool> CancelRegistrationAsync(int registrationId);
    Task<bool> ConfirmRegistrationAsync(int registrationId);
}