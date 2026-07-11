using ApiToolKit.Application.DTOs.Facebook.Insight;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookInsightService
{
    Task<Result<GetPageInsightsResponse>> GetPageInsightsAsync(GetPageInsightsRequest request, string accessToken);
    Task<Result<GetPageInsightsResponse>> GetVideoInsightsAsync(VideoInsightRequest request, string accessToken);
    Task<Result<GetAdInsightsResponse>> GetAdInsightsAsync(GetAdInsightsRequest request, string accessToken);
    Task<Result<GetAdInsightsResponse>> GetCampaignInsightsAsync(string campaignId, string accessToken, string? datePreset, int limit);
    Task<Result<GetAdInsightsResponse>> GetAdSetInsightsAsync(string adSetId, string accessToken, string? datePreset, int limit);
    Task<Result<GetAdInsightsResponse>> GetAccountInsightsAsync(string accountId, string accessToken, string? datePreset, int limit);
}
