using CommunityEventManagementSystem.Domain.Entities;
using CommunityEventManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Data.SeedData;

public static class DbSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Venues.AnyAsync())
        {
            return;
        }

        var venues = new[]
        {
            new Venue("City Community Hall", "12 High Street, Sunderland", 500),
            new Venue("Riverside Park Pavilion", "45 River Road, Sunderland", 200),
            new Venue("Library Conference Room", "88 Library Lane, Sunderland", 80)
        };

        var activities = new[]
        {
            new Activity("Creative Writing Workshop", ActivityType.Workshop),
            new Activity("Community Health Talk", ActivityType.Talk),
            new Activity("Family Fun Games", ActivityType.Game),
            new Activity("Local Business Networking", ActivityType.Networking),
            new Activity("Charity Fundraiser", ActivityType.CommunityService)
        };

        await context.Venues.AddRangeAsync(venues);
        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();

        var participants = new[]
        {
            new Participant("Alice", "Johnson", "alice.j@email.com", "+447700900001"),
            new Participant("Ben", "Smith", "ben.s@email.com", "+447700900002"),
            new Participant("Chloe", "Williams", "chloe.w@email.com", "+447700900003")
        };

        await context.Participants.AddRangeAsync(participants);
        await context.SaveChangesAsync();

        var communityFair = new Event(
            "Summer Community Fair",
            DateOnly.FromDateTime(DateTime.Today.AddDays(14)),
            new TimeOnly(10, 0),
            "A family-friendly community fair with workshops, talks, and games for all ages.",
            150);

        communityFair.SetVenues(new[] { venues[0].Id, venues[1].Id });
        communityFair.SetActivities(new[]
        {
            activities[0].Id,
            activities[2].Id,
            activities[4].Id
        });

        var healthSeminar = new Event(
            "Health & Wellness Seminar",
            DateOnly.FromDateTime(DateTime.Today.AddDays(21)),
            new TimeOnly(14, 0),
            "Expert talks on nutrition, mental health, and community wellbeing.",
            80);

        healthSeminar.SetVenues(new[] { venues[2].Id });
        healthSeminar.SetActivities(new[]
        {
            activities[1].Id,
            activities[3].Id
        });

        await context.Events.AddRangeAsync(
            communityFair,
            healthSeminar);

        await context.SaveChangesAsync();

        await context.Registrations.AddRangeAsync(
            new Registration(participants[0].Id, communityFair.Id),
            new Registration(participants[1].Id, communityFair.Id));

        await context.SaveChangesAsync();
    }
}
