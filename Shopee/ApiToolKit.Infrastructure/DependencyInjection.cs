using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Application.Services.Shopee;
using ApiToolKit.Domain.Interfaces.Repositories.Shopee;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Shopee;
using ApiToolKit.Infrastructure.Repositories.Shopee;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IShopeeAuthRepository, ShopeeAuthRepository>();
        services.AddScoped<IShopeeShopRepository, ShopeeShopRepository>();
        services.AddScoped<IShopeeProductRepository, ShopeeProductRepository>();
        services.AddScoped<IShopeeOrderRepository, ShopeeOrderRepository>();
        services.AddScoped<IShopeeLogisticsRepository, ShopeeLogisticsRepository>();
        services.AddScoped<IShopeeVoucherRepository, ShopeeVoucherRepository>();
        services.AddScoped<IShopeeDiscountRepository, ShopeeDiscountRepository>();

        // Shopee HTTP client
        services.AddHttpClient<ShopeeApiClient>();
        services.AddScoped<IShopeeApiClient>(sp => sp.GetRequiredService<ShopeeApiClient>());

        // Application Services — Core
        services.AddScoped<IShopeeAuthService, ShopeeAuthService>();
        services.AddScoped<IShopeeShopService, ShopeeShopService>();
        services.AddScoped<IShopeeProductService, ShopeeProductService>();
        services.AddScoped<IShopeeOrderService, ShopeeOrderService>();
        services.AddScoped<IShopeeLogisticsService, ShopeeLogisticsService>();
        services.AddScoped<IShopeeVoucherService, ShopeeVoucherService>();
        services.AddScoped<IShopeeDiscountService, ShopeeDiscountService>();
        services.AddScoped<IShopeeMediaService, ShopeeMediaService>();
        services.AddScoped<IShopeeMerchantService, ShopeeMerchantService>();

        // Application Services — Extended
        services.AddScoped<IShopeeReturnService, ShopeeReturnService>();
        services.AddScoped<IShopeePaymentService, ShopeePaymentService>();
        services.AddScoped<IShopeeBundleDealService, ShopeeBundleDealService>();
        services.AddScoped<IShopeeAddOnDealService, ShopeeAddOnDealService>();
        services.AddScoped<IShopeeShopFlashSaleService, ShopeeShopFlashSaleService>();
        services.AddScoped<IShopeeTopPicksService, ShopeeTopPicksService>();
        services.AddScoped<IShopeeShopCategoryService, ShopeeShopCategoryService>();
        services.AddScoped<IShopeePublicService, ShopeePublicService>();
        services.AddScoped<IShopeeFirstMileService, ShopeeFirstMileService>();
        services.AddScoped<IShopeePushNotificationService, ShopeePushNotificationService>();
        services.AddScoped<IShopeeAccountHealthService, ShopeeAccountHealthService>();
        services.AddScoped<IShopeeSbsService, ShopeeSbsService>();
        services.AddScoped<IShopeeFbsService, ShopeeFbsService>();
        services.AddScoped<IShopeeGlobalProductService, ShopeeGlobalProductService>();
        services.AddScoped<IShopeeAdsService, ShopeeAdsService>();
        services.AddScoped<IShopeeFollowPrizeService, ShopeeFollowPrizeService>();
        services.AddScoped<IShopeeVideoService, ShopeeVideoService>();
        services.AddScoped<IShopeeLivestreamService, ShopeeLivestreamService>();
        services.AddScoped<IShopeeAmsService, ShopeeAmsService>();

        return services;
    }
}
