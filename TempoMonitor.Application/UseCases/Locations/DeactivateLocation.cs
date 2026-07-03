using TempoMonitor.Application.Abstractions.Locations;

namespace TempoMonitor.Application.UseCases.Locations;

public class DeactivateLocation
{
    private readonly ILocationRepository _locationRepository;

    public DeactivateLocation(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public bool Execute(Guid locationId)
    {
        var location = _locationRepository.GetById(locationId);

        if (location is null) return false;

        location.IsActive = false;
        _locationRepository.Update(location);
        return true;
    }
}