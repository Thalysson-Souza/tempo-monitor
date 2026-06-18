using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.Fakes;

public class FakeLocationRepository : ILocationRepository
{
    public List<Location> Locations { get; } = [];

    public void Add(Location location)
    {
        Locations.Add(location);
    }

    public List<Location> GetAll()
    {
        return Locations;
    }

    public Location? GetById(Guid id)
    {
        return Locations.FirstOrDefault(location => location.Id == id);
    }
}