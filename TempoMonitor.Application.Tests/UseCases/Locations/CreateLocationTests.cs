using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Application.Tests.Fakes;

namespace TempoMonitor.Application.Tests.UseCases.Locations;

public class CreateLocationTests
{
    [Fact]
    public void Execute_ShouldCreateLocation_WhenInputIsValid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new CreateLocation(repository);

        var input = new CreateLocationInput
        {
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333
        };

        var location = useCase.Execute(input);

        Assert.NotEqual(Guid.Empty, location.Id);
        Assert.Equal("Casa", location.Name);
        Assert.Equal("Sao Paulo", location.City);
        Assert.Equal(-23.5505, location.Latitude);
        Assert.Equal(-46.6333, location.Longitude);
        Assert.True(location.IsActive);
        Assert.Single(repository.Locations);
    }

    [Fact]
    public void Execute_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        var repository = new FakeLocationRepository();
        var useCase = new CreateLocation(repository);

        var input = new CreateLocationInput
        {
            Name = "",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333
        };

        var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(input));

        Assert.Equal("Location name is required.", exception.Message);
        Assert.Empty(repository.Locations);
    }

    [Fact]
    public void Execute_ShouldThrowArgumentException_WhenLatitudeIsInvalid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new CreateLocation(repository);

        var input = new CreateLocationInput
        {
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = 999,
            Longitude = -46.6333
        };

        var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(input));

        Assert.Equal("Latitude must be between -90 and 90.", exception.Message);
        Assert.Empty(repository.Locations);
    }

    [Fact]
    public void Execute_ShouldThrowArgumentException_WhenLongitudeIsInvalid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new CreateLocation(repository);

        var input = new CreateLocationInput
        {
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = 999
        };

        var exception = Assert.Throws<ArgumentException>(() => useCase.Execute(input));

        Assert.Equal("Longitude must be between -180 and 180.", exception.Message);
        Assert.Empty(repository.Locations);
    }


}