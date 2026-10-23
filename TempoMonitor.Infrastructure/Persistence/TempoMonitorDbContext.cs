using Microsoft.EntityFrameworkCore;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Infrastructure.Persistence;

public class TempoMonitorDbContext : DbContext
{
    public TempoMonitorDbContext(DbContextOptions<TempoMonitorDbContext> options) : base(options) { }

    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TempoMonitorDbContext).Assembly);
    }
}