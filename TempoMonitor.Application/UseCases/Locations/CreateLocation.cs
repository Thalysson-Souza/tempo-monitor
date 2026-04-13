using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class CreateLocation
{
    public Location Execute(CreateLocationInput input)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            City = input.City,
            Latitude = input.Latitude,
            Longitude = input.Longitude,
            IsActive = true
        };
    }
}