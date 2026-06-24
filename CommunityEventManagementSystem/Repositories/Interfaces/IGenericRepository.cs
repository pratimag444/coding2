using CommunityEventManagementSystem.Domain.Entities;

namespace CommunityEventManagementSystem.Repositories.Interfaces;

/// <summary>
/// Generic repository interface providing CRUD operations for any entity type.
/// Demonstrates abstraction and generic programming principles.
/// Implements DRY (Don't Repeat Yourself) pattern for data access.
/// </summary>
/// <typeparam name="T">The entity type (must inherit from BaseEntity)</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>Gets an entity by its ID asynchronously.</summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>Gets all entities asynchronously.</summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>Adds a new entity to the database asynchronously.</summary>
    Task<T> AddAsync(T entity);

    /// <summary>Updates an existing entity asynchronously.</summary>
    Task<T> UpdateAsync(T entity);

    /// <summary>Deletes an entity by its ID asynchronously.</summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>Checks if an entity exists asynchronously.</summary>
    Task<bool> ExistsAsync(int id);

    /// <summary>Returns queryable for advanced LINQ queries.</summary>
    IQueryable<T> AsQueryable();
}
