using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
{
    public RegistrationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Registration?> GetRegistrationAsync(int eventId, int participantId)
    {
        if (eventId <= 0 || participantId <= 0)
            throw new ArgumentException("IDs must be greater than zero.");

        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.ParticipantId == participantId);
    }

    public async Task<IEnumerable<Registration>> GetEventRegistrationsAsync(int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Event ID must be greater than zero.", nameof(eventId));

        return await _dbSet
            .AsNoTracking()
            .Where(r => r.EventId == eventId)
            .OrderByDescending(r => r.RegistrationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Registration>> GetParticipantRegistrationsAsync(int participantId)
    {
        if (participantId <= 0)
            throw new ArgumentException("Participant ID must be greater than zero.", nameof(participantId));

        return await _dbSet
            .AsNoTracking()
            .Where(r => r.ParticipantId == participantId)
            .OrderByDescending(r => r.RegistrationDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByStatusAsync(RegistrationStatus status)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(r => r.Status == status)
            .ToListAsync();
    }

    public async Task<bool> IsParticipantRegisteredAsync(int eventId, int participantId)
    {
        if (eventId <= 0 || participantId <= 0)
            throw new ArgumentException("IDs must be greater than zero.");

        return await _dbSet
            .AsNoTracking()
            .AnyAsync(r => r.EventId == eventId && r.ParticipantId == participantId && r.Status != RegistrationStatus.Cancelled);
    }

    public async Task<int> GetActiveRegistrationCountAsync(int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Event ID must be greater than zero.", nameof(eventId));

        return await _dbSet
            .AsNoTracking()
            .CountAsync(r => r.EventId == eventId && r.Status != RegistrationStatus.Cancelled);
    }
}
