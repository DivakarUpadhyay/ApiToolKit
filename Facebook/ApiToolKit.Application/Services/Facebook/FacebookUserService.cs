using ApiToolKit.Application.DTOs.Facebook.User;
using ApiToolKit.Application.DTOs.Facebook.Page;
using ApiToolKit.Application.DTOs.Facebook.AdAccount;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookUserService : IFacebookUserService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookUserRepository _userRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookUserService(IFacebookAuthRepository authRepo, IFacebookUserRepository userRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _userRepo = userRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<UserProfileResponse>> GetUserProfileAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UserProfileResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,email,picture,birthday,gender,location,about,link",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}", queryParams);
        if (response == null) return Result<UserProfileResponse>.Fail("No response from Facebook API.");

        var profile = new UserProfileResponse
        {
            Id = response?.id?.ToString() ?? string.Empty,
            Name = response?.name?.ToString() ?? string.Empty,
            Email = response?.email?.ToString(),
            Picture = response?.picture?.data?.url?.ToString(),
            Birthday = response?.birthday?.ToString(),
            Gender = response?.gender?.ToString(),
            Location = response?.location?.name?.ToString(),
            About = response?.about?.ToString(),
            Link = response?.link?.ToString()
        };

        await _userRepo.SaveUserAsync(profile.Id, profile.Name, profile.Email, profile.Picture);
        await _userRepo.SaveApiLogAsync(new FacebookApiLog
        {
            UserId = userId,
            ApiName = "GetUserProfile",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<UserProfileResponse>.Ok(profile);
    }

    public async Task<Result<UserFriendsResponse>> GetUserFriendsAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UserFriendsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/friends", queryParams);
        if (response == null) return Result<UserFriendsResponse>.Fail("No response from Facebook API.");

        var friends = JsonConvert.DeserializeObject<List<UserFriendItem>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<UserFriendItem>();
        int total = response?.summary?.total_count ?? 0;

        return Result<UserFriendsResponse>.Ok(new UserFriendsResponse { Data = friends, Total = total });
    }

    public async Task<Result<UserPermissionsResponse>> GetUserPermissionsAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UserPermissionsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/permissions", queryParams);
        if (response == null) return Result<UserPermissionsResponse>.Fail("No response from Facebook API.");

        var permissions = JsonConvert.DeserializeObject<List<UserPermissionItem>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<UserPermissionItem>();

        return Result<UserPermissionsResponse>.Ok(new UserPermissionsResponse { Data = permissions });
    }

    public async Task<Result<UserPictureResponse>> GetUserPictureAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UserPictureResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "is_silhouette,url,width,height",
            ["redirect"] = "false",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/picture", queryParams);
        if (response == null) return Result<UserPictureResponse>.Fail("No response from Facebook API.");

        var data = response?.data;
        return Result<UserPictureResponse>.Ok(new UserPictureResponse
        {
            IsSilhouette = data?.is_silhouette == true,
            Url = data?.url?.ToString() ?? string.Empty,
            Width = data?.width ?? 0,
            Height = data?.height ?? 0
        });
    }

    public async Task<Result<UserFeedResponse>> GetUserFeedAsync(string userId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<UserFeedResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/feed", queryParams);
        if (response == null) return Result<UserFeedResponse>.Fail("No response from Facebook API.");

        var data = JsonConvert.DeserializeObject<List<object>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<object>();

        return Result<UserFeedResponse>.Ok(new UserFeedResponse { Data = data });
    }

    public async Task<Result<GetPagesResponse>> GetUserPagesAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPagesResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,category,about,description,followers_count,fan_count,website,phone,emails,picture,cover,access_token",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/accounts", queryParams);
        if (response == null) return Result<GetPagesResponse>.Fail("No response from Facebook API.");

        var pages = JsonConvert.DeserializeObject<List<PageResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PageResponse>();

        return Result<GetPagesResponse>.Ok(new GetPagesResponse { Data = pages });
    }

    public async Task<Result<List<AdAccountResponse>>> GetUserAdAccountsAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<AdAccountResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,account_id,name,currency,balance,account_status,timezone_name,business",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/adaccounts", queryParams);
        if (response == null) return Result<List<AdAccountResponse>>.Fail("No response from Facebook API.");

        var accounts = JsonConvert.DeserializeObject<List<AdAccountResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<AdAccountResponse>();

        return Result<List<AdAccountResponse>>.Ok(accounts);
    }
}
