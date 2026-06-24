using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IParticipantService
{
    Task<IEnumerable<ParticipantDetailsDto>>
        GetAllParticipantsAsync();

    Task<ParticipantDetailsDto?>
        GetParticipantByIdAsync(
            int id);

    Task<IEnumerable<ParticipantDetailsDto>>
        GetMostActiveParticipantsAsync();

    Task<int>
        GetTotalParticipantsAsync();

    Task CreateParticipantAsync(
        CreateParticipantDto dto);

    Task UpdateParticipantAsync(
        UpdateParticipantDto dto);

    Task DeleteParticipantAsync(
        int id);
}