using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;

    public ParticipantService(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public async Task<ParticipantDetailsDto?> GetParticipantByIdAsync(int id)
    {
        var participant = await _participantRepository.GetByIdAsync(id);
        return participant == null ? null : MapToDetailsDto(participant);
    }

    public async Task<ParticipantDetailsDto?> GetParticipantByEmailAsync(string email)
    {
        var participant = await _participantRepository.GetByEmailAsync(email);
        return participant == null ? null : MapToDetailsDto(participant);
    }

    public async Task<IEnumerable<ParticipantDetailsDto>> GetAllParticipantsAsync()
    {
        var participants = await _participantRepository.GetAllAsync();
        return participants.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<ParticipantDetailsDto>> GetParticipantsByEventAsync(int eventId)
    {
        var participants = await _participantRepository.GetParticipantsByEventAsync(eventId);
        return participants.Select(MapToDetailsDto);
    }

    public async Task<ParticipantDetailsDto> CreateParticipantAsync(CreateParticipantDto dto)
    {
        var participant = new Participant(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
        await _participantRepository.AddAsync(participant);
        return MapToDetailsDto(participant);
    }

    public async Task<ParticipantDetailsDto> UpdateParticipantAsync(int id, UpdateParticipantDto dto)
    {
        var participant = await _participantRepository.GetByIdAsync(id);
        if (participant == null)
            throw new KeyNotFoundException($\"Participant {id} not found.\");

        participant.UpdateDetails(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);
        await _participantRepository.UpdateAsync(participant);
        return MapToDetailsDto(participant);
    }

    public async Task<bool> DeleteParticipantAsync(int id)
    {
        return await _participantRepository.DeleteAsync(id);
    }

    private ParticipantDetailsDto MapToDetailsDto(Participant participant) => new()
    {
        Id = participant.Id,
        FirstName = participant.FirstName,
        LastName = participant.LastName,
        Email = participant.Email,
        PhoneNumber = participant.PhoneNumber,
        CreatedDate = participant.CreatedDate
    };
}