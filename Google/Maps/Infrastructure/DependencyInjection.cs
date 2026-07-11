using ApiToolKit.Application.Interfaces.Services.Maps;
using ApiToolKit.Application.Services.Maps;
using ApiToolKit.Domain.Interfaces.Repositories.Maps;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Maps;
using ApiToolKit.Infrastructure.Repositories.Maps;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IMapsConfigRepository, MapsConfigRepository>();
        services.AddScoped<IMapsGeocodingRepository, MapsGeocodingRepository>();
        services.AddScoped<IMapsPlaceRepository, MapsPlaceRepository>();

        // Maps HTTP client
        services.AddHttpClient<MapsApiClient>();
        services.AddScoped<IMapsApiClient>(sp => sp.GetRequiredService<MapsApiClient>());

        // Application Services
        services.AddScoped<IMapsGeocodingService, MapsGeocodingService>();
        services.AddScoped<IMapsDirectionsService, MapsDirectionsService>();
        services.AddScoped<IMapsDistanceMatrixService, MapsDistanceMatrixService>();
        services.AddScoped<IMapsPlacesService, MapsPlacesService>();
        services.AddScoped<IMapsElevationService, MapsElevationService>();
        services.AddScoped<IMapsTimeZoneService, MapsTimeZoneService>();
        services.AddScoped<IMapsRoadsService, MapsRoadsService>();
        services.AddScoped<IMapsStaticMapService, MapsStaticMapService>();

        return services;
    }
}
