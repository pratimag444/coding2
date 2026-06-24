using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class RegistrationRepository
    : GenericRepository<Registration>,
      IRegistrationRepository
{
    public RegistrationRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<bool>
        RegistrationExistsAsync(
            int participantId,
            int eventId)
    {
        return await _context.Registrations
            .AnyAsync(r =>
                r.ParticipantId == participantId &&
                r.EventId == eventId &&
                r.Status != Domain.Enums.RegistrationStatus.Cancelled);
    }

    public async Task<int>
        GetRegistrationCountAsync(
            int eventId)
    {
        return await _context.Registrations
            .CountAsync(r =>
                r.EventId == eventId &&
                r.Status != Domain.Enums.RegistrationStatus.Cancelled);
    }

    public async Task<IEnumerable<Registration>>
        GetAllRegistrationsWithDetailsAsync()
    {
        return await _context.Registrations
            .Include(r => r.Participant)
            .Include(r => r.Event)
            .ToListAsync();
    }

 
public async Task<IEnumerable<Registration>>
    GetRegistrationsForEventAsync(int eventId)
    {
        return await _context.Registrations
            .Where(r => r.EventId == eventId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Registration>>
        GetRegistrationsForParticipantAsync(
            int participantId)
    {
        return await _context.Registrations
            .Where(r => r.ParticipantId == participantId)
            .ToListAsync();
    }
 
}