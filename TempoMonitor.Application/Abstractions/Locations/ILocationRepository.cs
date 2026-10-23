using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Abstractions.Locations;

public interface ILocationRepository
{
    Task AddAsync(Location location);
    List<Location> GetAll();
    Location? GetById(Guid id);
    Location? Update(Location location);
}
