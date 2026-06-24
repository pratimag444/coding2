using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class ActivityRepository
    : GenericRepository<Activity>,
      IActivityRepository
{
    public ActivityRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }
}
