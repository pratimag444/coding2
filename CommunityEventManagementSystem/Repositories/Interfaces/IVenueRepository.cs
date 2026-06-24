using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Specialized repository for Venue entity.
/// </summary>
public interface IVenueRepository : IGenericRepository<Venue>
{
    /// <summary>Gets venue by name asynchronously.</summary>
    Task<Venue?> GetByNameAsync(string name);

    /// <summary>Gets venues with sufficient capacity.</summary>
    Task<IEnumerable<Venue>> GetVenuesByCapacityAsync(int minimumCapacity);
}
