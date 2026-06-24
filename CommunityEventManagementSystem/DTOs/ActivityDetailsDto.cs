using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.DTOs;

public class ActivityDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ActivityType ActivityType { get; set; }

    public string ActivityTypeDisplay =>
        ActivityType.ToString();
}
