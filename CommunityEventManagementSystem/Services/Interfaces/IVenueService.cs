using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IVenueService
{
    Task<VenueDetailsDto?> GetVenueByIdAsync(int id);
    Task<VenueDetailsDto?> GetVenueByNameAsync(string name);
    Task<IEnumerable<VenueDetailsDto>> GetAllVenuesAsync();
    Task<IEnumerable<VenueDetailsDto>> GetVenuesByCapacityAsync(int minimumCapacity);
    Task<VenueDetailsDto> CreateVenueAsync(CreateVenueDto dto);
    Task<VenueDetailsDto> UpdateVenueAsync(int id, CreateVenueDto dto);
    Task<bool> DeleteVenueAsync(int id);
}