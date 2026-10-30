using ApiToolKit.Application.DTOs.TagManager.GtagConfig;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerGtagConfigService : TagManagerServiceBase, ITagManagerGtagConfigService
{
    public TagManagerGtagConfigService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListGtagConfigsAsync(ListGtagConfigsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/gtag_config";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListGtagConfigs", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetGtagConfigAsync(GetGtagConfigRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/gtag_config/{r.GtagConfigId}";
        return await SendAsync(r.AccountKey, "GetGtagConfig", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateGtagConfigAsync(CreateGtagConfigRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/gtag_config";
        return await SendAsync(r.AccountKey, "CreateGtagConfig", HttpMethod.Post, url, null, new { type = r.Type, parameter = r.Parameter });
    }

    public async Task<Result<object>> UpdateGtagConfigAsync(UpdateGtagConfigRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/gtag_config/{r.GtagConfigId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateGtagConfig", HttpMethod.Put, url, query, new { type = r.Type, parameter = r.Parameter });
    }

    public async Task<Result<object>> DeleteGtagConfigAsync(DeleteGtagConfigRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/gtag_config/{r.GtagConfigId}";
        return await SendAsync(r.AccountKey, "DeleteGtagConfig", HttpMethod.Delete, url);
    }
}
