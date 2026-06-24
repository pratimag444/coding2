using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Repositories.Implementations;

public class EventRepository
    : GenericRepository<Event>,
      IEventRepository
{
    public EventRepository(
        ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Event>>
        GetUpcomingEventsAsync()
    {
        return await _context.Events
            .Include(e => e.Registrations)
            .Include(e => e.EventVenues)
            .Include(e => e.EventActivities)
            .Where(e => e.EventDate >= DateOnly.FromDateTime(DateTime.Today))
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>>
        GetAllWithDetailsAsync()
    {
        return await _context.Events
            .Include(e => e.Registrations)
            .Include(e => e.EventVenues)
                .ThenInclude(ev => ev.Venue)
            .Include(e => e.EventActivities)
                .ThenInclude(ea => ea.Activity)
            .OrderBy(e => e.EventDate)
            .ToListAsync();
    }

    public async Task<Event?>
        GetEventWithDetailsAsync(int eventId)
    {
        return await _context.Events
            .Include(e => e.Registrations)
            .Include(e => e.EventActivities)
                .ThenInclude(ea => ea.Activity)
            .Include(e => e.EventVenues)
                .ThenInclude(ev => ev.Venue)
            .FirstOrDefaultAsync(e => e.Id == eventId);
    }
   
public async Task<IEnumerable<Event>>
    GetEventsByDateAsync(DateOnly date)
    {
        return await _context.Events
            .Where(e => e.EventDate == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>>
        SearchEventsAsync(string searchTerm)
    {
        return await _context.Events
            .Where(e =>
                e.Name.Contains(searchTerm) ||
                e.Description.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<Event?>
        GetMostPopularEventAsync()
    {
        return await _context.Events
            .OrderByDescending(e =>
                e.Registrations.Count)
            .FirstOrDefaultAsync();
    }


}