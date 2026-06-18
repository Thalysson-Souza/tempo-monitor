using TempoMonitor.Application.Tests.Fakes;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.UseCases.Locations;

public class GetAllLocationsTests
{
    [Fact]
    public void Execute_ShouldReturnAllLocations()
    {
        var repository = new FakeLocationRepository();

        repository.Locations.Add(new Location
        {
            Id = Guid.NewGuid(),
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333,
            IsActive = true
        });

        repository.Locations.Add(new Location
        {
            Id = Guid.NewGuid(),
            Name = "Trabalho",
            City = "Sao Paulo",
            Latitude = -23.56,
            Longitude = -46.65,
            IsActive = true
        });

        var useCase = new GetAllLocations(repository);

        var locations = useCase.Execute();

        Assert.Equal(2, locations.Count);
    }


}