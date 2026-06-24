using CommunityEventManagementSystem.Data;
using CommunityEventManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityEventManagementSystem.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(e => e.EventDate)
            .IsRequired();

        builder.Property(e => e.EventTime)
            .IsRequired();

        builder.Property(e => e.MaximumParticipants)
            .IsRequired();

        builder.HasMany(e => e.Registrations)
            .WithOne(r => r.Event)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.EventVenues)
            .WithOne(ev => ev.Event)
            .HasForeignKey(ev => ev.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.EventActivities)
            .WithOne(ea => ea.Event)
            .HasForeignKey(ea => ea.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
