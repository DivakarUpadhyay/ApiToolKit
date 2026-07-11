using ApiToolKit.Application.DTOs.YouTube.Caption;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeCaptionService : IYouTubeCaptionService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeCaptionService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<List<CaptionResponse>>> ListCaptionsAsync(ListCaptionsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet",
            ["videoId"] = request.VideoId
        };

        var response = await _apiClient.GetAsync<dynamic>("captions", accessToken, queryParams);
        if (response == null) return Result<List<CaptionResponse>>.Fail("No response from YouTube API.");

        var captions = new List<CaptionResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
                captions.Add(MapCaptionResponse(item, request.VideoId));
        }

        return Result<List<CaptionResponse>>.Ok(captions);
    }

    public async Task<Result<CaptionResponse>> CreateCaptionAsync(CreateCaptionRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                videoId = request.VideoId,
                language = request.Language,
                name = request.Name,
                isDraft = request.IsDraft
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("captions?part=snippet", body, accessToken);
        if (response == null) return Result<CaptionResponse>.Fail("No response from YouTube API.");

        return Result<CaptionResponse>.Ok(MapCaptionResponse(response, request.VideoId));
    }

    public async Task<Result<CaptionResponse>> UpdateCaptionAsync(UpdateCaptionRequest request, string accessToken)
    {
        var body = new
        {
            id = request.CaptionId,
            snippet = new { isDraft = request.IsDraft }
        };

        var response = await _apiClient.PutAsync<dynamic>("captions?part=snippet", body, accessToken);
        if (response == null) return Result<CaptionResponse>.Fail("No response from YouTube API.");

        return Result<CaptionResponse>.Ok(MapCaptionResponse(response, string.Empty));
    }

    public async Task<Result<bool>> DeleteCaptionAsync(DeleteCaptionRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"captions?id={request.CaptionId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete caption.");
        return Result<bool>.Ok(true);
    }

    private static CaptionResponse MapCaptionResponse(dynamic item, string videoId)
    {
        var snippet = item?.snippet;

        return new CaptionResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            VideoId = (string?)snippet?.videoId ?? videoId,
            Name = (string?)snippet?.name ?? string.Empty,
            Language = (string?)snippet?.language ?? string.Empty,
            TrackKind = (string?)snippet?.trackKind ?? string.Empty,
            Status = (string?)snippet?.status ?? string.Empty,
            IsDraft = (bool)(snippet?.isDraft ?? false),
            IsAutoSynced = (bool)(snippet?.isAutoSynced ?? false),
            LastUpdated = snippet?.lastUpdated != null ? (DateTime)snippet.lastUpdated : DateTime.MinValue
        };
    }
}
