using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
    }

    public async Task<ActivityDetailsDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        return activity == null ? null : MapToDetailsDto(activity);
    }

    public async Task<ActivityDetailsDto?> GetActivityByNameAsync(string name)
    {
        var activity = await _activityRepository.GetByNameAsync(name);
        return activity == null ? null : MapToDetailsDto(activity);
    }

    public async Task<IEnumerable<ActivityDetailsDto>> GetAllActivitiesAsync()
    {
        var activities = await _activityRepository.GetAllAsync();
        return activities.Select(MapToDetailsDto);
    }

    public async Task<IEnumerable<ActivityDetailsDto>> GetActivitiesByEventAsync(int eventId)
    {
        var activities = await _activityRepository.GetActivitiesByEventAsync(eventId);
        return activities.Select(MapToDetailsDto);
    }

    public async Task<ActivityDetailsDto> CreateActivityAsync(CreateActivityDto dto)
    {
        var activity = new Activity(dto.Name, dto.Description);
        await _activityRepository.AddAsync(activity);
        return MapToDetailsDto(activity);
    }

    public async Task<ActivityDetailsDto> UpdateActivityAsync(int id, CreateActivityDto dto)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
            throw new KeyNotFoundException($"Activity {id} not found.");

        activity.UpdateDetails(dto.Name, dto.Description);
        await _activityRepository.UpdateAsync(activity);
        return MapToDetailsDto(activity);
    }

    public async Task<bool> DeleteActivityAsync(int id)
    {
        return await _activityRepository.DeleteAsync(id);
    }

    private ActivityDetailsDto MapToDetailsDto(Activity activity) => new()
    {
        Id = activity.Id,
        Name = activity.Name,
        Description = activity.Description,
        CreatedDate = activity.CreatedDate
    };
}
