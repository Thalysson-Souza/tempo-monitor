using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Domain.Entities;
using TempoMonitor.Web.Contracts.Locations;

namespace TempoMonitor.Web.Mappers;

public static class LocationMapper
{

    public static CreateLocationInput ToInput(CreateLocationRequest request)
    {
        return new CreateLocationInput
        {
            Name = request.Name,
            City = request.City,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };
    }

    public static UpdateLocationInput ToInput(Guid id, UpdateLocationRequest request)
    {
        return new UpdateLocationInput
        {
            Id = id,
            Name = request.Name,
            City = request.City,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };
    }

    public static LocationResponse ToResponse(Location location)
    {
        return new LocationResponse
        {
            Id = location.Id,
            Name = location.Name,
            City = location.City,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            IsActive = location.IsActive
        };
    }
}