using ApiToolKit.Application.DTOs.TagManager.UserPermission;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerUserPermissionService : TagManagerServiceBase, ITagManagerUserPermissionService
{
    public TagManagerUserPermissionService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListUserPermissionsAsync(ListUserPermissionsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/user_permissions";
        return await SendAsync(r.AccountKey, "ListUserPermissions", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetUserPermissionAsync(GetUserPermissionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/user_permissions/{r.PermissionId}";
        return await SendAsync(r.AccountKey, "GetUserPermission", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateUserPermissionAsync(CreateUserPermissionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/user_permissions";
        return await SendAsync(r.AccountKey, "CreateUserPermission", HttpMethod.Post, url, null, new { emailAddress = r.EmailAddress, accountAccess = new { permission = r.AccountPermission }, containerAccess = r.ContainerAccess });
    }

    public async Task<Result<object>> UpdateUserPermissionAsync(UpdateUserPermissionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/user_permissions/{r.PermissionId}";
        return await SendAsync(r.AccountKey, "UpdateUserPermission", HttpMethod.Put, url, null, new { emailAddress = r.EmailAddress, accountAccess = new { permission = r.AccountPermission }, containerAccess = r.ContainerAccess });
    }

    public async Task<Result<object>> DeleteUserPermissionAsync(DeleteUserPermissionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/user_permissions/{r.PermissionId}";
        return await SendAsync(r.AccountKey, "DeleteUserPermission", HttpMethod.Delete, url);
    }
}
