using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class VenueRepository : GenericRepository<Venue>, IVenueRepository
{
    public VenueRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Venue?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(v => v.Name == name);
    }

    public async Task<IEnumerable<Venue>> GetVenuesByCapacityAsync(int minimumCapacity)
    {
        if (minimumCapacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(minimumCapacity));

        return await _dbSet
            .AsNoTracking()
            .Where(v => v.Capacity >= minimumCapacity)
            .OrderByDescending(v => v.Capacity)
            .ToListAsync();
    }
}
