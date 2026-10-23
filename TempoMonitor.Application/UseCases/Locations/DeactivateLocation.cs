using TempoMonitor.Application.Abstractions.Locations;

namespace TempoMonitor.Application.UseCases.Locations;

public class DeactivateLocation
{
    private readonly ILocationRepository _locationRepository;

    public DeactivateLocation(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<bool> ExecuteAsync(Guid locationId)
    {
        var location = await _locationRepository.GetByIdAsync(locationId);

        if (location is null) return false;

        location.IsActive = false;
        await _locationRepository.UpdateAsync(location);
        return true;
    }
}