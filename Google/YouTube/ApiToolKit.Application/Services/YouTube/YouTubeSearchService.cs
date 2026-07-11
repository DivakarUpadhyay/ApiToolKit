using ApiToolKit.Application.DTOs.YouTube.Search;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeSearchService : IYouTubeSearchService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeSearchService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    public async Task<Result<SearchResponse>> SearchAsync(SearchRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet",
            ["q"] = request.Q,
            ["type"] = request.Type,
            ["maxResults"] = request.PageSize.ToString(),
            ["order"] = request.Order
        };

        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;
        if (request.PublishedAfter.HasValue)
            queryParams["publishedAfter"] = request.PublishedAfter.Value.ToString("o");
        if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["channelId"] = request.ChannelId;
        if (!string.IsNullOrEmpty(request.VideoDefinition))
            queryParams["videoDefinition"] = request.VideoDefinition;
        if (!string.IsNullOrEmpty(request.VideoDuration))
            queryParams["videoDuration"] = request.VideoDuration;
        if (!string.IsNullOrEmpty(request.VideoType))
            queryParams["videoType"] = request.VideoType;

        var response = await _apiClient.GetAsync<dynamic>("search", accessToken, queryParams);
        if (response == null) return Result<SearchResponse>.Fail("No response from YouTube API.");

        var results = new List<SearchResult>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
            {
                var id = item?.id;
                var snippet = item?.snippet;
                results.Add(new SearchResult
                {
                    Id = new SearchResultId
                    {
                        Kind = (string?)id?.kind ?? string.Empty,
                        VideoId = (string?)id?.videoId ?? string.Empty,
                        ChannelId = (string?)id?.channelId ?? string.Empty,
                        PlaylistId = (string?)id?.playlistId ?? string.Empty
                    },
                    Snippet = new SearchResultSnippet
                    {
                        PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
                        ChannelId = (string?)snippet?.channelId ?? string.Empty,
                        Title = (string?)snippet?.title ?? string.Empty,
                        Description = (string?)snippet?.description ?? string.Empty,
                        ChannelTitle = (string?)snippet?.channelTitle ?? string.Empty,
                        ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails)
                    }
                });
            }
        }

        var pageInfo = response?.pageInfo;
        return Result<SearchResponse>.Ok(new SearchResponse
        {
            Items = results,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0),
            ResultsPerPage = (int)(pageInfo?.resultsPerPage ?? 0)
        });
    }
}
