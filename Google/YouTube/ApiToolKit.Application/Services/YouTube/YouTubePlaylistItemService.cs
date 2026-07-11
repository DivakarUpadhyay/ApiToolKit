using ApiToolKit.Application.DTOs.YouTube.PlaylistItem;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubePlaylistItemService : IYouTubePlaylistItemService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubePlaylistItemService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<PlaylistItemListResponse>> ListPlaylistItemsAsync(ListPlaylistItemsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails",
            ["playlistId"] = request.PlaylistId,
            ["maxResults"] = request.PageSize.ToString()
        };

        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("playlistItems", accessToken, queryParams);
        if (response == null) return Result<PlaylistItemListResponse>.Fail("No response from YouTube API.");

        var items = new List<PlaylistItemResponse>();
        var responseItems = response?.items;
        if (responseItems != null)
        {
            foreach (var item in responseItems)
                items.Add(MapPlaylistItemResponse(item));
        }

        var pageInfo = response?.pageInfo;
        return Result<PlaylistItemListResponse>.Ok(new PlaylistItemListResponse
        {
            Items = items,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        });
    }

    public async Task<Result<PlaylistItemResponse>> AddPlaylistItemAsync(AddPlaylistItemRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                playlistId = request.PlaylistId,
                resourceId = new { kind = "youtube#video", videoId = request.VideoId },
                position = request.Position
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("playlistItems?part=snippet", body, accessToken);
        if (response == null) return Result<PlaylistItemResponse>.Fail("No response from YouTube API.");

        return Result<PlaylistItemResponse>.Ok(MapPlaylistItemResponse(response));
    }

    public async Task<Result<PlaylistItemResponse>> UpdatePlaylistItemAsync(UpdatePlaylistItemRequest request, string accessToken)
    {
        var body = new
        {
            id = request.ItemId,
            snippet = new
            {
                playlistId = request.PlaylistId,
                position = request.Position,
                resourceId = new { kind = "youtube#video" }
            }
        };

        var response = await _apiClient.PutAsync<dynamic>("playlistItems?part=snippet", body, accessToken);
        if (response == null) return Result<PlaylistItemResponse>.Fail("No response from YouTube API.");

        return Result<PlaylistItemResponse>.Ok(MapPlaylistItemResponse(response));
    }

    public async Task<Result<bool>> DeletePlaylistItemAsync(DeletePlaylistItemRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"playlistItems?id={request.ItemId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete playlist item.");
        return Result<bool>.Ok(true);
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    private static PlaylistItemResponse MapPlaylistItemResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var resourceId = snippet?.resourceId;

        return new PlaylistItemResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            PlaylistId = (string?)snippet?.playlistId ?? string.Empty,
            VideoId = (string?)resourceId?.videoId ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            Position = (int)(snippet?.position ?? 0),
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails),
            VideoOwnerChannelTitle = (string?)snippet?.videoOwnerChannelTitle ?? string.Empty,
            VideoOwnerChannelId = (string?)snippet?.videoOwnerChannelId ?? string.Empty
        };
    }
}
