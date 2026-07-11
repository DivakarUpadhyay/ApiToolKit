using ApiToolKit.Application.Interfaces.Services.Analytics;
using ApiToolKit.Application.Services.Analytics;
using ApiToolKit.Domain.Interfaces.Repositories.Analytics;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Analytics;
using ApiToolKit.Infrastructure.Repositories.Analytics;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IAnalyticsAuthRepository, AnalyticsAuthRepository>();
        services.AddScoped<IAnalyticsReportRepository, AnalyticsReportRepository>();

        // Analytics HTTP client
        services.AddHttpClient<AnalyticsApiClient>();
        services.AddScoped<IAnalyticsApiClient>(sp => sp.GetRequiredService<AnalyticsApiClient>());

        // Application Services
        services.AddScoped<IAnalyticsAuthService, AnalyticsAuthService>();
        services.AddScoped<IAnalyticsReportService, AnalyticsReportService>();
        services.AddScoped<IAnalyticsRealtimeService, AnalyticsRealtimeService>();
        services.AddScoped<IAnalyticsMetadataService, AnalyticsMetadataService>();
        services.AddScoped<IAnalyticsPropertyService, AnalyticsPropertyService>();

        return services;
    }
}
