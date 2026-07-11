using ApiToolKit.Application.DTOs.YouTube.LiveBroadcast;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeLiveBroadcastService : IYouTubeLiveBroadcastService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeLiveBroadcastService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<LiveBroadcastListResponse>> ListLiveBroadcastsAsync(ListLiveBroadcastsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails,status",
            ["broadcastStatus"] = request.BroadcastStatus,
            ["maxResults"] = request.PageSize.ToString()
        };

        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("liveBroadcasts", accessToken, queryParams);
        if (response == null) return Result<LiveBroadcastListResponse>.Fail("No response from YouTube API.");

        var broadcasts = new List<LiveBroadcastResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
                broadcasts.Add(MapLiveBroadcastResponse(item));
        }

        var pageInfo = response?.pageInfo;
        return Result<LiveBroadcastListResponse>.Ok(new LiveBroadcastListResponse
        {
            Broadcasts = broadcasts,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        });
    }

    public async Task<Result<LiveBroadcastResponse>> CreateLiveBroadcastAsync(CreateLiveBroadcastRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                title = request.Title,
                description = request.Description,
                scheduledStartTime = request.ScheduledStartTime,
                scheduledEndTime = request.ScheduledEndTime
            },
            status = new { privacyStatus = request.PrivacyStatus },
            contentDetails = new
            {
                enableAutoStart = request.EnableAutoStart,
                enableAutoStop = request.EnableAutoStop
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("liveBroadcasts?part=snippet,status,contentDetails", body, accessToken);
        if (response == null) return Result<LiveBroadcastResponse>.Fail("No response from YouTube API.");

        return Result<LiveBroadcastResponse>.Ok(MapLiveBroadcastResponse(response));
    }

    public async Task<Result<LiveBroadcastResponse>> UpdateLiveBroadcastAsync(UpdateLiveBroadcastRequest request, string accessToken)
    {
        var body = new
        {
            id = request.BroadcastId,
            snippet = new
            {
                title = request.Title,
                description = request.Description
            },
            status = new { privacyStatus = request.Status }
        };

        var response = await _apiClient.PutAsync<dynamic>("liveBroadcasts?part=snippet,status", body, accessToken);
        if (response == null) return Result<LiveBroadcastResponse>.Fail("No response from YouTube API.");

        return Result<LiveBroadcastResponse>.Ok(MapLiveBroadcastResponse(response));
    }

    public async Task<Result<LiveBroadcastResponse>> TransitionBroadcastAsync(TransitionBroadcastRequest request, string accessToken)
    {
        var body = new { };
        var response = await _apiClient.PostAsync<dynamic>(
            $"liveBroadcasts/transition?broadcastStatus={request.BroadcastStatus}&id={request.BroadcastId}&part=status",
            body, accessToken);

        if (response == null) return Result<LiveBroadcastResponse>.Fail("No response from YouTube API.");
        return Result<LiveBroadcastResponse>.Ok(MapLiveBroadcastResponse(response));
    }

    public async Task<Result<LiveBroadcastResponse>> BindBroadcastAsync(BindBroadcastRequest request, string accessToken)
    {
        var body = new { };
        var response = await _apiClient.PostAsync<dynamic>(
            $"liveBroadcasts/bind?id={request.BroadcastId}&streamId={request.StreamId}&part=id,contentDetails",
            body, accessToken);

        if (response == null) return Result<LiveBroadcastResponse>.Fail("No response from YouTube API.");
        return Result<LiveBroadcastResponse>.Ok(MapLiveBroadcastResponse(response));
    }

    public async Task<Result<bool>> DeleteLiveBroadcastAsync(string broadcastId, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"liveBroadcasts?id={broadcastId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete live broadcast.");
        return Result<bool>.Ok(true);
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    private static LiveBroadcastResponse MapLiveBroadcastResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var status = item?.status;
        var contentDetails = item?.contentDetails;

        return new LiveBroadcastResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            ScheduledStartTime = snippet?.scheduledStartTime != null ? (DateTime?)snippet.scheduledStartTime : null,
            ScheduledEndTime = snippet?.scheduledEndTime != null ? (DateTime?)snippet.scheduledEndTime : null,
            ActualStartTime = snippet?.actualStartTime != null ? (DateTime?)snippet.actualStartTime : null,
            ActualEndTime = snippet?.actualEndTime != null ? (DateTime?)snippet.actualEndTime : null,
            Status = (string?)status?.lifeCycleStatus ?? string.Empty,
            StreamId = (string?)contentDetails?.boundStreamId ?? string.Empty,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails),
            ViewerCount = 0L
        };
    }
}
