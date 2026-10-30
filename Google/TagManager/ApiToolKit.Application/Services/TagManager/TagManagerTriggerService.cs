using ApiToolKit.Application.DTOs.TagManager.Trigger;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerTriggerService : TagManagerServiceBase, ITagManagerTriggerService
{
    public TagManagerTriggerService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTriggersAsync(ListTriggersRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers";
        return await SendAsync(r.AccountKey, "ListTriggers", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetTriggerAsync(GetTriggerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers/{r.TriggerId}";
        return await SendAsync(r.AccountKey, "GetTrigger", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateTriggerAsync(CreateTriggerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers";
        return await SendAsync(r.AccountKey, "CreateTrigger", HttpMethod.Post, url, null, new { name = r.Name, type = r.Type, filter = r.Filter, customEventFilter = r.CustomEventFilter, autoEventFilter = r.AutoEventFilter, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateTriggerAsync(UpdateTriggerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers/{r.TriggerId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateTrigger", HttpMethod.Put, url, query, new { name = r.Name, type = r.Type, filter = r.Filter, customEventFilter = r.CustomEventFilter, autoEventFilter = r.AutoEventFilter, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteTriggerAsync(DeleteTriggerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers/{r.TriggerId}";
        return await SendAsync(r.AccountKey, "DeleteTrigger", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertTriggerAsync(RevertTriggerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/triggers/{r.TriggerId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertTrigger", HttpMethod.Post, url, query, new { });
    }
}
