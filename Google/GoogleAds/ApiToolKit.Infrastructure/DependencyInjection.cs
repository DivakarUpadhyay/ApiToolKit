using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using ApiToolKit.Application.Services.GoogleAds;
using ApiToolKit.Domain.Interfaces.Repositories.GoogleAds;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.GoogleAds;
using ApiToolKit.Infrastructure.Repositories.GoogleAds;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IGoogleAdsAuthRepository, GoogleAdsAuthRepository>();
        services.AddScoped<IGoogleAdsCampaignRepository, GoogleAdsCampaignRepository>();
        services.AddScoped<IGoogleAdsAdGroupRepository, GoogleAdsAdGroupRepository>();
        services.AddScoped<IGoogleAdsKeywordRepository, GoogleAdsKeywordRepository>();

        // Google Ads HTTP client
        services.AddHttpClient<GoogleAdsApiClient>();
        services.AddScoped<IGoogleAdsApiClient>(sp => sp.GetRequiredService<GoogleAdsApiClient>());

        // Application Services
        services.AddScoped<IGoogleAdsAuthService, GoogleAdsAuthService>();
        services.AddScoped<IGoogleAdsCampaignService, GoogleAdsCampaignService>();
        services.AddScoped<IGoogleAdsAdGroupService, GoogleAdsAdGroupService>();
        services.AddScoped<IGoogleAdsAdService, GoogleAdsAdService>();
        services.AddScoped<IGoogleAdsKeywordService, GoogleAdsKeywordService>();
        services.AddScoped<IGoogleAdsCustomerService, GoogleAdsCustomerService>();
        services.AddScoped<IGoogleAdsReportService, GoogleAdsReportService>();
        services.AddScoped<IGoogleAdsBudgetService, GoogleAdsBudgetService>();
        services.AddScoped<IGoogleAdsAudienceService, GoogleAdsAudienceService>();
        services.AddScoped<IGoogleAdsConversionService, GoogleAdsConversionService>();
        services.AddScoped<IGoogleAdsBiddingStrategyService, GoogleAdsBiddingStrategyService>();
        services.AddScoped<IGoogleAdsRestService, GoogleAdsRestService>();

        return services;
    }
}
