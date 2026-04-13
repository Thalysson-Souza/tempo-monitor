using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Web.Contracts.Locations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var createLocation = new CreateLocation();

    var request = new CreateLocationRequest
    {
        Name = "Campinas",
        City = "Campinas",
        Latitude = -22.9056,
        Longitude = -47.0608
    };

    var input = new CreateLocationInput
    {
        Name = request.Name,
        City = request.City,
        Latitude = request.Latitude,
        Longitude = request.Longitude
    };

    var location = createLocation.Execute(input);
    var response = new LocationResponse
    {
        Id = location.Id,
        Name = location.Name,
        City = location.City,
        Latitude = location.Latitude,
        Longitude = location.Longitude,
        IsActive = location.IsActive
    };

    return Results.Ok(response);
});

app.Run();