using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Application.Services.Flipkart;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Flipkart;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
        services.AddHttpClient<FlipkartApiClient>();
        services.AddScoped<IFlipkartApiClient>(sp => sp.GetRequiredService<FlipkartApiClient>());
        services.AddScoped<IFlipkartListingService, FlipkartListingService>();
        services.AddScoped<IFlipkartHyperLocalService, FlipkartHyperLocalService>();
        services.AddScoped<IFlipkartOrderService, FlipkartOrderService>();
        services.AddScoped<IFlipkartReturnsService, FlipkartReturnsService>();
        services.AddScoped<IFlipkartReportService, FlipkartReportService>();
        return services;
    }
}
