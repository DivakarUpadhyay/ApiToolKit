using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Application.Services.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Shopify;
using ApiToolKit.Infrastructure.Repositories.Shopify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ApiToolKit.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<IShopifyAuthRepository, ShopifyAuthRepository>();
        services.AddHttpClient<ShopifyApiClient>();
        services.AddScoped<IShopifyApiClient>(sp => sp.GetRequiredService<ShopifyApiClient>());
        services.AddScoped<IShopifyInventoryItemService, ShopifyInventoryItemService>();
        services.AddScoped<IShopifyInventoryLevelService, ShopifyInventoryLevelService>();
        services.AddScoped<IShopifyLocationService, ShopifyLocationService>();
        return services;
    }
}
