using ApiToolKit.Application.DTOs.Facebook.User;
using ApiToolKit.Application.DTOs.Facebook.Page;
using ApiToolKit.Application.DTOs.Facebook.AdAccount;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookUserService
{
    Task<Result<UserProfileResponse>> GetUserProfileAsync(string userId, string accessToken);
    Task<Result<UserFriendsResponse>> GetUserFriendsAsync(string userId, string accessToken);
    Task<Result<UserPermissionsResponse>> GetUserPermissionsAsync(string userId, string accessToken);
    Task<Result<UserPictureResponse>> GetUserPictureAsync(string userId, string accessToken);
    Task<Result<UserFeedResponse>> GetUserFeedAsync(string userId, string accessToken, int limit);
    Task<Result<GetPagesResponse>> GetUserPagesAsync(string userId, string accessToken);
    Task<Result<List<AdAccountResponse>>> GetUserAdAccountsAsync(string userId, string accessToken);
}
