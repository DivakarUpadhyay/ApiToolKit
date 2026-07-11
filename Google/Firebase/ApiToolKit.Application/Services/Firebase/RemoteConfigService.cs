using ApiToolKit.Application.DTOs.Firebase.RemoteConfig;
using ApiToolKit.Application.Interfaces.Services.Firebase;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Firebase;
using ApiToolKit.Domain.Interfaces.Repositories.Firebase;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Firebase;

public class RemoteConfigService : IRemoteConfigService
{
    private readonly IFirebaseApiClient _apiClient;
    private readonly IRemoteConfigRepository _configRepo;
    private readonly IFirebaseAuthRepository _authRepo;

    public RemoteConfigService(IFirebaseApiClient apiClient, IRemoteConfigRepository configRepo, IFirebaseAuthRepository authRepo)
    {
        _apiClient = apiClient;
        _configRepo = configRepo;
        _authRepo = authRepo;
    }

    private string RemoteConfigUrl(string projectId) =>
        $"https://firebaseremoteconfig.googleapis.com/v1/projects/{projectId}/remoteConfig";

    public async Task<Result<GetRemoteConfigResponse>> GetRemoteConfigAsync(GetRemoteConfigRequest request)
    {
        try
        {
            var response = await _apiClient.GetAsync<GetRemoteConfigResponse>(
                RemoteConfigUrl(request.ProjectId), request.AccessToken);
            if (response == null) return Result<GetRemoteConfigResponse>.Fail("No response from Remote Config API.");

            await _configRepo.SaveTemplateAsync(new ApiToolKit.Domain.Entities.Firebase.RemoteConfigTemplate
            {
                ProjectId = request.ProjectId,
                Parameters = response.Parameters != null ? JsonConvert.SerializeObject(response.Parameters) : null,
                Conditions = response.Conditions != null ? JsonConvert.SerializeObject(response.Conditions) : null,
                ETag = response.ETag,
                Version = response.Version?.VersionNumber,
                UpdateTime = response.Version?.UpdateTime,
                CreatedAt = DateTime.UtcNow
            });

            return Result<GetRemoteConfigResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetRemoteConfigResponse>.Fail($"GetRemoteConfig failed: {ex.Message}");
        }
    }

    public async Task<Result<GetRemoteConfigResponse>> UpdateRemoteConfigAsync(UpdateRemoteConfigRequest request)
    {
        try
        {
            var headers = new Dictionary<string, string>
            {
                ["If-Match"] = request.ValidateOnly ? "*" : request.ETag
            };

            var url = request.ValidateOnly
                ? $"{RemoteConfigUrl(request.ProjectId)}?validateOnly=true"
                : RemoteConfigUrl(request.ProjectId);

            var response = await _apiClient.PatchAsync<GetRemoteConfigResponse>(url, request.Template, request.AccessToken, headers);
            if (response == null) return Result<GetRemoteConfigResponse>.Fail("No response from Remote Config API.");
            return Result<GetRemoteConfigResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetRemoteConfigResponse>.Fail($"UpdateRemoteConfig failed: {ex.Message}");
        }
    }

    public async Task<Result<object>> ListVersionsAsync(ListRemoteConfigVersionsRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["pageSize"] = request.PageSize.ToString()
            };
            if (!string.IsNullOrEmpty(request.PageToken)) queryParams["pageToken"] = request.PageToken;
            if (!string.IsNullOrEmpty(request.StartTime)) queryParams["startTime"] = request.StartTime;
            if (!string.IsNullOrEmpty(request.EndTime)) queryParams["endTime"] = request.EndTime;

            var response = await _apiClient.GetAsync<object>(
                $"{RemoteConfigUrl(request.ProjectId)}:listVersions", request.AccessToken, queryParams);
            if (response == null) return Result<object>.Fail("No response from Remote Config API.");
            return Result<object>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<object>.Fail($"ListVersions failed: {ex.Message}");
        }
    }

    public async Task<Result<GetRemoteConfigResponse>> RollbackAsync(RollbackRemoteConfigRequest request)
    {
        try
        {
            var body = new { versionNumber = request.VersionNumber };
            var response = await _apiClient.PostAsync<GetRemoteConfigResponse>(
                $"{RemoteConfigUrl(request.ProjectId)}:rollback", body, request.AccessToken);
            if (response == null) return Result<GetRemoteConfigResponse>.Fail("No response from Remote Config API.");
            return Result<GetRemoteConfigResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetRemoteConfigResponse>.Fail($"Rollback failed: {ex.Message}");
        }
    }
}
