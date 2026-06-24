namespace CommunityEventManagementSystem.Domain.Entities;

public class AdminUser : User
{
    protected AdminUser()
    {
    }

    public AdminUser(
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
        : base(firstName, lastName, email, phoneNumber)
    {
    }

    public override bool CanManageEvents()
    {
        return true;
    }
}