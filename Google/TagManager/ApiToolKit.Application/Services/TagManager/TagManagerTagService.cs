using ApiToolKit.Application.DTOs.TagManager.Tag;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerTagService : TagManagerServiceBase, ITagManagerTagService
{
    public TagManagerTagService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTagsAsync(ListTagsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags";
        return await SendAsync(r.AccountKey, "ListTags", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetTagAsync(GetTagRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags/{r.TagId}";
        return await SendAsync(r.AccountKey, "GetTag", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateTagAsync(CreateTagRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags";
        return await SendAsync(r.AccountKey, "CreateTag", HttpMethod.Post, url, null, new { name = r.Name, type = r.Type, parameter = r.Parameter, firingTriggerId = r.FiringTriggerId, blockingTriggerId = r.BlockingTriggerId, notes = r.Notes, paused = r.Paused });
    }

    public async Task<Result<object>> UpdateTagAsync(UpdateTagRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags/{r.TagId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateTag", HttpMethod.Put, url, query, new { name = r.Name, type = r.Type, parameter = r.Parameter, firingTriggerId = r.FiringTriggerId, blockingTriggerId = r.BlockingTriggerId, notes = r.Notes, paused = r.Paused });
    }

    public async Task<Result<object>> DeleteTagAsync(DeleteTagRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags/{r.TagId}";
        return await SendAsync(r.AccountKey, "DeleteTag", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertTagAsync(RevertTagRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/tags/{r.TagId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertTag", HttpMethod.Post, url, query, new { });
    }
}
