using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.UseCases.Locations;

public class CreateLocation
{
    public Location Execute(string name, string? city, double latitude, double longitude)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            Name = name,
            City = city,
            Latitude = latitude,
            Longitude = longitude,
            IsActive = true
        };
    }
}