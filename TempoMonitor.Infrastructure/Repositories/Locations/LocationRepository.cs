using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Infrastructure.Repositories.Locations;

public class LocationRepository : ILocationRepository
{
    private static readonly List<Location> Locations = [];

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