using ApiToolKit.Application.DTOs.YouTube.Channel;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeChannelService : IYouTubeChannelService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeChannelService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<ListMyChannelsResponse>> GetMyChannelsAsync(string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,statistics,brandingSettings",
            ["mine"] = "true"
        };

        var response = await _apiClient.GetAsync<dynamic>("channels", accessToken, queryParams);
        if (response == null) return Result<ListMyChannelsResponse>.Fail("No response from YouTube API.");

        var channels = new List<ChannelResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
            {
                channels.Add(MapChannelResponse(item));
            }
        }

        var pageInfo = response?.pageInfo;
        var result = new ListMyChannelsResponse
        {
            Channels = channels,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        };

        return Result<ListMyChannelsResponse>.Ok(result);
    }

    public async Task<Result<ChannelResponse>> GetChannelAsync(GetChannelRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,statistics,brandingSettings"
        };

        if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["id"] = request.ChannelId;
        else if (!string.IsNullOrEmpty(request.ForHandle))
            queryParams["forHandle"] = request.ForHandle;
        else
            return Result<ChannelResponse>.Fail("Either ChannelId or ForHandle must be provided.");

        var response = await _apiClient.GetAsync<dynamic>("channels", accessToken, queryParams);
        if (response == null) return Result<ChannelResponse>.Fail("No response from YouTube API.");

        var items = response?.items;
        if (items == null || items.Count == 0)
            return Result<ChannelResponse>.Fail("Channel not found.");

        return Result<ChannelResponse>.Ok(MapChannelResponse(items[0]));
    }

    public async Task<Result<ChannelResponse>> UpdateChannelAsync(UpdateChannelRequest request, string accessToken)
    {
        var body = new
        {
            id = request.ChannelId,
            brandingSettings = new
            {
                channel = new
                {
                    description = request.Description,
                    defaultLanguage = request.DefaultLanguage,
                    country = request.Country,
                    keywords = request.Keywords
                }
            }
        };

        var response = await _apiClient.PutAsync<dynamic>($"channels?part=brandingSettings", body, accessToken);
        if (response == null) return Result<ChannelResponse>.Fail("No response from YouTube API.");

        return Result<ChannelResponse>.Ok(MapChannelResponse(response));
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try
        {
            var defaultThumb = JObject.FromObject(thumbnails)["default"];
            return defaultThumb?["url"]?.ToString() ?? string.Empty;
        }
        catch { return string.Empty; }
    }

    private static ChannelResponse MapChannelResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var statistics = item?.statistics;
        var branding = item?.brandingSettings;

        return new ChannelResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            CustomUrl = (string?)snippet?.customUrl ?? string.Empty,
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            Country = (string?)snippet?.country ?? string.Empty,
            ViewCount = statistics?.viewCount != null ? (long)statistics.viewCount : 0L,
            SubscriberCount = statistics?.subscriberCount != null ? (long)statistics.subscriberCount : 0L,
            VideoCount = statistics?.videoCount != null ? (long)statistics.videoCount : 0L,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails),
            BannerImageUrl = (string?)branding?.image?.bannerExternalUrl ?? string.Empty
        };
    }
}
