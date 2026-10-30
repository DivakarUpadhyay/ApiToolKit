using ApiToolKit.Application.DTOs.TagManager.Version;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerVersionService : TagManagerServiceBase, ITagManagerVersionService
{
    public TagManagerVersionService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetLiveVersionAsync(GetLiveVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions:live";
        return await SendAsync(r.AccountKey, "GetLiveVersion", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListVersionHeadersAsync(ListVersionHeadersRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/version_headers";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "includeDeleted", r.IncludeDeleted?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "ListVersionHeaders", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetLatestVersionHeaderAsync(GetLatestVersionHeaderRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/version_headers:latest";
        return await SendAsync(r.AccountKey, "GetLatestVersionHeader", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetVersionAsync(GetVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}";
        return await SendAsync(r.AccountKey, "GetVersion", HttpMethod.Get, url);
    }

    public async Task<Result<object>> PublishVersionAsync(PublishVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}:publish";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "PublishVersion", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> DeleteVersionAsync(DeleteVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}";
        return await SendAsync(r.AccountKey, "DeleteVersion", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> UpdateVersionAsync(UpdateVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateVersion", HttpMethod.Put, url, query, new { name = r.Name, description = r.Description });
    }

    public async Task<Result<object>> SetLatestVersionAsync(SetLatestVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}:set_latest";
        return await SendAsync(r.AccountKey, "SetLatestVersion", HttpMethod.Post, url, null, new { });
    }

    public async Task<Result<object>> UndeleteVersionAsync(UndeleteVersionRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/versions/{r.VersionId}:undelete";
        return await SendAsync(r.AccountKey, "UndeleteVersion", HttpMethod.Post, url, null, new { });
    }
}
