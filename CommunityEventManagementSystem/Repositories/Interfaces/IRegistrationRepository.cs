using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Specialized repository for Registration entity (many-to-many relationship).
/// </summary>
public interface IRegistrationRepository : IGenericRepository<Registration>
{
    /// <summary>Gets registration by event and participant ID.</summary>
    Task<Registration?> GetRegistrationAsync(int eventId, int participantId);

    /// <summary>Gets all registrations for an event.</summary>
    Task<IEnumerable<Registration>> GetEventRegistrationsAsync(int eventId);

    /// <summary>Gets all registrations for a participant.</summary>
    Task<IEnumerable<Registration>> GetParticipantRegistrationsAsync(int participantId);

    /// <summary>Gets registrations with specific status.</summary>
    Task<IEnumerable<Registration>> GetRegistrationsByStatusAsync(RegistrationStatus status);

    /// <summary>Checks if participant is already registered for event.</summary>
    Task<bool> IsParticipantRegisteredAsync(int eventId, int participantId);

    /// <summary>Gets count of active registrations for an event.</summary>
    Task<int> GetActiveRegistrationCountAsync(int eventId);
}
