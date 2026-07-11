using ApiToolKit.Application.DTOs.YouTube.Subscription;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeSubscriptionService : IYouTubeSubscriptionService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeSubscriptionService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<SubscriptionListResponse>> ListSubscriptionsAsync(ListSubscriptionsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails",
            ["maxResults"] = request.PageSize.ToString(),
            ["order"] = request.Order
        };

        if (request.Mine)
            queryParams["mine"] = "true";
        else if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["channelId"] = request.ChannelId;
        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("subscriptions", accessToken, queryParams);
        if (response == null) return Result<SubscriptionListResponse>.Fail("No response from YouTube API.");

        var subscriptions = new List<SubscriptionResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
            {
                var snippet = item?.snippet;
                var contentDetails = item?.contentDetails;
                subscriptions.Add(new SubscriptionResponse
                {
                    Id = (string?)item?.id ?? string.Empty,
                    ChannelId = (string?)snippet?.resourceId?.channelId ?? string.Empty,
                    Title = (string?)snippet?.title ?? string.Empty,
                    Description = (string?)snippet?.description ?? string.Empty,
                    PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
                    ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails),
                    NewItemCount = (int)(contentDetails?.newItemCount ?? 0),
                    ActivityType = (string?)contentDetails?.activityType ?? string.Empty
                });
            }
        }

        var pageInfo = response?.pageInfo;
        return Result<SubscriptionListResponse>.Ok(new SubscriptionListResponse
        {
            Subscriptions = subscriptions,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        });
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    public async Task<Result<SubscriptionResponse>> SubscribeAsync(SubscribeRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                resourceId = new { kind = "youtube#channel", channelId = request.ChannelId }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("subscriptions?part=snippet", body, accessToken);
        if (response == null) return Result<SubscriptionResponse>.Fail("No response from YouTube API.");

        var snippet = response?.snippet;
        return Result<SubscriptionResponse>.Ok(new SubscriptionResponse
        {
            Id = (string?)response?.id ?? string.Empty,
            ChannelId = (string?)snippet?.resourceId?.channelId ?? request.ChannelId,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails)
        });
    }

    public async Task<Result<bool>> DeleteSubscriptionAsync(DeleteSubscriptionRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"subscriptions?id={request.SubscriptionId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete subscription.");
        return Result<bool>.Ok(true);
    }
}
