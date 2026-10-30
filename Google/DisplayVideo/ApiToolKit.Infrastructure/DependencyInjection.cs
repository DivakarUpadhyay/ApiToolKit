using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Application.Services.DisplayVideo;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.DisplayVideo;
using ApiToolKit.Infrastructure.Repositories.DisplayVideo;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IDisplayVideoAuthRepository, DisplayVideoAuthRepository>();

        // Display & Video 360 API HTTP client
        services.AddHttpClient<DisplayVideoApiClient>();
        services.AddScoped<IDisplayVideoApiClient>(sp => sp.GetRequiredService<DisplayVideoApiClient>());

        // Application Services
        services.AddScoped<IDisplayVideoAuthService, DisplayVideoAuthService>();
        services.AddScoped<IDisplayVideoGuaranteedOrderService, DisplayVideoGuaranteedOrderService>();
        services.AddScoped<IDisplayVideoCustomListService, DisplayVideoCustomListService>();
        services.AddScoped<IDisplayVideoTargetingTypeTargetingOptionService, DisplayVideoTargetingTypeTargetingOptionService>();
        services.AddScoped<IDisplayVideoUserService, DisplayVideoUserService>();
        services.AddScoped<IDisplayVideoInventorySourceGroupService, DisplayVideoInventorySourceGroupService>();
        services.AddScoped<IDisplayVideoInventorySourceGroupAssignedInventorySourceService, DisplayVideoInventorySourceGroupAssignedInventorySourceService>();
        services.AddScoped<IDisplayVideoAdvertiserService, DisplayVideoAdvertiserService>();
        services.AddScoped<IDisplayVideoReachForecastService, DisplayVideoReachForecastService>();
        services.AddScoped<IDisplayVideoAdGroupService, DisplayVideoAdGroupService>();
        services.AddScoped<IDisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService, DisplayVideoAdGroupYoutubeAssetTypeYoutubeAssetAssociationService>();
        services.AddScoped<IDisplayVideoAdGroupTargetingTypeAssignedTargetingOptionService, DisplayVideoAdGroupTargetingTypeAssignedTargetingOptionService>();
        services.AddScoped<IDisplayVideoChannelService, DisplayVideoChannelService>();
        services.AddScoped<IDisplayVideoChannelSiteService, DisplayVideoChannelSiteService>();
        services.AddScoped<IDisplayVideoInvoiceService, DisplayVideoInvoiceService>();
        services.AddScoped<IDisplayVideoLocationListService, DisplayVideoLocationListService>();
        services.AddScoped<IDisplayVideoLocationListAssignedLocationService, DisplayVideoLocationListAssignedLocationService>();
        services.AddScoped<IDisplayVideoTargetingTypeAssignedTargetingOptionService, DisplayVideoTargetingTypeAssignedTargetingOptionService>();
        services.AddScoped<IDisplayVideoNegativeKeywordListService, DisplayVideoNegativeKeywordListService>();
        services.AddScoped<IDisplayVideoNegativeKeywordListNegativeKeywordService, DisplayVideoNegativeKeywordListNegativeKeywordService>();
        services.AddScoped<IDisplayVideoCreativeService, DisplayVideoCreativeService>();
        services.AddScoped<IDisplayVideoAdGroupAdService, DisplayVideoAdGroupAdService>();
        services.AddScoped<IDisplayVideoLineItemService, DisplayVideoLineItemService>();
        services.AddScoped<IDisplayVideoLineItemTargetingTypeAssignedTargetingOptionService, DisplayVideoLineItemTargetingTypeAssignedTargetingOptionService>();
        services.AddScoped<IDisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService, DisplayVideoLineItemYoutubeAssetTypeYoutubeAssetAssociationService>();
        services.AddScoped<IDisplayVideoCampaignService, DisplayVideoCampaignService>();
        services.AddScoped<IDisplayVideoAdAssetService, DisplayVideoAdAssetService>();
        services.AddScoped<IDisplayVideoInsertionOrderService, DisplayVideoInsertionOrderService>();
        services.AddScoped<IDisplayVideoCombinedAudienceService, DisplayVideoCombinedAudienceService>();
        services.AddScoped<IDisplayVideoInventorySourceService, DisplayVideoInventorySourceService>();
        services.AddScoped<IDisplayVideoFirstPartyAndPartnerAudienceService, DisplayVideoFirstPartyAndPartnerAudienceService>();
        services.AddScoped<IDisplayVideoFloodlightGroupService, DisplayVideoFloodlightGroupService>();
        services.AddScoped<IDisplayVideoFloodlightGroupFloodlightActivityService, DisplayVideoFloodlightGroupFloodlightActivityService>();
        services.AddScoped<IDisplayVideoCustomBiddingAlgorithmService, DisplayVideoCustomBiddingAlgorithmService>();
        services.AddScoped<IDisplayVideoCustomBiddingAlgorithmScriptService, DisplayVideoCustomBiddingAlgorithmScriptService>();
        services.AddScoped<IDisplayVideoCustomBiddingAlgorithmRuleService, DisplayVideoCustomBiddingAlgorithmRuleService>();
        services.AddScoped<IDisplayVideoPartnerService, DisplayVideoPartnerService>();
        services.AddScoped<IDisplayVideoPartnerTargetingTypeAssignedTargetingOptionService, DisplayVideoPartnerTargetingTypeAssignedTargetingOptionService>();
        services.AddScoped<IDisplayVideoPartnerChannelService, DisplayVideoPartnerChannelService>();
        services.AddScoped<IDisplayVideoPartnerChannelSiteService, DisplayVideoPartnerChannelSiteService>();
        services.AddScoped<IDisplayVideoSdfuploadtaskOperationService, DisplayVideoSdfuploadtaskOperationService>();
        services.AddScoped<IDisplayVideoMediaService, DisplayVideoMediaService>();
        services.AddScoped<IDisplayVideoSdfdownloadtaskService, DisplayVideoSdfdownloadtaskService>();
        services.AddScoped<IDisplayVideoSdfdownloadtaskOperationService, DisplayVideoSdfdownloadtaskOperationService>();
        services.AddScoped<IDisplayVideoGoogleAudienceService, DisplayVideoGoogleAudienceService>();

        return services;
    }
}
