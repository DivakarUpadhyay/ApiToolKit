using ApiToolKit.Application.DTOs.TagManager.Folder;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerFolderService : TagManagerServiceBase, ITagManagerFolderService
{
    public TagManagerFolderService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListFoldersAsync(ListFoldersRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListFolders", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetFolderAsync(GetFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}";
        return await SendAsync(r.AccountKey, "GetFolder", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateFolderAsync(CreateFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders";
        return await SendAsync(r.AccountKey, "CreateFolder", HttpMethod.Post, url, null, new { name = r.Name, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateFolderAsync(UpdateFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateFolder", HttpMethod.Put, url, query, new { name = r.Name, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteFolderAsync(DeleteFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}";
        return await SendAsync(r.AccountKey, "DeleteFolder", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertFolderAsync(RevertFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertFolder", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> GetFolderEntitiesAsync(GetFolderEntitiesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}:entities";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "GetFolderEntities", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> MoveEntitiesToFolderAsync(MoveEntitiesToFolderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/folders/{r.FolderId}:move_entities_to_folder";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "tagId", r.TagIds);
        AddMany(query, "triggerId", r.TriggerIds);
        AddMany(query, "variableId", r.VariableIds);
        return await SendAsync(r.AccountKey, "MoveEntitiesToFolder", HttpMethod.Post, url, query, new { });
    }
}
