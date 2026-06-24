using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.Domain.Entities;

public class Activity : BaseEntity
{
    private string _name = string.Empty;

    private readonly List<EventActivity> _eventActivities = new();

    protected Activity()
    {
    }

    public Activity(
        string name,
        ActivityType activityType)
    {
        Name = name;
        ActivityType = activityType;
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Activity name is required.");

            _name = value;
        }
    }

    public ActivityType ActivityType { get; private set; }

    public IReadOnlyCollection<EventActivity> EventActivities =>
        _eventActivities.AsReadOnly();
}