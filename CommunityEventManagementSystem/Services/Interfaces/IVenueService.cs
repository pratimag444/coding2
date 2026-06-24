using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<VenueDetailsDto>>
        GetAllVenuesAsync();

    Task CreateVenueAsync(
        CreateVenueDto dto);
}