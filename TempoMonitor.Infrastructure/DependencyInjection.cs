using Microsoft.Extensions.DependencyInjection;
using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Infrastructure.Repositories.Locations;

namespace TempoMonitor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILocationRepository, LocationRepository>();

        return services;
    }
}