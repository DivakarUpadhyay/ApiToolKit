using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookVideoService : IFacebookVideoService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookVideoRepository _videoRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookVideoService(IFacebookAuthRepository authRepo, IFacebookVideoRepository videoRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _videoRepo = videoRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<UploadVideoResponse>> UploadVideoAsync(UploadVideoRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UploadVideoResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.FileUrl != null) body["file_url"] = request.FileUrl;
        if (request.Title != null) body["title"] = request.Title;
        if (request.Description != null) body["description"] = request.Description;
        body["published"] = request.Published;
        if (request.ScheduledPublishTime.HasValue)
            body["scheduled_publish_time"] = request.ScheduledPublishTime.Value;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/videos", body, accessToken);
        if (response == null) return Result<UploadVideoResponse>.Fail("No response from Facebook API.");

        string videoId = response?.id?.ToString() ?? string.Empty;

        await _videoRepo.SaveVideoAsync(new FacebookVideo
        {
            VideoId = videoId,
            PageId = request.PageId,
            Title = request.Title,
            Description = request.Description,
            Source = request.FileUrl,
            Status = request.Published ? "published" : "scheduled",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _videoRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "UploadVideo",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<UploadVideoResponse>.Ok(new UploadVideoResponse
        {
            Id = videoId,
            UploadSessionId = response?.upload_session_id?.ToString()
        });
    }

    public async Task<Result<VideoResponse>> GetVideoAsync(string videoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<VideoResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,title,description,source,embed_html,status,length,created_time,updated_time,format",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{videoId}", queryParams);
        if (response == null) return Result<VideoResponse>.Fail("No response from Facebook API.");

        var video = JsonConvert.DeserializeObject<VideoResponse>(JsonConvert.SerializeObject(response)) ?? new VideoResponse();
        return Result<VideoResponse>.Ok(video);
    }

    public async Task<Result<GetVideosResponse>> GetVideosAsync(GetVideosRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetVideosResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/videos", queryParams);
        if (response == null) return Result<GetVideosResponse>.Fail("No response from Facebook API.");

        var videos = JsonConvert.DeserializeObject<List<VideoResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<VideoResponse>();

        return Result<GetVideosResponse>.Ok(new GetVideosResponse { Data = videos });
    }

    public async Task<Result<bool>> UpdateVideoAsync(string videoId, string? title, string? description, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (title != null) body["title"] = title;
        if (description != null) body["description"] = description;

        var response = await _apiClient.PostAsync<dynamic>($"/{videoId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteVideoAsync(string videoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{videoId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _videoRepo.DeleteVideoAsync(videoId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<VideoStatusResponse>> GetVideoStatusAsync(string videoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<VideoStatusResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "status",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{videoId}", queryParams);
        if (response == null) return Result<VideoStatusResponse>.Fail("No response from Facebook API.");

        return Result<VideoStatusResponse>.Ok(new VideoStatusResponse
        {
            VideoStatus = response?.status?.video_status?.ToString() ?? string.Empty,
            ProcessingProgress = response?.status?.processing_progress ?? 0
        });
    }

    public async Task<Result<object>> GetVideoThumbnailsAsync(string videoId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<object>($"/{videoId}/thumbnails", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }
}
