namespace CommunityEventManagementSystem.DTOs;

public class VenueDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int Capacity { get; set; }
}