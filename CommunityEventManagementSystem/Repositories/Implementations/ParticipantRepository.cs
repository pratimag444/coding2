using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class ParticipantRepository
    : GenericRepository<Participant>,
      IParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepository(
        ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Participant?> GetByEmailAsync(
        string email)
    {
        return await _context.Participants
            .FirstOrDefaultAsync(
                p => p.Email == email);
    }

    public async Task<Participant?> GetParticipantWithRegistrationsAsync(
        int id)
    {
        return await _context.Participants
            .Include(p => p.Registrations)
            .FirstOrDefaultAsync(
                p => p.Id == id);
    }

    public async Task<IEnumerable<Participant>>
        GetMostActiveParticipantsAsync()
    {
        return await _context.Participants
            .Include(p => p.Registrations)
            .OrderByDescending(
                p => p.Registrations.Count)
            .Take(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<Participant>>
        GetAllWithRegistrationsAsync()
    {
        return await _context.Participants
            .Include(p => p.Registrations)
            .OrderBy(p => p.LastName)
            .ToListAsync();
    }
}