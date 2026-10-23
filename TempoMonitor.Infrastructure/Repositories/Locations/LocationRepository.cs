using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(Guid id)
    {
        return await _context.Locations.FirstOrDefaultAsync(location => location.Id == id);
    }

    public async Task<Location?> UpdateAsync(Location location)
    {
        var existingLocation = await GetByIdAsync(location.Id);

        if (existingLocation is null)
        {
            return null;
        }

        existingLocation.Name = location.Name;
        existingLocation.City = location.City;
        existingLocation.Latitude = location.Latitude;
        existingLocation.Longitude = location.Longitude;
        existingLocation.IsActive = location.IsActive;

        await _context.SaveChangesAsync();

        return existingLocation;
    }
}
