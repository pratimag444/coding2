using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

public interface IParticipantRepository
    : IGenericRepository<Participant>
{
    Task<Participant?> GetByEmailAsync(
        string email);

    Task<Participant?> GetParticipantWithRegistrationsAsync(
        int id);

    Task<IEnumerable<Participant>>
        GetMostActiveParticipantsAsync();

    Task<IEnumerable<Participant>>
        GetAllWithRegistrationsAsync();
}