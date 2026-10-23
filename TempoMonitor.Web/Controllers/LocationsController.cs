using Microsoft.AspNetCore.Mvc;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Web.Contracts.Locations;
using TempoMonitor.Web.Mappers;

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
            var input = LocationMapper.ToInput(request);

            var location = createLocation.Execute(input);

            return Ok(LocationMapper.ToResponse(location));
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

        var response = locations.Select(LocationMapper.ToResponse);

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

        return Ok(LocationMapper.ToResponse(location));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(
        Guid id,
        DeactivateLocation deactivateLocation
    )
    {
        var result = deactivateLocation.Execute(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(
        Guid id,
        UpdateLocationRequest request,
        UpdateLocation updateLocation
    )
    {
        try
        {
            var input = LocationMapper.ToInput(id, request);

            var location = updateLocation.Execute(input);

            if (location is null)
            {
                return NotFound();
            }

            return Ok(LocationMapper.ToResponse(location));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }

}