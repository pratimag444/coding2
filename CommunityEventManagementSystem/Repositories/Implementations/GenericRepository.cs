using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

/// <summary>
/// Generic repository implementation providing base CRUD operations.
/// This is a reusable base class for all repository implementations.
/// Demonstrates polymorphism and generic programming.
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }

    /// <summary>Gets an entity by ID with error handling.</summary>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be greater than zero.", nameof(id));

        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>Gets all entities with no tracking for read-only scenarios.</summary>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    /// <summary>Adds entity and saves changes to database.</summary>
    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>Updates entity and saves changes to database.</summary>
    public virtual async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>Deletes entity by ID.</summary>
    public virtual async Task<bool> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be greater than zero.", nameof(id));

        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>Checks entity existence by ID.</summary>
    public virtual async Task<bool> ExistsAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID must be greater than zero.", nameof(id));

        return await _dbSet.AsNoTracking().AnyAsync(e => e.Id == id);
    }

    /// <summary>Returns queryable for advanced filtering.</summary>
    public virtual IQueryable<T> AsQueryable()
    {
        return _dbSet.AsNoTracking();
    }
}
