using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
{
    public ParticipantRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Participant?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<IEnumerable<Participant>> GetParticipantsByEventAsync(int eventId)
    {
        if (eventId <= 0)
            throw new ArgumentException("Event ID must be greater than zero.", nameof(eventId));

        return await _dbSet
            .AsNoTracking()
            .Where(p => p.Registrations.Any(r => r.EventId == eventId))
            .ToListAsync();
    }

    public async Task<Participant?> GetParticipantWithRegistrationsAsync(int participantId)
    {
        if (participantId <= 0)
            throw new ArgumentException("Participant ID must be greater than zero.", nameof(participantId));

        return await _dbSet
            .AsNoTracking()
            .Include(p => p.Registrations)
            .FirstOrDefaultAsync(p => p.Id == participantId);
    }
}
