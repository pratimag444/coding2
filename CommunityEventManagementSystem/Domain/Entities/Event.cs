using CommunityEventManagementSystem.Domain.Enums;

namespace CommunityEventManagementSystem.Domain.Entities;

public class Event : BaseEntity
{
    private string _name = string.Empty;
    private string _description = string.Empty;
    private int _maximumParticipants;

    private readonly List<Registration> _registrations = new();
    private readonly List<EventVenue> _eventVenues = new();
    private readonly List<EventActivity> _eventActivities = new();

    protected Event()
    {
    }

    public Event(
     string name,
     DateOnly eventDate,
     TimeOnly eventTime,
     string description,
     int maximumParticipants)
    {
        Name = name;
        EventDate = eventDate;
        EventTime = eventTime;
        Description = description;
        MaximumParticipants = maximumParticipants;
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Event name is required.");

            _name = value;
        }
    }

    public DateOnly EventDate { get; private set; }

    public TimeOnly EventTime { get; private set; }

    public string Description
    {
        get => _description;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description is required.");

            _description = value;
        }
    }

    public int MaximumParticipants
    {
        get => _maximumParticipants;
        private set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Maximum participants must be greater than zero.");

            _maximumParticipants = value;
        }
    }


    public IReadOnlyCollection<Registration> Registrations =>
        _registrations.AsReadOnly();

    public IReadOnlyCollection<EventVenue> EventVenues =>
        _eventVenues.AsReadOnly();

    public IReadOnlyCollection<EventActivity> EventActivities =>
        _eventActivities.AsReadOnly();

    public void UpdateDetails(
    string name,
    DateOnly eventDate,
    TimeOnly eventTime,
    string description,
    int maximumParticipants)
    {
        Name = name;
        EventDate = eventDate;
        EventTime = eventTime;
        Description = description;
        MaximumParticipants = maximumParticipants;

        UpdateModifiedDate();
    }

    public bool IsPastEvent()
    {
        return EventDate < DateOnly.FromDateTime(DateTime.Today);
    }

    public int GetRegistrationCount()
    {
        return _registrations.Count(
            r => r.Status != RegistrationStatus.Cancelled);
    }

    public bool HasReachedCapacity()
    {
        return GetRegistrationCount() >= MaximumParticipants;
    }

    public void SetVenues(IEnumerable<int> venueIds)
    {
        _eventVenues.Clear();

        foreach (var venueId in venueIds.Distinct())
        {
            _eventVenues.Add(new EventVenue
            {
                VenueId = venueId
            });
        }

        UpdateModifiedDate();
    }

    public void SetActivities(IEnumerable<int> activityIds)
    {
        _eventActivities.Clear();

        foreach (var activityId in activityIds.Distinct())
        {
            _eventActivities.Add(new EventActivity
            {
                ActivityId = activityId
            });
        }

        UpdateModifiedDate();
    }

}