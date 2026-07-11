using ApiToolKit.Application.Interfaces.Services.BigQuery;
using ApiToolKit.Application.Services.BigQuery;
using ApiToolKit.Domain.Interfaces.Repositories.BigQuery;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.BigQuery;
using ApiToolKit.Infrastructure.Repositories.BigQuery;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IBigQueryAuthRepository, BigQueryAuthRepository>();
        services.AddScoped<IBigQueryDatasetRepository, BigQueryDatasetRepository>();
        services.AddScoped<IBigQueryTableRepository, BigQueryTableRepository>();
        services.AddScoped<IBigQueryJobRepository, BigQueryJobRepository>();

        // BigQuery HTTP client
        services.AddHttpClient<BigQueryApiClient>();
        services.AddScoped<IBigQueryApiClient>(sp => sp.GetRequiredService<BigQueryApiClient>());

        // Application Services
        services.AddScoped<IBigQueryAuthService, BigQueryAuthService>();
        services.AddScoped<IBigQueryDatasetService, BigQueryDatasetService>();
        services.AddScoped<IBigQueryTableService, BigQueryTableService>();
        services.AddScoped<IBigQueryJobService, BigQueryJobService>();
        services.AddScoped<IBigQueryRoutineService, BigQueryRoutineService>();

        return services;
    }
}
