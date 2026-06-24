
namespace CommunityEventManagementSystem.Domain.Entities;

public class Venue : BaseEntity
{
    private string _name = string.Empty;
    private string _address = string.Empty;
    private int _capacity;

    private readonly List<EventVenue> _eventVenues = new();

    protected Venue()
    {
    }

    public Venue(
        string name,
        string address,
        int capacity)
    {
        Name = name;
        Address = address;
        Capacity = capacity;
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    "Venue name is required.");

            _name = value;
        }
    }

    public string Address
    {
        get => _address;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    "Address is required.");

            _address = value;
        }
    }

    public int Capacity
    {
        get => _capacity;
        private set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Capacity must be greater than zero.");

            _capacity = value;
        }
    }

    public IReadOnlyCollection<EventVenue> EventVenues =>
        _eventVenues.AsReadOnly();

    public bool HasCapacityFor(int attendees)
    {
        return attendees <= Capacity;
    }
}
