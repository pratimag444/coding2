using System.ComponentModel.DataAnnotations;

namespace CommunityEventManagementSystem.DTOs;

public class CreateVenueDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Capacity { get; set; }
}