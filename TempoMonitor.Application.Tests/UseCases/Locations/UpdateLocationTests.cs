using TempoMonitor.Application.DTOs.Locations;
using TempoMonitor.Application.Tests.Fakes;
using TempoMonitor.Application.UseCases.Locations;
using TempoMonitor.Domain.Entities;

namespace TempoMonitor.Application.Tests.UseCases.Locations;

public class UpdateLocationTests
{
    [Fact]
    public async Task Execute_ShouldUpdateLocation_WhenInputIsValid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new UpdateLocation(repository);

        var locationId = Guid.NewGuid();

        repository.Locations.Add(new Location
        {
            Id = locationId,
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333,
            IsActive = true
        });

        var input = new UpdateLocationInput
        {
            Id = locationId,
            Name = "Casa Atualizada",
            City = "Rio de Janeiro",
            Latitude = -22.9068,
            Longitude = -43.1729
        };

        var updatedLocation = await useCase.ExecuteAsync(input);

        Assert.NotNull(updatedLocation);
        Assert.Equal(locationId, updatedLocation.Id);
        Assert.Equal("Casa Atualizada", updatedLocation.Name);
        Assert.Equal("Rio de Janeiro", updatedLocation.City);
        Assert.Equal(-22.9068, updatedLocation.Latitude);
        Assert.Equal(-43.1729, updatedLocation.Longitude);
    }

    [Fact]
    public async Task Execute_ShouldThrowArgumentException_WhenLatitudeIsInvalid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new UpdateLocation(repository);

        var locationId = Guid.NewGuid();

        repository.Locations.Add(new Location
        {
            Id = locationId,
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333,
            IsActive = true
        });

        var input = new UpdateLocationInput
        {
            Id = locationId,
            Name = "Casa Atualizada",
            City = "Rio de Janeiro",
            Latitude = 999,
            Longitude = -43.1729
        };

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(input));

        Assert.Equal("Latitude must be between -90 and 90.", exception.Message);
        Assert.Single(repository.Locations);

        var existingLocation = await repository.GetByIdAsync(locationId);

        Assert.NotNull(existingLocation);
        Assert.Equal(-23.5505, existingLocation.Latitude);
        Assert.Equal(-46.6333, existingLocation.Longitude);
    }

    [Fact]
    public async Task Execute_ShouldThrowArgumentException_WhenLongitudeIsInvalid()
    {
        var repository = new FakeLocationRepository();
        var useCase = new UpdateLocation(repository);

        var locationId = Guid.NewGuid();

        repository.Locations.Add(new Location
        {
            Id = locationId,
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333,
            IsActive = true
        });

        var input = new UpdateLocationInput
        {
            Id = locationId,
            Name = "Casa Atualizada",
            City = "Rio de Janeiro",
            Latitude = -23.5505,
            Longitude = 999
        };

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(input));

        Assert.Equal("Longitude must be between -180 and 180.", exception.Message);
        Assert.Single(repository.Locations);

        var existingLocation = await repository.GetByIdAsync(locationId);

        Assert.NotNull(existingLocation);
        Assert.Equal(-23.5505, existingLocation.Latitude);
        Assert.Equal(-46.6333, existingLocation.Longitude);
    }


}