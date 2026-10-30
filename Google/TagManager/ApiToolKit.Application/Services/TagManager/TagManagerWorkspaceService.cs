using ApiToolKit.Application.DTOs.TagManager.Workspace;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerWorkspaceService : TagManagerServiceBase, ITagManagerWorkspaceService
{
    public TagManagerWorkspaceService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListWorkspacesAsync(ListWorkspacesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces";
        return await SendAsync(r.AccountKey, "ListWorkspaces", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetWorkspaceAsync(GetWorkspaceRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}";
        return await SendAsync(r.AccountKey, "GetWorkspace", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateWorkspaceAsync(CreateWorkspaceRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces";
        return await SendAsync(r.AccountKey, "CreateWorkspace", HttpMethod.Post, url, null, new { name = r.Name, description = r.Description });
    }

    public async Task<Result<object>> DeleteWorkspaceAsync(DeleteWorkspaceRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}";
        return await SendAsync(r.AccountKey, "DeleteWorkspace", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetWorkspaceStatusAsync(GetWorkspaceStatusRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/status";
        return await SendAsync(r.AccountKey, "GetWorkspaceStatus", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateVersionAsync(CreateVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}:create_version";
        return await SendAsync(r.AccountKey, "CreateVersion", HttpMethod.Post, url, null, new { name = r.Name, notes = r.Notes });
    }

    public async Task<Result<object>> SyncWorkspaceAsync(SyncWorkspaceRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}:sync";
        return await SendAsync(r.AccountKey, "SyncWorkspace", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> UpdateWorkspaceAsync(UpdateWorkspaceRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateWorkspace", HttpMethod.Put, url, query, new { name = r.Name, description = r.Description });
    }

    public async Task<Result<object>> QuickPreviewAsync(QuickPreviewRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}:quick_preview";
        return await SendAsync(r.AccountKey, "QuickPreview", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> ResolveConflictAsync(ResolveConflictRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}:resolve_conflict";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "ResolveConflict", HttpMethod.Post, url, query, r.Entity);
    }
}
