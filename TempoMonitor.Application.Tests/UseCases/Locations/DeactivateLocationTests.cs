using TempoMonitor.Application.Tests.Fakes;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.UseCases.Locations;

public class DeactivateLocationTests
{
    [Fact]
    public void Execute_ShouldReturnTrueAndDeactivateLocation_WhenLocationExists()
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

        var useCase = new DeactivateLocation(repository);

        var result = useCase.Execute(locationId);

        var location = repository.GetById(locationId);

        Assert.True(result);
        Assert.NotNull(location);
        Assert.False(location.IsActive);
    }

    [Fact]
    public void Execute_ShouldReturnFalse_WhenLocationDoesNotExist()
    {
        var repository = new FakeLocationRepository();
        var useCase = new DeactivateLocation(repository);

        var result = useCase.Execute(Guid.NewGuid());

        Assert.False(result);
    }
}