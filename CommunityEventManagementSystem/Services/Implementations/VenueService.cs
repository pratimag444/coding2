using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(
        IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<IEnumerable<VenueDetailsDto>>
        GetAllVenuesAsync()
    {
        var venues =
            await _venueRepository.GetAllAsync();

        return venues.Select(v =>
            new VenueDetailsDto
            {
                Id = v.Id,
                Name = v.Name,
                Address = v.Address,
                Capacity = v.Capacity
            });
    }

    public async Task CreateVenueAsync(
        CreateVenueDto dto)
    {
        var venue =
            new Venue(
                dto.Name,
                dto.Address,
                dto.Capacity);

        await _venueRepository
            .AddAsync(venue);

        await _venueRepository
            .SaveChangesAsync();
    }
}