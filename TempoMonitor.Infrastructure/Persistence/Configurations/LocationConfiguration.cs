using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Infrastructure.Persistence.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> entity)
    {
        entity.ToTable("locations");

        entity.HasKey(location => location.Id);

        entity.Property(location => location.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(location => location.City)
            .HasMaxLength(100);

        entity.Property(location => location.Latitude)
            .IsRequired();

        entity.Property(location => location.Longitude)
            .IsRequired();

        entity.Property(location => location.IsActive)
            .IsRequired();
    }
}