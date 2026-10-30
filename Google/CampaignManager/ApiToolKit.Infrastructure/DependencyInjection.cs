using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Application.Services.CampaignManager;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.CampaignManager;
using ApiToolKit.Infrastructure.Repositories.CampaignManager;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<ICampaignManagerAuthRepository, CampaignManagerAuthRepository>();

        // Campaign Manager 360 API HTTP client
        services.AddHttpClient<CampaignManagerApiClient>();
        services.AddScoped<ICampaignManagerApiClient>(sp => sp.GetRequiredService<CampaignManagerApiClient>());

        // Application Services
        services.AddScoped<ICampaignManagerAuthService, CampaignManagerAuthService>();
        services.AddScoped<ICampaignManagerAdvertiserGroupService, CampaignManagerAdvertiserGroupService>();
        services.AddScoped<ICampaignManagerDimensionValueService, CampaignManagerDimensionValueService>();
        services.AddScoped<ICampaignManagerMetroService, CampaignManagerMetroService>();
        services.AddScoped<ICampaignManagerTvCampaignDetailService, CampaignManagerTvCampaignDetailService>();
        services.AddScoped<ICampaignManagerContentCategoryService, CampaignManagerContentCategoryService>();
        services.AddScoped<ICampaignManagerFloodlightConfigurationService, CampaignManagerFloodlightConfigurationService>();
        services.AddScoped<ICampaignManagerAccountPermissionService, CampaignManagerAccountPermissionService>();
        services.AddScoped<ICampaignManagerAdService, CampaignManagerAdService>();
        services.AddScoped<ICampaignManagerMobileAppService, CampaignManagerMobileAppService>();
        services.AddScoped<ICampaignManagerBillingAssignmentService, CampaignManagerBillingAssignmentService>();
        services.AddScoped<ICampaignManagerAccountUserProfileService, CampaignManagerAccountUserProfileService>();
        services.AddScoped<ICampaignManagerConversionService, CampaignManagerConversionService>();
        services.AddScoped<ICampaignManagerConnectionTypeService, CampaignManagerConnectionTypeService>();
        services.AddScoped<ICampaignManagerCountryService, CampaignManagerCountryService>();
        services.AddScoped<ICampaignManagerReportDataService, CampaignManagerReportDataService>();
        services.AddScoped<ICampaignManagerOperatingSystemService, CampaignManagerOperatingSystemService>();
        services.AddScoped<ICampaignManagerLanguageService, CampaignManagerLanguageService>();
        services.AddScoped<ICampaignManagerUserRoleService, CampaignManagerUserRoleService>();
        services.AddScoped<ICampaignManagerCampaignCreativeAssociationService, CampaignManagerCampaignCreativeAssociationService>();
        services.AddScoped<ICampaignManagerPlatformTypeService, CampaignManagerPlatformTypeService>();
        services.AddScoped<ICampaignManagerChangeLogService, CampaignManagerChangeLogService>();
        services.AddScoped<ICampaignManagerAdvertiserInvoiceService, CampaignManagerAdvertiserInvoiceService>();
        services.AddScoped<ICampaignManagerAccountPermissionGroupService, CampaignManagerAccountPermissionGroupService>();
        services.AddScoped<ICampaignManagerFileService, CampaignManagerFileService>();
        services.AddScoped<ICampaignManagerSubaccountService, CampaignManagerSubaccountService>();
        services.AddScoped<ICampaignManagerAdvertiserLandingPageService, CampaignManagerAdvertiserLandingPageService>();
        services.AddScoped<ICampaignManagerTvCampaignSummaryService, CampaignManagerTvCampaignSummaryService>();
        services.AddScoped<ICampaignManagerOperatingSystemVersionService, CampaignManagerOperatingSystemVersionService>();
        services.AddScoped<ICampaignManagerCreativeFieldService, CampaignManagerCreativeFieldService>();
        services.AddScoped<ICampaignManagerRegionService, CampaignManagerRegionService>();
        services.AddScoped<ICampaignManagerRemarketingListShareService, CampaignManagerRemarketingListShareService>();
        services.AddScoped<ICampaignManagerStudioCreativeService, CampaignManagerStudioCreativeService>();
        services.AddScoped<ICampaignManagerUserRolePermissionGroupService, CampaignManagerUserRolePermissionGroupService>();
        services.AddScoped<ICampaignManagerMobileCarrierService, CampaignManagerMobileCarrierService>();
        services.AddScoped<ICampaignManagerAccountActiveAdSummaryService, CampaignManagerAccountActiveAdSummaryService>();
        services.AddScoped<ICampaignManagerCreativeGroupService, CampaignManagerCreativeGroupService>();
        services.AddScoped<ICampaignManagerPlacementStrategyService, CampaignManagerPlacementStrategyService>();
        services.AddScoped<ICampaignManagerDirectorySiteService, CampaignManagerDirectorySiteService>();
        services.AddScoped<ICampaignManagerPostalCodeService, CampaignManagerPostalCodeService>();
        services.AddScoped<ICampaignManagerAccountService, CampaignManagerAccountService>();
        services.AddScoped<ICampaignManagerCampaignService, CampaignManagerCampaignService>();
        services.AddScoped<ICampaignManagerBrowserService, CampaignManagerBrowserService>();
        services.AddScoped<ICampaignManagerAdvertiserService, CampaignManagerAdvertiserService>();
        services.AddScoped<ICampaignManagerCityService, CampaignManagerCityService>();
        services.AddScoped<ICampaignManagerUserRolePermissionService, CampaignManagerUserRolePermissionService>();
        services.AddScoped<ICampaignManagerPlacementGroupService, CampaignManagerPlacementGroupService>();
        services.AddScoped<ICampaignManagerDynamicFeedService, CampaignManagerDynamicFeedService>();
        services.AddScoped<ICampaignManagerBillingProfileService, CampaignManagerBillingProfileService>();
        services.AddScoped<ICampaignManagerFloodlightActivityService, CampaignManagerFloodlightActivityService>();
        services.AddScoped<ICampaignManagerUserProfileService, CampaignManagerUserProfileService>();
        services.AddScoped<ICampaignManagerTargetingTemplateService, CampaignManagerTargetingTemplateService>();
        services.AddScoped<ICampaignManagerVideoFormatService, CampaignManagerVideoFormatService>();
        services.AddScoped<ICampaignManagerPlacementService, CampaignManagerPlacementService>();
        services.AddScoped<ICampaignManagerCreativeService, CampaignManagerCreativeService>();
        services.AddScoped<ICampaignManagerSizeService, CampaignManagerSizeService>();
        services.AddScoped<ICampaignManagerBillingRateService, CampaignManagerBillingRateService>();
        services.AddScoped<ICampaignManagerRemarketingListService, CampaignManagerRemarketingListService>();
        services.AddScoped<ICampaignManagerTargetableRemarketingListService, CampaignManagerTargetableRemarketingListService>();
        services.AddScoped<ICampaignManagerDynamicTargetingKeyService, CampaignManagerDynamicTargetingKeyService>();
        services.AddScoped<ICampaignManagerReportService, CampaignManagerReportService>();
        services.AddScoped<ICampaignManagerReportCompatibleFieldService, CampaignManagerReportCompatibleFieldService>();
        services.AddScoped<ICampaignManagerReportFileService, CampaignManagerReportFileService>();
        services.AddScoped<ICampaignManagerDynamicProfileService, CampaignManagerDynamicProfileService>();
        services.AddScoped<ICampaignManagerFloodlightActivityGroupService, CampaignManagerFloodlightActivityGroupService>();
        services.AddScoped<ICampaignManagerEventTagService, CampaignManagerEventTagService>();
        services.AddScoped<ICampaignManagerSiteService, CampaignManagerSiteService>();
        services.AddScoped<ICampaignManagerCreativeFieldValueService, CampaignManagerCreativeFieldValueService>();

        return services;
    }
}
