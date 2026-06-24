using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));
    }

    public async Task<VenueDetailsDto?> GetVenueByIdAsync(int id)
    {
        var venue = await _venueRepository.GetByIdAsync(id);
        return venue == null ? null : MapToDetailsDto(venue);
    }

    public async Task<VenueDetailsDto?> GetVenueByNameAsync(string name)
    {
        var venue = await _venueRepository.GetByNameAsync(name);
        return venue == null ? null : MapToDetailsDto(venue);
    }

    public async Task<IEnumerable<VenueDetailsDto>> GetAllVenuesAsync()
    {
        var venues = await _venueRepository.GetAllAsync();
        return venues.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<VenueDetailsDto>> GetVenuesByCapacityAsync(int minimumCapacity)
    {
        var venues = await _venueRepository.GetVenuesByCapacityAsync(minimumCapacity);
        return venues.Select(MapToDetailsDto);
    }

    public async Task<VenueDetailsDto> CreateVenueAsync(CreateVenueDto dto)
    {
        var venue = new Venue(dto.Name, dto.Address, dto.Capacity);
        await _venueRepository.AddAsync(venue);
        return MapToDetailsDto(venue);
    }

    public async Task<VenueDetailsDto> UpdateVenueAsync(int id, CreateVenueDto dto)
    {
        var venue = await _venueRepository.GetByIdAsync(id);
        if (venue == null)
            throw new KeyNotFoundException($"Venue {id} not found.");

        venue.UpdateDetails(dto.Name, dto.Address, dto.Capacity);
        await _venueRepository.UpdateAsync(venue);
        return MapToDetailsDto(venue);
    }

    public async Task<bool> DeleteVenueAsync(int id)
    {
        return await _venueRepository.DeleteAsync(id);
    }

    private VenueDetailsDto MapToDetailsDto(Venue venue) => new()
    {
        Id = venue.Id,
        Name = venue.Name,
        Address = venue.Address,
        Capacity = venue.Capacity,
        CreatedDate = venue.CreatedDate
    };
}
