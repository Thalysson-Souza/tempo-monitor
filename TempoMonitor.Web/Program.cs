using TempoMonitor.Application.UseCases.Locations;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    var createLocation = new CreateLocation();

    var location = createLocation.Execute(
        "Campinas",
        "Campinas",
        -22.9056,
        -47.0608
    );

    return Results.Ok(location);
});

app.Run();