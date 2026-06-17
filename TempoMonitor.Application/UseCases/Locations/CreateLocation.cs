using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class CreateLocation
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocation(
        ILocationRepository locationRepository
    )
    {
        _locationRepository = locationRepository;
    }

    public Location Execute(CreateLocationInput input)
    {

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

        var location = new Location
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            City = input.City,
            Latitude = input.Latitude,
            Longitude = input.Longitude,
            IsActive = true
        };

        _locationRepository.Add(location);
        return location;
    }
}
