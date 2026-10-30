using ApiToolKit.Application.DTOs.DisplayVideo.User;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoUserService : DisplayVideoServiceBase, IDisplayVideoUserService
{
    public DisplayVideoUserService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateUserAsync(CreateUserRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/users";
        return await SendAsync(r.AccountKey, "CreateUser", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> GetUserAsync(GetUserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/users/{Esc(r.UserId)}";
        return await SendAsync(r.AccountKey, "GetUser", HttpMethod.Get, url);
    }

    public async Task<Result<object>> UpdateUserAsync(UpdateUserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/users/{Esc(r.UserId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateUser", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> DeleteUserAsync(DeleteUserRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/users/{Esc(r.UserId)}";
        return await SendAsync(r.AccountKey, "DeleteUser", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> BulkEditAssignedUserRolesAsync(BulkEditAssignedUserRolesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/users/{Esc(r.UserId)}:bulkEditAssignedUserRoles";
        return await SendAsync(r.AccountKey, "BulkEditAssignedUserRoles", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListUsersAsync(ListUsersRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/users";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListUsers", HttpMethod.Get, url, query);
    }
}
