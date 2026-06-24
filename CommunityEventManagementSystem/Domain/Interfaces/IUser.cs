
namespace CommunityEventManagementSystem.Domain.Interfaces;

public interface IUser
{
    string GetDisplayName();

    bool CanManageEvents();

    string Email { get; }

    string PhoneNumber { get; }
}

