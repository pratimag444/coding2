
using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

public interface IRegistrationRepository
    : IGenericRepository<Registration>
{
    Task<bool> RegistrationExistsAsync(
        int participantId,
        int eventId);

    Task<int> GetRegistrationCountAsync(
        int eventId);

    Task<IEnumerable<Registration>>
        GetAllRegistrationsWithDetailsAsync();

    Task<IEnumerable<Registration>>
        GetRegistrationsForEventAsync(
            int eventId);

    Task<IEnumerable<Registration>>
        GetRegistrationsForParticipantAsync(
            int participantId);
}

