using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TempoMonitor.Application.Abstractions.Locations;
using TempoMonitor.Infrastructure.Persistence;
using TempoMonitor.Infrastructure.Repositories.Locations;

namespace TempoMonitor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddDbContext<TempoMonitorDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}