using ApiToolKit.Application.DTOs.TagManager.Variable;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerVariableService : TagManagerServiceBase, ITagManagerVariableService
{
    public TagManagerVariableService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListVariablesAsync(ListVariablesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables";
        return await SendAsync(r.AccountKey, "ListVariables", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetVariableAsync(GetVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables/{r.VariableId}";
        return await SendAsync(r.AccountKey, "GetVariable", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateVariableAsync(CreateVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables";
        return await SendAsync(r.AccountKey, "CreateVariable", HttpMethod.Post, url, null, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateVariableAsync(UpdateVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables/{r.VariableId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateVariable", HttpMethod.Put, url, query, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteVariableAsync(DeleteVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables/{r.VariableId}";
        return await SendAsync(r.AccountKey, "DeleteVariable", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertVariableAsync(RevertVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/variables/{r.VariableId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertVariable", HttpMethod.Post, url, query, new { });
    }
}
