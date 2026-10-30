using ApiToolKit.Application.DTOs.CampaignManager.UserRole;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerUserRoleService : CampaignManagerServiceBase, ICampaignManagerUserRoleService
{
    public CampaignManagerUserRoleService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListUserRolesAsync(ListUserRolesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "sortOrder", r.SortOrder);
        Add(query, "searchString", r.SearchString);
        Add(query, "pageToken", r.PageToken);
        Add(query, "accountUserRoleOnly", r.AccountUserRoleOnly?.ToString().ToLower());
        Add(query, "sortField", r.SortField);
        Add(query, "subaccountId", r.SubaccountId);
        AddMany(query, "ids", r.Ids);
        Add(query, "maxResults", r.MaxResults?.ToString());
        return await SendAsync(r.AccountKey, "ListUserRoles", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertUserRoleAsync(InsertUserRoleRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles";
        return await SendAsync(r.AccountKey, "InsertUserRole", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateUserRoleAsync(UpdateUserRoleRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "UpdateUserRole", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetUserRoleAsync(GetUserRoleRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles/{Esc(r.UserRoleId)}";
        return await SendAsync(r.AccountKey, "GetUserRole", HttpMethod.Get, url);
    }

    public async Task<Result<object>> DeleteUserRoleAsync(DeleteUserRoleRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles/{Esc(r.UserRoleId)}";
        return await SendAsync(r.AccountKey, "DeleteUserRole", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> UpdateUserRoleAltAsync(UpdateUserRoleAltRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/userRoles";
        return await SendAsync(r.AccountKey, "UpdateUserRoleAlt", HttpMethod.Put, url, null, r.Body);
    }
}
