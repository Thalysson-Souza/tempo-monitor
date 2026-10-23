using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class GetLocationById
{
    private readonly ILocationRepository _locationRepository;

    public GetLocationById(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location?> ExecuteAsync(Guid id)
    {
        return await _locationRepository.GetByIdAsync(id);
    }
}