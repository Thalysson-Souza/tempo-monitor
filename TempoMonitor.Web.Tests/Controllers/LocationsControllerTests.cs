using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TempoMonitor.Web.Contracts.Locations;

namespace TempoMonitor.Web.Tests.Controllers;

public class LocationsControllerTests
{
    [Fact]
    public async Task PostLocations_ShouldReturnOk_WhenRequestIsValid()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var request = CreateValidRequest();

        var response = await client.PostAsJsonAsync("/locations", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var location = await response.Content.ReadFromJsonAsync<LocationResponse>();

        Assert.NotNull(location);
        Assert.NotEqual(Guid.Empty, location.Id);
        Assert.Equal("Casa", location.Name);
        Assert.Equal("Sao Paulo", location.City);
        Assert.Equal(-23.5505, location.Latitude);
        Assert.Equal(-46.6333, location.Longitude);
        Assert.True(location.IsActive);
    }

    [Fact]
    public async Task PostLocations_ShouldReturnBadRequest_WhenNameIsEmpty()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var request = CreateValidRequest();
        request.Name = "";

        var response = await client.PostAsJsonAsync("/locations", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetLocations_ShouldReturnOkAndIncludeCreatedLocation_WhenLocationExists()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var createdLocation = await CreateLocationAsync(client);

        var response = await client.GetAsync("/locations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var locations = await response.Content.ReadFromJsonAsync<List<LocationResponse>>();

        Assert.NotNull(locations);
        Assert.Contains(locations, location => location.Id == createdLocation.Id);
    }

    [Fact]
    public async Task GetLocationById_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var id = Guid.NewGuid();

        var response = await client.GetAsync($"/locations/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetLocationById_ShouldReturnOk_WhenIdExists()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var createdLocation = await CreateLocationAsync(client);

        var response = await client.GetAsync($"/locations/{createdLocation.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var location = await response.Content.ReadFromJsonAsync<LocationResponse>();

        Assert.NotNull(location);
        Assert.Equal(createdLocation.Id, location.Id);
        Assert.Equal("Casa", location.Name);
        Assert.Equal("Sao Paulo", location.City);
        Assert.Equal(-23.5505, location.Latitude);
        Assert.Equal(-46.6333, location.Longitude);
        Assert.True(location.IsActive);
    }

    [Fact]
    public async Task SoftDeleteLocation_ShouldReturnNoContent_WhenIdExists()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var createdLocation = await CreateLocationAsync(client);

        var response = await client.DeleteAsync($"/locations/{createdLocation.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var getResponse = await client.GetAsync($"/locations/{createdLocation.Id}");
        var getLocation = await getResponse.Content.ReadFromJsonAsync<LocationResponse>();
        Assert.NotNull(getLocation);
        Assert.False(getLocation.IsActive);
    }

    [Fact]
    public async Task SoftDeleteLocation_ShouldReturnNotFound_WhenIdNotExists()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var id = Guid.NewGuid();

        var response = await client.DeleteAsync($"/locations/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateLocations_ShouldReturnOk_WhenRequestIsValid()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var createdLocation = await CreateLocationAsync(client);

        var request = new UpdateLocationRequest
        {
            Name = "Casa Atualizada",
            City = "Rio de Janeiro",
            Latitude = -22.9068,
            Longitude = -43.1729
        };

        var response = await client.PutAsJsonAsync($"/locations/{createdLocation.Id}", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var location = await response.Content.ReadFromJsonAsync<LocationResponse>();

        Assert.NotNull(location);
        Assert.NotEqual(Guid.Empty, location.Id);
        Assert.Equal("Casa Atualizada", location.Name);
        Assert.Equal("Rio de Janeiro", location.City);
        Assert.Equal(-22.9068, location.Latitude);
        Assert.Equal(-43.1729, location.Longitude);
        Assert.True(location.IsActive);
    }

    [Fact]
    public async Task UpdateLocation_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var id = Guid.NewGuid();

        var request = CreateValidUpdateRequest();

        var response = await client.PutAsJsonAsync($"/locations/{id}", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static CreateLocationRequest CreateValidRequest()
    {
        return new CreateLocationRequest
        {
            Name = "Casa",
            City = "Sao Paulo",
            Latitude = -23.5505,
            Longitude = -46.6333
        };
    }

    private static UpdateLocationRequest CreateValidUpdateRequest()
    {
        return new UpdateLocationRequest
        {
            Name = "Casa Atualizada",
            City = "Rio de Janeiro",
            Latitude = -22.9068,
            Longitude = -43.1729
        };
    }

    private static async Task<LocationResponse> CreateLocationAsync(HttpClient client)
    {
        var response = await client.PostAsJsonAsync("/locations", CreateValidRequest());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var location = await response.Content.ReadFromJsonAsync<LocationResponse>();

        Assert.NotNull(location);

        return location;
    }
}
