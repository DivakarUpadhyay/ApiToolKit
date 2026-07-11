using ApiToolKit.Application.DTOs.Workspace.Meet;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Workspace.Meet;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Workspace.Meet;

public class MeetSpaceService : IMeetSpaceService
{
    private readonly IWorkspaceAuthService _authService;
    private readonly IMeetRepository _meetRepo;
    private readonly IWorkspaceApiClient _apiClient;
    private const string BaseUrl = "https://meet.googleapis.com/v2/spaces";

    public MeetSpaceService(IWorkspaceAuthService authService, IMeetRepository meetRepo, IWorkspaceApiClient apiClient)
    {
        _authService = authService;
        _meetRepo = meetRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<SpaceResponse>> CreateSpaceAsync(string userId, CreateSpaceRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpaceResponse>.Fail("Access token not found.");

        var body = new { config = new { accessType = request.Config_AccessType, entryPointAccess = request.InitialConfig_EntryPointAccess } };
        var response = await _apiClient.PostAsync<SpaceResponse>(BaseUrl, body, accessToken);
        if (response == null) return Result<SpaceResponse>.Fail("Failed to create Meet space.");

        await _meetRepo.SaveSpaceAsync(new MeetSpace
        {
            SpaceName = response.Name ?? string.Empty,
            UserId = userId,
            MeetingCode = response.MeetingCode,
            MeetingUri = response.MeetingUri,
            Config = JsonConvert.SerializeObject(response.Config),
            ActiveConference = JsonConvert.SerializeObject(response.ActiveConference),
            CreatedAt = DateTime.UtcNow
        });

        return Result<SpaceResponse>.Ok(response);
    }

    public async Task<Result<SpaceResponse>> GetSpaceAsync(string userId, string spaceName)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<SpaceResponse>.Fail("Access token not found.");

        var response = await _apiClient.GetAsync<SpaceResponse>($"{BaseUrl}/{spaceName}", accessToken);
        if (response == null) return Result<SpaceResponse>.Fail("Meet space not found.");

        return Result<SpaceResponse>.Ok(response);
    }

    public async Task<Result<bool>> EndSpaceAsync(string userId, EndSpaceRequest request)
    {
        var accessToken = await _authService.GetAccessTokenAsync(userId);
        if (accessToken == null) return Result<bool>.Fail("Access token not found.");

        await _apiClient.PostAsync<object>($"{BaseUrl}/{request.SpaceName}:endActiveConference", new { }, accessToken);
        return Result<bool>.Ok(true);
    }
}
