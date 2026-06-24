using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Specialized repository for Activity entity.
/// </summary>
public interface IActivityRepository : IGenericRepository<Activity>
{
    /// <summary>Gets activity by name asynchronously.</summary>
    Task<Activity?> GetByNameAsync(string name);

    /// <summary>Gets activities for a specific event.</summary>
    Task<IEnumerable<Activity>> GetActivitiesByEventAsync(int eventId);
}
