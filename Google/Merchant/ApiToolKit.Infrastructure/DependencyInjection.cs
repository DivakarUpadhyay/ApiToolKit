using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Application.Services.Merchant;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Merchant;
using ApiToolKit.Infrastructure.Repositories.Merchant;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IMerchantAuthRepository, MerchantAuthRepository>();

        // Google Merchant API HTTP client
        services.AddHttpClient<MerchantApiClient>();
        services.AddScoped<IMerchantApiClient>(sp => sp.GetRequiredService<MerchantApiClient>());

        // Application Services
        services.AddScoped<IMerchantAuthService, MerchantAuthService>();
        services.AddScoped<IMerchantTermsOfServiceService, MerchantTermsOfServiceService>();
        services.AddScoped<IMerchantAccountService, MerchantAccountService>();
        services.AddScoped<IMerchantHomepageService, MerchantHomepageService>();
        services.AddScoped<IMerchantBusinessInfoService, MerchantBusinessInfoService>();
        services.AddScoped<IMerchantAutomaticImprovementService, MerchantAutomaticImprovementService>();
        services.AddScoped<IMerchantRelationshipService, MerchantRelationshipService>();
        services.AddScoped<IMerchantOnlineReturnPolicyService, MerchantOnlineReturnPolicyService>();
        services.AddScoped<IMerchantDeveloperRegistrationService, MerchantDeveloperRegistrationService>();
        services.AddScoped<IMerchantOmnichannelSettingService, MerchantOmnichannelSettingService>();
        services.AddScoped<IMerchantOmnichannelSettingLfpProviderService, MerchantOmnichannelSettingLfpProviderService>();
        services.AddScoped<IMerchantBusinessIdentityService, MerchantBusinessIdentityService>();
        services.AddScoped<IMerchantUserService, MerchantUserService>();
        services.AddScoped<IMerchantUserMeService, MerchantUserMeService>();
        services.AddScoped<IMerchantProgramService, MerchantProgramService>();
        services.AddScoped<IMerchantProgramCheckoutSettingService, MerchantProgramCheckoutSettingService>();
        services.AddScoped<IMerchantServiceService, MerchantServiceService>();
        services.AddScoped<IMerchantEmailPreferenceService, MerchantEmailPreferenceService>();
        services.AddScoped<IMerchantGbpAccountService, MerchantGbpAccountService>();
        services.AddScoped<IMerchantShippingSettingService, MerchantShippingSettingService>();
        services.AddScoped<IMerchantRegionService, MerchantRegionService>();
        services.AddScoped<IMerchantTermsOfServiceAgreementStateService, MerchantTermsOfServiceAgreementStateService>();
        services.AddScoped<IMerchantAutofeedSettingService, MerchantAutofeedSettingService>();
        services.AddScoped<IMerchantIssueService, MerchantIssueService>();
        services.AddScoped<IMerchantConversionsConversionSourceService, MerchantConversionsConversionSourceService>();
        services.AddScoped<IMerchantDatasourcesDataSourceService, MerchantDatasourcesDataSourceService>();
        services.AddScoped<IMerchantDatasourcesDataSourceFileUploadService, MerchantDatasourcesDataSourceFileUploadService>();
        services.AddScoped<IMerchantInventoriesProductRegionalInventoryService, MerchantInventoriesProductRegionalInventoryService>();
        services.AddScoped<IMerchantInventoriesProductLocalInventoryService, MerchantInventoriesProductLocalInventoryService>();
        services.AddScoped<IMerchantIssueresolutionAggregateProductStatuseService, MerchantIssueresolutionAggregateProductStatuseService>();
        services.AddScoped<IMerchantIssueresolutionIssueresolutionService, MerchantIssueresolutionIssueresolutionService>();
        services.AddScoped<IMerchantLfpLfpMerchantStateService, MerchantLfpLfpMerchantStateService>();
        services.AddScoped<IMerchantLfpLfpSaleService, MerchantLfpLfpSaleService>();
        services.AddScoped<IMerchantLfpLfpInventoryService, MerchantLfpLfpInventoryService>();
        services.AddScoped<IMerchantLfpLfpStoreService, MerchantLfpLfpStoreService>();
        services.AddScoped<IMerchantNotificationsNotificationsubscriptionService, MerchantNotificationsNotificationsubscriptionService>();
        services.AddScoped<IMerchantOrdertrackingOrderTrackingSignalService, MerchantOrdertrackingOrderTrackingSignalService>();
        services.AddScoped<IMerchantProductsProductInputService, MerchantProductsProductInputService>();
        services.AddScoped<IMerchantProductsProductService, MerchantProductsProductService>();
        services.AddScoped<IMerchantPromotionsPromotionService, MerchantPromotionsPromotionService>();
        services.AddScoped<IMerchantQuotaQuotaService, MerchantQuotaQuotaService>();
        services.AddScoped<IMerchantQuotaLimitService, MerchantQuotaLimitService>();
        services.AddScoped<IMerchantReportsReportService, MerchantReportsReportService>();
        services.AddScoped<IMerchantReviewsProductReviewService, MerchantReviewsProductReviewService>();
        services.AddScoped<IMerchantReviewsMerchantReviewAlphaService, MerchantReviewsMerchantReviewAlphaService>();
        services.AddScoped<IMerchantReviewsProductReviewAlphaService, MerchantReviewsProductReviewAlphaService>();
        services.AddScoped<IMerchantProductstudioTextSuggestionAlphaService, MerchantProductstudioTextSuggestionAlphaService>();
        services.AddScoped<IMerchantProductstudioGeneratedImageAlphaService, MerchantProductstudioGeneratedImageAlphaService>();

        return services;
    }
}
