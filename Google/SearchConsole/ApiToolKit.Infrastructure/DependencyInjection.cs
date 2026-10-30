using ApiToolKit.Application.Interfaces.Services.SearchConsole;
using ApiToolKit.Application.Services.SearchConsole;
using ApiToolKit.Domain.Interfaces.Repositories.SearchConsole;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.SearchConsole;
using ApiToolKit.Infrastructure.Repositories.SearchConsole;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<ISearchConsoleAuthRepository, SearchConsoleAuthRepository>();

        // Google Search Console & Indexing API HTTP client
        services.AddHttpClient<SearchConsoleApiClient>();
        services.AddScoped<ISearchConsoleApiClient>(sp => sp.GetRequiredService<SearchConsoleApiClient>());

        // Application Services
        services.AddScoped<ISearchConsoleAuthService, SearchConsoleAuthService>();
        services.AddScoped<ISearchConsoleSiteService, SearchConsoleSiteService>();
        services.AddScoped<ISearchConsoleSearchAnalyticsService, SearchConsoleSearchAnalyticsService>();
        services.AddScoped<ISearchConsoleSitemapService, SearchConsoleSitemapService>();
        services.AddScoped<ISearchConsoleUrlInspectionService, SearchConsoleUrlInspectionService>();
        services.AddScoped<ISearchConsoleIndexingService, SearchConsoleIndexingService>();

        return services;
    }
}
