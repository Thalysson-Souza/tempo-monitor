using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;
using TempoMonitor.Infrastructure.Persistence;

namespace TempoMonitor.Infrastructure.Repositories.Locations;

public class LocationRepository : ILocationRepository
{
    private readonly TempoMonitorDbContext _context;

    public LocationRepository(TempoMonitorDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
    }

    public List<Location> GetAll()
    {
        return _context.Locations.ToList();
    }

    public Location? GetById(Guid id)
    {
        return _context.Locations.FirstOrDefault(location => location.Id == id);
    }

    public Location? Update(Location location)
    {
        var existingLocation = GetById(location.Id);

        if (existingLocation is null)
        {
            return null;
        }

        existingLocation.Name = location.Name;
        existingLocation.City = location.City;
        existingLocation.Latitude = location.Latitude;
        existingLocation.Longitude = location.Longitude;
        existingLocation.IsActive = location.IsActive;

        _context.SaveChanges();

        return existingLocation;
    }
}
