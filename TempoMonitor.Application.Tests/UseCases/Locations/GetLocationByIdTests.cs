using TempoMonitor.Application.Tests.Fakes;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.UseCases.Locations;

public class GetLocationByIdTests
{
    [Fact]
    public void Execute_ShouldReturnLocation_WhenLocationExists()
    {
        var locationId = Guid.NewGuid();

        var repository = new FakeLocationRepository();

        repository.Locations.Add(new Location
        {
            Id = locationId,
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333,
            IsActive = true
        });

        var useCase = new GetLocationById(repository);

        var location = useCase.Execute(locationId);

        Assert.NotNull(location);
        Assert.Equal(locationId, location.Id);
        Assert.Equal("Casa", location.Name);
    }

    [Fact]
    public void Execute_ShouldReturnNull_WhenLocationDoesNotExist()
    {
        var repository = new FakeLocationRepository();
        var useCase = new GetLocationById(repository);

        var location = useCase.Execute(Guid.NewGuid());

        Assert.Null(location);
    }


}