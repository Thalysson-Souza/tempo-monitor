using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Abstractions.Locations;

public interface ILocationRepository
{
    void Add(Location location);
    List<Location> GetAll();
    Location? GetById(Guid id);
}