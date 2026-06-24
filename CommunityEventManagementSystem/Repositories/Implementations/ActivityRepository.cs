using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
    public ActivityRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Activity?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task<IEnumerable<Activity>> GetActivitiesByEventAsync(int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Event ID must be greater than zero.", nameof(eventId));

        return await _dbSet
            .AsNoTracking()
            .Where(a => a.EventActivities.Any(ea => ea.EventId == eventId))
            .ToListAsync();
    }
}
