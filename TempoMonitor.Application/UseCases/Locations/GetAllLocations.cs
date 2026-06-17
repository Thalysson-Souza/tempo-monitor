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

    public List<Location> Execute()
    {
        return _locationRepository.GetAll();
    }
}