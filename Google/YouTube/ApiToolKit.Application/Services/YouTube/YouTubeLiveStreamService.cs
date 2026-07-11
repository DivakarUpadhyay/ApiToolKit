using ApiToolKit.Application.DTOs.YouTube.LiveStream;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeLiveStreamService : IYouTubeLiveStreamService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeLiveStreamService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<List<LiveStreamResponse>>> ListLiveStreamsAsync(ListLiveStreamsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,cdn,status",
            ["mine"] = "true",
            ["maxResults"] = request.PageSize.ToString()
        };

        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("liveStreams", accessToken, queryParams);
        if (response == null) return Result<List<LiveStreamResponse>>.Fail("No response from YouTube API.");

        var streams = new List<LiveStreamResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
                streams.Add(MapLiveStreamResponse(item));
        }

        return Result<List<LiveStreamResponse>>.Ok(streams);
    }

    public async Task<Result<LiveStreamResponse>> CreateLiveStreamAsync(CreateLiveStreamRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new { title = request.Title },
            cdn = new
            {
                frameRate = "variable",
                ingestionType = request.IngestionType,
                resolution = request.Format
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("liveStreams?part=snippet,cdn,status", body, accessToken);
        if (response == null) return Result<LiveStreamResponse>.Fail("No response from YouTube API.");

        return Result<LiveStreamResponse>.Ok(MapLiveStreamResponse(response));
    }

    public async Task<Result<LiveStreamResponse>> UpdateLiveStreamAsync(UpdateLiveStreamRequest request, string accessToken)
    {
        var body = new
        {
            id = request.StreamId,
            snippet = new
            {
                title = request.Title,
                description = request.Description
            }
        };

        var response = await _apiClient.PutAsync<dynamic>("liveStreams?part=snippet", body, accessToken);
        if (response == null) return Result<LiveStreamResponse>.Fail("No response from YouTube API.");

        return Result<LiveStreamResponse>.Ok(MapLiveStreamResponse(response));
    }

    public async Task<Result<bool>> DeleteLiveStreamAsync(string streamId, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"liveStreams?id={streamId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete live stream.");
        return Result<bool>.Ok(true);
    }

    private static LiveStreamResponse MapLiveStreamResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var cdn = item?.cdn;
        var ingestionInfo = cdn?.ingestionInfo;
        var status = item?.status;
        var healthStatus = item?.status?.healthStatus;

        return new LiveStreamResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            Format = (string?)cdn?.resolution ?? string.Empty,
            IngestionType = (string?)cdn?.ingestionType ?? string.Empty,
            IngestionAddress = (string?)ingestionInfo?.ingestionAddress ?? string.Empty,
            StreamName = (string?)ingestionInfo?.streamName ?? string.Empty,
            BackupIngestionAddress = (string?)ingestionInfo?.backupIngestionAddress ?? string.Empty,
            Status = (string?)status?.streamStatus ?? string.Empty,
            HealthStatus = (string?)healthStatus?.status ?? string.Empty
        };
    }
}
