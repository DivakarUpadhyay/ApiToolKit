using ApiToolKit.Application.DTOs.Facebook.Live;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookLiveService : IFacebookLiveService
{
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookLiveRepository _repo;

    public FacebookLiveService(IFacebookApiClient apiClient, IFacebookLiveRepository repo)
    {
        _apiClient = apiClient;
        _repo = repo;
    }

    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null)
    {
        await _repo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = apiName,
            RequestBody = request ?? string.Empty,
            ResponseBody = response ?? string.Empty,
            IsSuccess = success,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<LiveVideo>> CreateLiveVideoAsync(string pageId, CreateLiveVideoRequest request, string accessToken)
    {
        try
        {
            var body = new { title = request.Title, description = request.Description, status = request.Status, save_vod = request.SaveVod };
            var response = await _apiClient.PostAsync<dynamic>($"/{pageId}/live_videos", body, accessToken: accessToken);
            if (response == null) return Result<LiveVideo>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<LiveVideo>(JsonConvert.SerializeObject(response));
            await LogAsync("CreateLiveVideo", true, 200, pageId, JsonConvert.SerializeObject(response));
            return result != null ? Result<LiveVideo>.Ok(result) : Result<LiveVideo>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("CreateLiveVideo", false, 500, pageId, ex.Message);
            return Result<LiveVideo>.Fail(ex.Message);
        }
    }

    public async Task<Result<LiveVideosResponse>> GetLiveVideosAsync(string pageId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/live_videos?fields=id,title,description,status,stream_url,dash_ingest_url,embed_html,creation_time,permalink_url,live_views", accessToken: accessToken);
            if (response == null) return Result<LiveVideosResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<LiveVideosResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetLiveVideos", true, 200, pageId, JsonConvert.SerializeObject(response));
            return result != null ? Result<LiveVideosResponse>.Ok(result) : Result<LiveVideosResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetLiveVideos", false, 500, pageId, ex.Message);
            return Result<LiveVideosResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<LiveVideo>> GetLiveVideoAsync(string liveVideoId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{liveVideoId}?fields=id,title,description,status,stream_url,dash_ingest_url,embed_html,creation_time,permalink_url,live_views", accessToken: accessToken);
            if (response == null) return Result<LiveVideo>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<LiveVideo>(JsonConvert.SerializeObject(response));
            await LogAsync("GetLiveVideo", true, 200, liveVideoId, JsonConvert.SerializeObject(response));
            return result != null ? Result<LiveVideo>.Ok(result) : Result<LiveVideo>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetLiveVideo", false, 500, liveVideoId, ex.Message);
            return Result<LiveVideo>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateLiveVideoAsync(string liveVideoId, UpdateLiveVideoRequest request, string accessToken)
    {
        try
        {
            var body = new { title = request.Title, description = request.Description, status = request.Status };
            var response = await _apiClient.PostAsync<dynamic>($"/{liveVideoId}", body, accessToken: accessToken);
            var success = response != null;
            await LogAsync("UpdateLiveVideo", success, success ? 200 : 400, liveVideoId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to update live video.");
        }
        catch (Exception ex)
        {
            await LogAsync("UpdateLiveVideo", false, 500, liveVideoId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> EndLiveVideoAsync(string liveVideoId, string accessToken)
    {
        try
        {
            var body = new { end_live_video = true };
            var response = await _apiClient.PostAsync<dynamic>($"/{liveVideoId}", body, accessToken: accessToken);
            var success = response != null;
            await LogAsync("EndLiveVideo", success, success ? 200 : 400, liveVideoId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to end live video.");
        }
        catch (Exception ex)
        {
            await LogAsync("EndLiveVideo", false, 500, liveVideoId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteLiveVideoAsync(string liveVideoId, string accessToken)
    {
        try
        {
            var response = await _apiClient.DeleteAsync<dynamic>($"/{liveVideoId}", accessToken: accessToken);
            var success = response != null;
            await LogAsync("DeleteLiveVideo", success, success ? 200 : 400, liveVideoId, JsonConvert.SerializeObject(response));
            return success ? Result<bool>.Ok(true) : Result<bool>.Fail("Failed to delete live video.");
        }
        catch (Exception ex)
        {
            await LogAsync("DeleteLiveVideo", false, 500, liveVideoId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<LiveVideoErrorsResponse>> GetLiveVideoErrorsAsync(string liveVideoId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{liveVideoId}/errors?fields=id,error_type,error_description,error_category,event_type,creation_time", accessToken: accessToken);
            if (response == null) return Result<LiveVideoErrorsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<LiveVideoErrorsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetLiveVideoErrors", true, 200, liveVideoId, JsonConvert.SerializeObject(response));
            return result != null ? Result<LiveVideoErrorsResponse>.Ok(result) : Result<LiveVideoErrorsResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetLiveVideoErrors", false, 500, liveVideoId, ex.Message);
            return Result<LiveVideoErrorsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<LiveVideoBlockedUsersResponse>> GetBlockedUsersAsync(string liveVideoId, string accessToken)
    {
        try
        {
            var response = await _apiClient.GetAsync<dynamic>($"/{liveVideoId}/blocked_users?fields=id,name,pic_url", accessToken: accessToken);
            if (response == null) return Result<LiveVideoBlockedUsersResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<LiveVideoBlockedUsersResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetBlockedUsers", true, 200, liveVideoId, JsonConvert.SerializeObject(response));
            return result != null ? Result<LiveVideoBlockedUsersResponse>.Ok(result) : Result<LiveVideoBlockedUsersResponse>.Fail("Failed to parse.");
        }
        catch (Exception ex)
        {
            await LogAsync("GetBlockedUsers", false, 500, liveVideoId, ex.Message);
            return Result<LiveVideoBlockedUsersResponse>.Fail(ex.Message);
        }
    }
}
