using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Application.Services.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.YouTube;
using ApiToolKit.Infrastructure.Repositories.YouTube;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IYouTubeAuthRepository, YouTubeAuthRepository>();
        services.AddScoped<IYouTubeChannelRepository, YouTubeChannelRepository>();
        services.AddScoped<IYouTubeVideoRepository, YouTubeVideoRepository>();
        services.AddScoped<IYouTubePlaylistRepository, YouTubePlaylistRepository>();

        // YouTube HTTP client
        services.AddHttpClient<YouTubeApiClient>();
        services.AddScoped<IYouTubeApiClient>(sp => sp.GetRequiredService<YouTubeApiClient>());

        // Application Services
        services.AddScoped<IYouTubeAuthService, YouTubeAuthService>();
        services.AddScoped<IYouTubeChannelService, YouTubeChannelService>();
        services.AddScoped<IYouTubeVideoService, YouTubeVideoService>();
        services.AddScoped<IYouTubePlaylistService, YouTubePlaylistService>();
        services.AddScoped<IYouTubePlaylistItemService, YouTubePlaylistItemService>();
        services.AddScoped<IYouTubeCommentService, YouTubeCommentService>();
        services.AddScoped<IYouTubeSearchService, YouTubeSearchService>();
        services.AddScoped<IYouTubeSubscriptionService, YouTubeSubscriptionService>();
        services.AddScoped<IYouTubeLiveBroadcastService, YouTubeLiveBroadcastService>();
        services.AddScoped<IYouTubeLiveStreamService, YouTubeLiveStreamService>();
        services.AddScoped<IYouTubeAnalyticsService, YouTubeAnalyticsService>();
        services.AddScoped<IYouTubeCaptionService, YouTubeCaptionService>();

        return services;
    }
}
