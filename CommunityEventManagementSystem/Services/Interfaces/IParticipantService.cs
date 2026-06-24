using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IParticipantService
{
    Task<ParticipantDetailsDto?> GetParticipantByIdAsync(int id);
    Task<ParticipantDetailsDto?> GetParticipantByEmailAsync(string email);
    Task<IEnumerable<ParticipantDetailsDto>> GetAllParticipantsAsync();
    Task<IEnumerable<ParticipantDetailsDto>> GetParticipantsByEventAsync(int eventId);
    Task<ParticipantDetailsDto> CreateParticipantAsync(CreateParticipantDto dto);
    Task<ParticipantDetailsDto> UpdateParticipantAsync(int id, UpdateParticipantDto dto);
    Task<bool> DeleteParticipantAsync(int id);
}