using ApiToolKit.Application.DTOs.Facebook.Pixel;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;
namespace ApiToolKit.Application.Services.Facebook;
public class FacebookPixelService : IFacebookPixelService {
    private readonly IFacebookApiClient _apiClient;
    private readonly IFacebookPixelRepository _repo;
    public FacebookPixelService(IFacebookApiClient apiClient, IFacebookPixelRepository repo) { _apiClient = apiClient; _repo = repo; }
    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null) {
        await _repo.SaveApiLogAsync(new FacebookApiLog { ApiName = apiName, RequestBody = request ?? string.Empty, ResponseBody = response ?? string.Empty, IsSuccess = success, StatusCode = statusCode, CreatedAt = DateTime.UtcNow });
    }

    private static string NormalizeActId(string id) => id.StartsWith("act_", StringComparison.OrdinalIgnoreCase) ? id[4..] : id;

    public async Task<Result<MetaPixel>> CreatePixelAsync(string adAccountId, CreatePixelRequest request, string accessToken) {
        try {
            var body = new { name = request.Name };
            var response = await _apiClient.PostAsync<dynamic>($"/act_{NormalizeActId(adAccountId)}/adspixels", body, accessToken);
            if (response == null) return Result<MetaPixel>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<MetaPixel>(JsonConvert.SerializeObject(response));
            await LogAsync("CreatePixel", true, 200, adAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<MetaPixel>.Ok(result) : Result<MetaPixel>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("CreatePixel", false, 500, adAccountId, ex.Message);
            return Result<MetaPixel>.Fail(ex.Message);
        }
    }

    public async Task<Result<PixelsResponse>> GetPixelsAsync(string adAccountId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/act_{NormalizeActId(adAccountId)}/adspixels?fields=id,name,creation_time,last_fired_time,is_unavailable", accessToken: accessToken);
            if (response == null) return Result<PixelsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<PixelsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetPixels", true, 200, adAccountId, JsonConvert.SerializeObject(response));
            return result != null ? Result<PixelsResponse>.Ok(result) : Result<PixelsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetPixels", false, 500, adAccountId, ex.Message);
            return Result<PixelsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<MetaPixel>> GetPixelAsync(string pixelId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{pixelId}?fields=id,name,creation_time,last_fired_time,is_unavailable", accessToken: accessToken);
            if (response == null) return Result<MetaPixel>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<MetaPixel>(JsonConvert.SerializeObject(response));
            await LogAsync("GetPixel", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<MetaPixel>.Ok(result) : Result<MetaPixel>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetPixel", false, 500, pixelId, ex.Message);
            return Result<MetaPixel>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdatePixelAsync(string pixelId, UpdatePixelRequest request, string accessToken) {
        try {
            var body = new { name = request.Name };
            var response = await _apiClient.PostAsync<dynamic>($"/{pixelId}", body, accessToken);
            await LogAsync("UpdatePixel", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("No response from API.");
        } catch (Exception ex) {
            await LogAsync("UpdatePixel", false, 500, pixelId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<PixelStatsResponse>> GetPixelStatsAsync(string pixelId, string accessToken, string? startTime = null, string? endTime = null) {
        try {
            var queryParams = new Dictionary<string, string>();
            if (startTime != null) queryParams["start_time"] = startTime;
            if (endTime != null) queryParams["end_time"] = endTime;
            var response = await _apiClient.GetAsync<dynamic>($"/{pixelId}/stats?fields=start_time,end_time,event_count,event", queryParams.Count > 0 ? queryParams : null, accessToken);
            if (response == null) return Result<PixelStatsResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<PixelStatsResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetPixelStats", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<PixelStatsResponse>.Ok(result) : Result<PixelStatsResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetPixelStats", false, 500, pixelId, ex.Message);
            return Result<PixelStatsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<PixelAudiencesResponse>> GetPixelAudiencesAsync(string pixelId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{pixelId}/audiences?fields=id,name,approximate_count_lower_bound,description", accessToken: accessToken);
            if (response == null) return Result<PixelAudiencesResponse>.Fail("No response from API.");
            var result = JsonConvert.DeserializeObject<PixelAudiencesResponse>(JsonConvert.SerializeObject(response));
            await LogAsync("GetPixelAudiences", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return result != null ? Result<PixelAudiencesResponse>.Ok(result) : Result<PixelAudiencesResponse>.Fail("Failed to parse response.");
        } catch (Exception ex) {
            await LogAsync("GetPixelAudiences", false, 500, pixelId, ex.Message);
            return Result<PixelAudiencesResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> SharePixelAsync(string pixelId, SharePixelRequest request, string accessToken) {
        try {
            var body = new { account_id = request.TargetAdAccountId };
            var response = await _apiClient.PostAsync<dynamic>($"/{pixelId}/shared_accounts", body, accessToken);
            await LogAsync("SharePixel", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return response != null ? Result<bool>.Ok(true) : Result<bool>.Fail("No response from API.");
        } catch (Exception ex) {
            await LogAsync("SharePixel", false, 500, pixelId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<dynamic>> GetPixelDiagnosticsAsync(string pixelId, string accessToken) {
        try {
            var response = await _apiClient.GetAsync<dynamic>($"/{pixelId}/diagnostics", accessToken: accessToken);
            if (response == null) return Result<dynamic>.Fail("No response from API.");
            await LogAsync("GetPixelDiagnostics", true, 200, pixelId, JsonConvert.SerializeObject(response));
            return Result<dynamic>.Ok(response);
        } catch (Exception ex) {
            await LogAsync("GetPixelDiagnostics", false, 500, pixelId, ex.Message);
            return Result<dynamic>.Fail(ex.Message);
        }
    }
}


