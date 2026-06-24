using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.DTOs;
using CommunityEventManagementSystem.Repositories.Interfaces;
using CommunityEventManagementSystem.Services.Interfaces;

namespace CommunityEventManagementSystem.Services.Implementations;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(
        IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<IEnumerable<ActivityDetailsDto>>
        GetAllActivitiesAsync()
    {
        var activities =
            await _activityRepository.GetAllAsync();

        return activities
            .OrderBy(a => a.Name)
            .Select(MapToDto);
    }

    public async Task CreateActivityAsync(
        CreateActivityDto dto)
    {
        var activity =
            new Activity(
                dto.Name,
                dto.ActivityType);

        await _activityRepository.AddAsync(activity);
        await _activityRepository.SaveChangesAsync();
    }

    public async Task DeleteActivityAsync(int id)
    {
        var activity =
            await _activityRepository.GetByIdAsync(id);

        if (activity is null)
        {
            throw new ArgumentException(
                "Activity not found.");
        }

        _activityRepository.Delete(activity);
        await _activityRepository.SaveChangesAsync();
    }

    private static ActivityDetailsDto MapToDto(Activity activity)
    {
        return new ActivityDetailsDto
        {
            Id = activity.Id,
            Name = activity.Name,
            ActivityType = activity.ActivityType
        };
    }
}
