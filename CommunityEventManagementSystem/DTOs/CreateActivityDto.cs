using System.ComponentModel.DataAnnotations;
using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.DTOs;

public class CreateActivityDto
{
    [Required(ErrorMessage = "Activity name is required.")]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select an activity type.")]
    public ActivityType ActivityType { get; set; } = ActivityType.Workshop;
}
