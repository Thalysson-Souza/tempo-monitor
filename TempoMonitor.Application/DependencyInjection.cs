using Microsoft.Extensions.DependencyInjection;
using TempoMonitor.Application.UseCases.Locations;

namespace TempoMonitor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateLocation>();
        services.AddScoped<GetAllLocations>();
        services.AddScoped<GetLocationById>();

        return services;
    }
}