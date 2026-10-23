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

    public List<Location> GetAll()
    {
        return Locations;
    }

    public Location? GetById(Guid id)
    {
        return Locations.FirstOrDefault(location => location.Id == id);
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

        return existingLocation;
    }
}
