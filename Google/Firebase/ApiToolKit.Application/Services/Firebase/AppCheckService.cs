using ApiToolKit.Application.DTOs.Firebase.AppCheck;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Domain.Common;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Firebase;

public class AppCheckService : IAppCheckService
{
    private readonly IFirebaseApiClient _apiClient;

    public AppCheckService(IFirebaseApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    private string AppCheckBase(string projectId) =>
        $"https://firebaseappcheck.googleapis.com/v1/projects/{projectId}";

    public async Task<Result<VerifyAppCheckTokenResponse>> VerifyTokenAsync(VerifyAppCheckTokenRequest request)
    {
        try
        {
            var body = new { app_check_token = request.AppCheckToken };
            var response = await _apiClient.PostAsync<VerifyAppCheckTokenResponse>(
                $"{AppCheckBase(request.ProjectId)}:verifyAppCheckToken",
                body, request.AccessToken);
            if (response == null) return Result<VerifyAppCheckTokenResponse>.Fail("No response from App Check API.");
            return Result<VerifyAppCheckTokenResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<VerifyAppCheckTokenResponse>.Fail($"VerifyToken failed: {ex.Message}");
        }
    }

    public async Task<Result<List<AppCheckConfig>>> BatchGetConfigsAsync(BatchGetAppCheckConfigsRequest request)
    {
        try
        {
            var appNamesQuery = string.Join("&", request.AppIds.Select(id =>
                $"names={Uri.EscapeDataString($"projects/{request.ProjectId}/apps/{id}/appCheckConfig")}"));

            var url = $"{AppCheckBase(request.ProjectId)}/apps/-/appCheckConfig:batchGet?{appNamesQuery}";
            var response = await _apiClient.GetAsync<dynamic>(url, request.AccessToken);
            if (response == null) return Result<List<AppCheckConfig>>.Fail("No response from App Check API.");

            var configs = JsonConvert.DeserializeObject<List<AppCheckConfig>>(
                JsonConvert.SerializeObject(response?.configs ?? new List<object>()));
            return Result<List<AppCheckConfig>>.Ok(configs ?? new List<AppCheckConfig>());
        }
        catch (Exception ex)
        {
            return Result<List<AppCheckConfig>>.Fail($"BatchGetConfigs failed: {ex.Message}");
        }
    }

    public async Task<Result<List<AppCheckConfig>>> BatchUpdateConfigsAsync(BatchUpdateAppCheckConfigsRequest request)
    {
        try
        {
            var body = new
            {
                requests = request.Configs.Select(c => new
                {
                    appCheckConfig = new
                    {
                        name = c.Name,
                        enforcementMode = c.EnforcementMode
                    }
                }).ToList()
            };

            var response = await _apiClient.PostAsync<dynamic>(
                $"{AppCheckBase(request.ProjectId)}/apps/-/appCheckConfig:batchUpdate",
                body, request.AccessToken);
            if (response == null) return Result<List<AppCheckConfig>>.Fail("No response from App Check API.");

            var configs = JsonConvert.DeserializeObject<List<AppCheckConfig>>(
                JsonConvert.SerializeObject(response?.appCheckConfigs ?? new List<object>()));
            return Result<List<AppCheckConfig>>.Ok(configs ?? new List<AppCheckConfig>());
        }
        catch (Exception ex)
        {
            return Result<List<AppCheckConfig>>.Fail($"BatchUpdateConfigs failed: {ex.Message}");
        }
    }
}
