using CommunityEventManagementSystem.DTOs;

namespace CommunityEventManagementSystem.Services.Interfaces;

public interface IActivityService
{
    Task<ActivityDetailsDto?> GetActivityByIdAsync(int id);
    Task<ActivityDetailsDto?> GetActivityByNameAsync(string name);
    Task<IEnumerable<ActivityDetailsDto>> GetAllActivitiesAsync();
    Task<IEnumerable<ActivityDetailsDto>> GetActivitiesByEventAsync(int eventId);
    Task<ActivityDetailsDto> CreateActivityAsync(CreateActivityDto dto);
    Task<ActivityDetailsDto> UpdateActivityAsync(int id, CreateActivityDto dto);
    Task<bool> DeleteActivityAsync(int id);
}