using CommunityEventManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();

    public DbSet<Participant> Participants => Set<Participant>();

    public DbSet<Venue> Venues => Set<Venue>();

    public DbSet<Activity> Activities => Set<Activity>();

    public DbSet<Registration> Registrations => Set<Registration>();

    public DbSet<EventVenue> EventVenues => Set<EventVenue>();

    public DbSet<EventActivity> EventActivities => Set<EventActivity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
    typeof(ApplicationDbContext).Assembly); 

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventVenue>()
            .HasKey(ev => new { ev.EventId, ev.VenueId });

        modelBuilder.Entity<EventActivity>()
            .HasKey(ea => new { ea.EventId, ea.ActivityId });
    }
}