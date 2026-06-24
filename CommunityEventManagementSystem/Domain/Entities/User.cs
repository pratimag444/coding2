using CommunityEventManagementSystem.Domain.Interfaces;

namespace CommunityEventManagementSystem.Domain.Entities;

public abstract class User : BaseEntity, IUser
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private string _phoneNumber = string.Empty;

    protected User()
    {
    }

    protected User(
        string firstName,
        string lastName,
        string email,
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string FirstName
    {
        get => _firstName;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name is required.");

            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name is required.");

            _lastName = value;
        }
    }

    public string Email
    {
        get => _email;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email is required.");

            _email = value;
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number is required.");

            _phoneNumber = value;
        }
    }

    public string GetDisplayName()
    {
        return $"{FirstName} {LastName}";
    }

    public abstract bool CanManageEvents();
}