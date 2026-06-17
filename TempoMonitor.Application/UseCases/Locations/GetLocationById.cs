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

    public Location? Execute(Guid id)
    {
        return _locationRepository.GetById(id);
    }
}