using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Application.Services.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Facebook;
using ApiToolKit.Infrastructure.Repositories.Facebook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IFacebookAuthRepository, FacebookAuthRepository>();
        services.AddScoped<IFacebookUserRepository, FacebookUserRepository>();
        services.AddScoped<IFacebookPageRepository, FacebookPageRepository>();
        services.AddScoped<IFacebookPostRepository, FacebookPostRepository>();
        services.AddScoped<IFacebookPhotoRepository, FacebookPhotoRepository>();
        services.AddScoped<IFacebookVideoRepository, FacebookVideoRepository>();
        services.AddScoped<IFacebookCommentRepository, FacebookCommentRepository>();
        services.AddScoped<IFacebookEventRepository, FacebookEventRepository>();
        services.AddScoped<IFacebookAdAccountRepository, FacebookAdAccountRepository>();
        services.AddScoped<IFacebookCampaignRepository, FacebookCampaignRepository>();
        services.AddScoped<IFacebookAdSetRepository, FacebookAdSetRepository>();
        services.AddScoped<IFacebookAdRepository, FacebookAdRepository>();
        services.AddScoped<IFacebookInsightRepository, FacebookInsightRepository>();
        services.AddScoped<IFacebookMessengerRepository, FacebookMessengerRepository>();
        services.AddScoped<IFacebookInstagramRepository, FacebookInstagramRepository>();
        services.AddScoped<IFacebookCatalogRepository, FacebookCatalogRepository>();
        services.AddScoped<IFacebookWebhookRepository, FacebookWebhookRepository>();
        services.AddScoped<IFacebookWhatsAppRepository, FacebookWhatsAppRepository>();

        // New repositories (Modules 21–29)
        services.AddScoped<IFacebookThreadsRepository, FacebookThreadsRepository>();
        services.AddScoped<IFacebookCommerceRepository, FacebookCommerceRepository>();
        services.AddScoped<IFacebookProductFeedRepository, FacebookProductFeedRepository>();
        services.AddScoped<IFacebookPixelRepository, FacebookPixelRepository>();
        services.AddScoped<IFacebookCapiRepository, FacebookCapiRepository>();
        services.AddScoped<IFacebookBusinessManagerRepository, FacebookBusinessManagerRepository>();
        services.AddScoped<IFacebookInstagramShoppingRepository, FacebookInstagramShoppingRepository>();
        services.AddScoped<IFacebookLiveRepository, FacebookLiveRepository>();
        services.AddScoped<IFacebookWhatsAppCatalogRepository, FacebookWhatsAppCatalogRepository>();

        // Facebook HTTP client
        services.AddHttpClient<FacebookApiClient>();
        services.AddScoped<IFacebookApiClient>(sp => sp.GetRequiredService<FacebookApiClient>());

        // WhatsApp HTTP client (Bearer auth, separate from Facebook)
        services.AddHttpClient<WhatsAppApiClient>();
        services.AddScoped<IWhatsAppApiClient>(sp => sp.GetRequiredService<WhatsAppApiClient>());

        // Threads HTTP client (Bearer auth, graph.threads.net base URL)
        services.AddHttpClient<ThreadsApiClient>();
        services.AddScoped<IThreadsApiClient>(sp => sp.GetRequiredService<ThreadsApiClient>());

        // Application Services
        services.AddScoped<IFacebookAuthService, FacebookAuthService>();
        services.AddScoped<IFacebookUserService, FacebookUserService>();
        services.AddScoped<IFacebookPageService, FacebookPageService>();
        services.AddScoped<IFacebookPostService, FacebookPostService>();
        services.AddScoped<IFacebookPhotoService, FacebookPhotoService>();
        services.AddScoped<IFacebookVideoService, FacebookVideoService>();
        services.AddScoped<IFacebookCommentService, FacebookCommentService>();
        services.AddScoped<IFacebookReactionService, FacebookReactionService>();
        services.AddScoped<IFacebookEventService, FacebookEventService>();
        services.AddScoped<IFacebookGroupService, FacebookGroupService>();
        services.AddScoped<IFacebookAdAccountService, FacebookAdAccountService>();
        services.AddScoped<IFacebookCampaignService, FacebookCampaignService>();
        services.AddScoped<IFacebookAdSetService, FacebookAdSetService>();
        services.AddScoped<IFacebookAdService, FacebookAdService>();
        services.AddScoped<IFacebookInsightService, FacebookInsightService>();
        services.AddScoped<IFacebookMessengerService, FacebookMessengerService>();
        services.AddScoped<IFacebookInstagramService, FacebookInstagramService>();
        services.AddScoped<IFacebookCatalogService, FacebookCatalogService>();
        services.AddScoped<IFacebookWebhookService, FacebookWebhookService>();
        services.AddScoped<IFacebookWhatsAppService, FacebookWhatsAppService>();

        // New application services (Modules 21–29)
        services.AddScoped<IFacebookThreadsService, FacebookThreadsService>();
        services.AddScoped<IFacebookCommerceService, FacebookCommerceService>();
        services.AddScoped<IFacebookProductFeedService, FacebookProductFeedService>();
        services.AddScoped<IFacebookPixelService, FacebookPixelService>();
        services.AddScoped<IFacebookCapiService, FacebookCapiService>();
        services.AddScoped<IFacebookBusinessManagerService, FacebookBusinessManagerService>();
        services.AddScoped<IFacebookInstagramShoppingService, FacebookInstagramShoppingService>();
        services.AddScoped<IFacebookLiveService, FacebookLiveService>();
        services.AddScoped<IFacebookWhatsAppCatalogService, FacebookWhatsAppCatalogService>();

        // New application services (Modules 30–31)
        services.AddScoped<IFacebookLeadGenService, FacebookLeadGenService>();
        services.AddScoped<IFacebookWhatsAppFlowsService, FacebookWhatsAppFlowsService>();
        services.AddScoped<IFacebookWhatsAppMessagingService, FacebookWhatsAppMessagingService>();
        services.AddScoped<IFacebookWhatsAppAccountService, FacebookWhatsAppAccountService>();
        services.AddScoped<IFacebookWhatsAppAnalyticsService, FacebookWhatsAppAnalyticsService>();
        services.AddScoped<IFacebookWhatsAppGroupsService, FacebookWhatsAppGroupsService>();
        services.AddScoped<IFacebookWhatsAppCallingService, FacebookWhatsAppCallingService>();
        services.AddScoped<IFacebookWhatsAppPaymentsService, FacebookWhatsAppPaymentsService>();

        return services;
    }
}
