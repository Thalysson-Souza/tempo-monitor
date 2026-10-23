using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Abstractions.Locations;

public interface ILocationRepository
{
    Task AddAsync(Location location);
    Task<List<Location>> GetAllAsync();
    Task<Location?> GetByIdAsync(Guid id);
    Task<Location?> UpdateAsync(Location location);
}
