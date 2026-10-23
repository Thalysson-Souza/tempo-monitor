using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.Fakes;

public class FakeLocationRepository : ILocationRepository
{
    public List<Location> Locations { get; } = [];

    public Task AddAsync(Location location)
    {
        Locations.Add(location);
        return Task.CompletedTask;
    }

    public Task<List<Location>> GetAllAsync()
    {
        return Task.FromResult(Locations);
    }

    public Task<Location?> GetByIdAsync(Guid id)
    {
        var location = Locations.FirstOrDefault(location => location.Id == id);
        return Task.FromResult(location);
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

        return existingLocation;
    }
}
