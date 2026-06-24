using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Specialized repository for Participant entity.
/// </summary>
public interface IParticipantRepository : IGenericRepository<Participant>
{
    /// <summary>Gets participant by email asynchronously.</summary>
    Task<Participant?> GetByEmailAsync(string email);

    /// <summary>Gets participants registered for a specific event.</summary>
    Task<IEnumerable<Participant>> GetParticipantsByEventAsync(int eventId);

    /// <summary>Gets participant with all registrations.</summary>
    Task<Participant?> GetParticipantWithRegistrationsAsync(int participantId);
}
