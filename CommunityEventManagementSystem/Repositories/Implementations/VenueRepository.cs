using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class VenueRepository
    : GenericRepository<Venue>,
      IVenueRepository
{
    public VenueRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }
}