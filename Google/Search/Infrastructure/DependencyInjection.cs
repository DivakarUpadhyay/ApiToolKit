using ApiToolKit.Application.Interfaces.Services.Search;
using ApiToolKit.Application.Services.Search;
using ApiToolKit.Domain.Interfaces.Repositories.Search;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Search;
using ApiToolKit.Infrastructure.Repositories.Search;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<ISearchConfigRepository, SearchConfigRepository>();
        services.AddScoped<ICustomSearchRepository, CustomSearchRepository>();
        services.AddScoped<IKnowledgeGraphRepository, KnowledgeGraphRepository>();

        // Search HTTP client
        services.AddHttpClient<SearchApiClient>();
        services.AddScoped<ISearchApiClient>(sp => sp.GetRequiredService<SearchApiClient>());

        // Application Services
        services.AddScoped<ICustomSearchService, CustomSearchService>();
        services.AddScoped<IKnowledgeGraphService, KnowledgeGraphService>();

        return services;
    }
}
