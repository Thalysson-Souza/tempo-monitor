using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class GetAllLocations
{
    private readonly ILocationRepository _locationRepository;

    public GetAllLocations(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<List<Location>> ExecuteAsync()
    {
        return await _locationRepository.GetAllAsync();
    }
}