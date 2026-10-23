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
    public async Task<IActionResult> Create(
        CreateLocationRequest request,
        CreateLocation createLocation
    )
    {
        try
        {
            var input = LocationMapper.ToInput(request);

            var location = await createLocation.ExecuteAsync(input);

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
    public async Task<IActionResult> GetAll(GetAllLocations getAllLocations)
    {
        var locations = await getAllLocations.ExecuteAsync();

        var response = locations.Select(LocationMapper.ToResponse);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        GetLocationById getLocationById
    )
    {
        var location = await getLocationById.ExecuteAsync(id);

        if (location is null)
        {
            return NotFound();
        }

        return Ok(LocationMapper.ToResponse(location));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        DeactivateLocation deactivateLocation
    )
    {
        var result = await deactivateLocation.ExecuteAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateLocationRequest request,
        UpdateLocation updateLocation
    )
    {
        try
        {
            var input = LocationMapper.ToInput(id, request);

            var location = await updateLocation.ExecuteAsync(input);

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
