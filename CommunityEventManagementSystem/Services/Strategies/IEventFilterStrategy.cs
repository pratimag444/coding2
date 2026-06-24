
using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Services.Strategies;

public interface IEventFilterStrategy
{
    IEnumerable<Event> Filter(
        IEnumerable<Event> events);
}

