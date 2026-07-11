using ApiToolKit.Application.DTOs.YouTube.Playlist;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubePlaylistService : IYouTubePlaylistService
{
    private readonly IYouTubeApiClient _apiClient;
    private readonly IYouTubePlaylistRepository _playlistRepo;

    public YouTubePlaylistService(IYouTubeApiClient apiClient, IYouTubePlaylistRepository playlistRepo)
    {
        _apiClient = apiClient;
        _playlistRepo = playlistRepo;
    }

    public async Task<Result<PlaylistListResponse>> ListPlaylistsAsync(ListPlaylistsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails,status",
            ["maxResults"] = request.PageSize.ToString()
        };

        if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["channelId"] = request.ChannelId;
        else if (request.Mine)
            queryParams["mine"] = "true";
        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("playlists", accessToken, queryParams);
        if (response == null) return Result<PlaylistListResponse>.Fail("No response from YouTube API.");

        var playlists = new List<PlaylistResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
                playlists.Add(MapPlaylistResponse(item));
        }

        var pageInfo = response?.pageInfo;
        return Result<PlaylistListResponse>.Ok(new PlaylistListResponse
        {
            Playlists = playlists,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        });
    }

    public async Task<Result<PlaylistResponse>> GetPlaylistAsync(string playlistId, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails,status",
            ["id"] = playlistId
        };

        var response = await _apiClient.GetAsync<dynamic>("playlists", accessToken, queryParams);
        if (response == null) return Result<PlaylistResponse>.Fail("No response from YouTube API.");

        var items = response?.items;
        if (items == null || items.Count == 0)
            return Result<PlaylistResponse>.Fail("Playlist not found.");

        return Result<PlaylistResponse>.Ok(MapPlaylistResponse(items[0]));
    }

    public async Task<Result<PlaylistResponse>> CreatePlaylistAsync(CreatePlaylistRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                title = request.Title,
                description = request.Description,
                defaultLanguage = request.DefaultLanguage
            },
            status = new { privacyStatus = request.Status }
        };

        var response = await _apiClient.PostAsync<dynamic>("playlists?part=snippet,status", body, accessToken);
        if (response == null) return Result<PlaylistResponse>.Fail("No response from YouTube API.");

        var mapped = MapPlaylistResponse(response);

        await _playlistRepo.SavePlaylistAsync(new YouTubePlaylist
        {
            PlaylistId = mapped.Id,
            Title = mapped.Title,
            Description = mapped.Description,
            Status = mapped.Status,
            ItemCount = mapped.ItemCount,
            PublishedAt = mapped.PublishedAt,
            ThumbnailUrl = mapped.ThumbnailUrl,
            CreatedAt = DateTime.UtcNow
        });

        return Result<PlaylistResponse>.Ok(mapped);
    }

    public async Task<Result<PlaylistResponse>> UpdatePlaylistAsync(UpdatePlaylistRequest request, string accessToken)
    {
        var body = new
        {
            id = request.PlaylistId,
            snippet = new
            {
                title = request.Title,
                description = request.Description,
                defaultLanguage = request.DefaultLanguage
            },
            status = new { privacyStatus = request.Status }
        };

        var response = await _apiClient.PutAsync<dynamic>("playlists?part=snippet,status", body, accessToken);
        if (response == null) return Result<PlaylistResponse>.Fail("No response from YouTube API.");

        var mapped = MapPlaylistResponse(response);
        await _playlistRepo.UpdatePlaylistAsync(new YouTubePlaylist
        {
            PlaylistId = request.PlaylistId,
            Title = request.Title ?? string.Empty,
            Description = request.Description ?? string.Empty,
            Status = request.Status ?? string.Empty
        });

        return Result<PlaylistResponse>.Ok(mapped);
    }

    public async Task<Result<bool>> DeletePlaylistAsync(DeletePlaylistRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"playlists?id={request.PlaylistId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete playlist.");

        await _playlistRepo.DeletePlaylistAsync(request.PlaylistId);
        return Result<bool>.Ok(true);
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    private static PlaylistResponse MapPlaylistResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var contentDetails = item?.contentDetails;
        var status = item?.status;

        return new PlaylistResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            ChannelId = (string?)snippet?.channelId ?? string.Empty,
            ChannelTitle = (string?)snippet?.channelTitle ?? string.Empty,
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            ItemCount = (int)(contentDetails?.itemCount ?? 0),
            Status = (string?)status?.privacyStatus ?? string.Empty,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails)
        };
    }
}
