using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;

    public ParticipantService(
        IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task<IEnumerable<ParticipantDetailsDto>>
        GetAllParticipantsAsync()
    {
        var participants =
            await _participantRepository
                .GetAllWithRegistrationsAsync();

        return participants.Select(MapToDto);
    }

    public async Task<ParticipantDetailsDto?>
      GetParticipantByIdAsync(
          int id)
    {
        var participant =
            await _participantRepository
                .GetParticipantWithRegistrationsAsync(id);

        return participant is null
            ? null
            : MapToDto(participant);
    }

    public async Task<IEnumerable<ParticipantDetailsDto>>
        GetMostActiveParticipantsAsync()
    {
        var participants =
            await _participantRepository
                .GetMostActiveParticipantsAsync();

        return participants.Select(MapToDto);
    }

    public async Task<int>
        GetTotalParticipantsAsync()
    {
        var participants =
            await _participantRepository.GetAllAsync();

        return participants.Count();
    }

    public async Task CreateParticipantAsync(
        CreateParticipantDto dto)
    {
        var participant =
            new Participant(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.PhoneNumber);

        await _participantRepository
            .AddAsync(participant);

        await _participantRepository
            .SaveChangesAsync();
    }

    public async Task UpdateParticipantAsync(
        UpdateParticipantDto dto)
    {
        var participant =
            await _participantRepository
                .GetByIdAsync(dto.Id);

        if (participant is null)
        {
            throw new Exception(
                "Participant not found.");
        }

        participant.UpdateDetails(
            dto.FirstName,
            dto.LastName,
            dto.Email,
            dto.PhoneNumber);

        _participantRepository.Update(
            participant);

        await _participantRepository
            .SaveChangesAsync();
    }

    public async Task DeleteParticipantAsync(
        int id)
    {
        var participant =
            await _participantRepository
                .GetByIdAsync(id);

        if (participant is null)
            return;


        _participantRepository.Delete(
            participant);

        await _participantRepository
            .SaveChangesAsync();
    }

    private static ParticipantDetailsDto
        MapToDto(
            Participant participant)
    {
        return new ParticipantDetailsDto
        {
            Id = participant.Id,

            FullName =
                $"{participant.FirstName} {participant.LastName}",

            Email = participant.Email,

            PhoneNumber =
                participant.PhoneNumber,

            RegistrationCount =
                participant.Registrations
                    .Count(r => r.Status
                        != Domain.Enums.RegistrationStatus.Cancelled)
        };
    }
}