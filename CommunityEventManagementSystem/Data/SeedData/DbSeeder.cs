using CommunityEventManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Data.SeedData;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Events.AnyAsync())
            return;

        var venues = new List<Venue>
        {
            new("City Convention Center", "123 Main St, Downtown", 500),
            new("Community Park Pavilion", "456 Park Ave, North Side", 200),
            new("University Auditorium", "789 College Rd, Campus", 300),
            new("Riverside Garden", "321 River Ln, Waterfront", 150)
        };

        var activities = new List<Activity>
        {
            new("Networking", "Connect with community members and professionals"),
            new("Workshop", "Educational sessions and skill development"),
            new("Entertainment", "Live performances and cultural activities"),
            new("Sports", "Athletic competitions and outdoor games"),
            new("Charity", "Community service and fundraising activities")
        };

        await context.Venues.AddRangeAsync(venues);
        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();

        var events = new List<Event>
        {
            new("Community Fair 2026", new DateOnly(2026, 7, 15), new TimeOnly(10, 0),
                "Annual community celebration with food, games, and entertainment", 300),
            new("Tech Meetup", new DateOnly(2026, 8, 1), new TimeOnly(18, 30),
                "Professional networking event for tech enthusiasts", 150),
            new("Charity Marathon", new DateOnly(2026, 8, 22), new TimeOnly(6, 0),
                "5K marathon to support local food bank", 500),
            new("Summer Concert Series", new DateOnly(2026, 8, 28), new TimeOnly(19, 0),
                "Live music performances in the park", 200)
        };

        await context.Events.AddRangeAsync(events);
        await context.SaveChangesAsync();

        foreach (var evt in events)
        {
            evt.SetVenues(venues.Take(2).Select(v => v.Id));
            evt.SetActivities(activities.Take(3).Select(a => a.Id));
        }

        await context.SaveChangesAsync();
    }
}
