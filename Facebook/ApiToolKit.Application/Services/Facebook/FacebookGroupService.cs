using ApiToolKit.Application.DTOs.Facebook.Group;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookGroupService : IFacebookGroupService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookGroupService(IFacebookAuthRepository authRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<GroupResponse>> GetGroupAsync(string groupId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GroupResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,privacy,description,member_count,member_request_count",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{groupId}", queryParams);
        if (response == null) return Result<GroupResponse>.Fail("No response from Facebook API.");

        var group = JsonConvert.DeserializeObject<GroupResponse>(JsonConvert.SerializeObject(response)) ?? new GroupResponse();
        return Result<GroupResponse>.Ok(group);
    }

    public async Task<Result<List<GroupResponse>>> GetGroupsByUserAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<GroupResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,privacy,description,member_count",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/groups", queryParams);
        if (response == null) return Result<List<GroupResponse>>.Fail("No response from Facebook API.");

        var groups = JsonConvert.DeserializeObject<List<GroupResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<GroupResponse>();

        return Result<List<GroupResponse>>.Ok(groups);
    }

    public async Task<Result<object>> GetGroupFeedAsync(GroupFeedRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,story,created_time,from,permalink_url",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<object>($"/{request.GroupId}/feed", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<List<GroupMemberResponse>>> GetGroupMembersAsync(GroupMembersRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<GroupMemberResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,administrator,picture",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.GroupId}/members", queryParams);
        if (response == null) return Result<List<GroupMemberResponse>>.Fail("No response from Facebook API.");

        var members = JsonConvert.DeserializeObject<List<GroupMemberResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<GroupMemberResponse>();

        return Result<List<GroupMemberResponse>>.Ok(members);
    }

    public async Task<Result<bool>> CreateGroupPostAsync(CreateGroupPostRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Message != null) body["message"] = request.Message;
        if (request.Link != null) body["link"] = request.Link;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.GroupId}/feed", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetGroupAlbumsAsync(GroupAlbumsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,created_time,count",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<object>($"/{request.GroupId}/albums", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }
}
