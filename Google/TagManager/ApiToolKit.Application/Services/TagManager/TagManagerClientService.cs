using ApiToolKit.Application.DTOs.TagManager.Client;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerClientService : TagManagerServiceBase, ITagManagerClientService
{
    public TagManagerClientService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListClientsAsync(ListClientsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListClients", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetClientAsync(GetClientRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients/{r.ClientId}";
        return await SendAsync(r.AccountKey, "GetClient", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateClientAsync(CreateClientRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients";
        return await SendAsync(r.AccountKey, "CreateClient", HttpMethod.Post, url, null, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes, priority = r.Priority });
    }

    public async Task<Result<object>> UpdateClientAsync(UpdateClientRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients/{r.ClientId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateClient", HttpMethod.Put, url, query, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes, priority = r.Priority });
    }

    public async Task<Result<object>> DeleteClientAsync(DeleteClientRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients/{r.ClientId}";
        return await SendAsync(r.AccountKey, "DeleteClient", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertClientAsync(RevertClientRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/clients/{r.ClientId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertClient", HttpMethod.Post, url, query, new { });
    }
}
