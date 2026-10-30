using ApiToolKit.Application.DTOs.TagManager.BuiltInVariable;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerBuiltInVariableService : TagManagerServiceBase, ITagManagerBuiltInVariableService
{
    public TagManagerBuiltInVariableService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListBuiltInVariablesAsync(ListBuiltInVariablesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/built_in_variables";
        return await SendAsync(r.AccountKey, "ListBuiltInVariables", HttpMethod.Get, url);
    }

    public async Task<Result<object>> EnableBuiltInVariablesAsync(EnableBuiltInVariablesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/built_in_variables";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "type", r.Types);
        return await SendAsync(r.AccountKey, "EnableBuiltInVariables", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> DisableBuiltInVariablesAsync(DisableBuiltInVariablesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/built_in_variables";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "type", r.Types);
        return await SendAsync(r.AccountKey, "DisableBuiltInVariables", HttpMethod.Delete, url, query);
    }

    public async Task<Result<object>> RevertBuiltInVariableAsync(RevertBuiltInVariableRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/built_in_variables:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "type", r.Type);
        return await SendAsync(r.AccountKey, "RevertBuiltInVariable", HttpMethod.Post, url, query, new { });
    }
}
