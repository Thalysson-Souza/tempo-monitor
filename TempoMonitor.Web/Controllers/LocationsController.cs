using Microsoft.AspNetCore.Mvc;
using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Domain.Entities;
using TempoMonitor.Web.Contracts.Locations;

namespace TempoMonitor.Web.Controllers;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(
        CreateLocationRequest request,
        CreateLocation createLocation
    )
    {
        try
        {
            var input = new CreateLocationInput
            {
                Name = request.Name,
                City = request.City,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var location = createLocation.Execute(input);

            return Ok(ToResponse(location));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }

    [HttpGet]
    public IActionResult GetAll(GetAllLocations getAllLocations)
    {
        var locations = getAllLocations.Execute();

        var response = locations.Select(ToResponse);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(
        Guid id,
        GetLocationById getLocationById
    )
    {
        var location = getLocationById.Execute(id);

        if (location is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(location));
    }

    #region Helper Methods
    private static LocationResponse ToResponse(Location location)
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
    #endregion
}