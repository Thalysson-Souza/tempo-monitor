using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class UpdateLocation
{
    private readonly ILocationRepository _locationRepository;

    public UpdateLocation(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public Location? Execute(UpdateLocationInput input)
    {
        var location = _locationRepository.GetById(input.Id);

        if (location is null) return null;

        if (string.IsNullOrWhiteSpace(input.Name))
        {
            throw new ArgumentException("Location name is required.");
        }

        if (input.Latitude < -90 || input.Latitude > 90)
        {
            throw new ArgumentException("Latitude must be between -90 and 90.");
        }

        if (input.Longitude < -180 || input.Longitude > 180)
        {
            throw new ArgumentException("Longitude must be between -180 and 180.");
        }
        location.Name = input.Name;
        location.City = input.City;
        location.Latitude = input.Latitude;
        location.Longitude = input.Longitude;
        _locationRepository.Update(location);
        return location;
    }
}