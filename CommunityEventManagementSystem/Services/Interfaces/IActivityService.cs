using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IActivityService
{
    Task<IEnumerable<ActivityDetailsDto>> GetAllActivitiesAsync();

    Task CreateActivityAsync(CreateActivityDto dto);

    Task DeleteActivityAsync(int id);
}
