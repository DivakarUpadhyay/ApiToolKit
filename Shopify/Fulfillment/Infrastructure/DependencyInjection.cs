using ApiToolKit.Application.Interfaces.Services.Shopify;
using ApiToolKit.Application.Services.Shopify;
using ApiToolKit.Domain.Interfaces.Repositories.Shopify;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Shopify;
using ApiToolKit.Infrastructure.Repositories.Shopify;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        services.AddScoped<IShopifyAuthRepository, ShopifyAuthRepository>();

        services.AddHttpClient<ShopifyApiClient>();
        services.AddScoped<IShopifyApiClient, ShopifyApiClient>();

        services.AddScoped<IShopifyFulfillmentService, ShopifyFulfillmentService>();
        services.AddScoped<IShopifyFulfillmentOrderService, ShopifyFulfillmentOrderService>();
        services.AddScoped<IShopifyFulfillmentServiceService, ShopifyFulfillmentServiceService>();
        services.AddScoped<IShopifyShippingZoneService, ShopifyShippingZoneService>();

        return services;
    }
}
