namespace CommunityEventManagementSystem.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }

    public DateTime CreatedDate { get; protected set; }

    public DateTime ModifiedDate { get; protected set; }

    protected BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
        ModifiedDate = DateTime.UtcNow;
    }

    public void UpdateModifiedDate()
    {
        ModifiedDate = DateTime.UtcNow;
    }
}