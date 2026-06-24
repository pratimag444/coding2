using CommunityEventManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityEventManagementSystem.Data.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(v => v.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(v => v.Capacity)
            .IsRequired();

        builder.HasMany(v => v.EventVenues)
            .WithOne(ev => ev.Venue)
            .HasForeignKey(ev => ev.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
