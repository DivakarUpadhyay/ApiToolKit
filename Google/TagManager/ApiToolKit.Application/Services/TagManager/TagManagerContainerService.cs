using ApiToolKit.Application.DTOs.TagManager.Container;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerContainerService : TagManagerServiceBase, ITagManagerContainerService
{
    public TagManagerContainerService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListContainersAsync(ListContainersRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers";
        return await SendAsync(r.AccountKey, "ListContainers", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetContainerAsync(GetContainerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}";
        return await SendAsync(r.AccountKey, "GetContainer", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateContainerAsync(CreateContainerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers";
        return await SendAsync(r.AccountKey, "CreateContainer", HttpMethod.Post, url, null, new { name = r.Name, usageContext = r.UsageContext, domainName = r.DomainName, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateContainerAsync(UpdateContainerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateContainer", HttpMethod.Put, url, query, new { name = r.Name, usageContext = r.UsageContext, domainName = r.DomainName, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteContainerAsync(DeleteContainerRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}";
        return await SendAsync(r.AccountKey, "DeleteContainer", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetContainerSnippetAsync(GetContainerSnippetRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}:snippet";
        return await SendAsync(r.AccountKey, "GetContainerSnippet", HttpMethod.Get, url);
    }

    public async Task<Result<object>> LookupContainerAsync(LookupContainerRequest r)
    {
        var url = "https://tagmanager.googleapis.com/tagmanager/v2/accounts/containers:lookup";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "destinationId", r.DestinationId);
        Add(query, "tagId", r.TagId);
        return await SendAsync(r.AccountKey, "LookupContainer", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CombineContainersAsync(CombineContainersRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}:combine";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "containerId", r.SourceContainerId);
        Add(query, "settingSource", r.SettingSource);
        Add(query, "allowUserPermissionFeatureUpdate", r.AllowUserPermissionFeatureUpdate?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "CombineContainers", HttpMethod.Post, url, query, new { });
    }

    public async Task<Result<object>> MoveTagIdAsync(MoveTagIdRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}:move_tag_id";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "tagId", r.TagId);
        Add(query, "tagName", r.TagName);
        Add(query, "copySettings", r.CopySettings?.ToString().ToLower());
        Add(query, "copyTermsOfService", r.CopyTermsOfService?.ToString().ToLower());
        Add(query, "copyUsers", r.CopyUsers?.ToString().ToLower());
        Add(query, "allowUserPermissionFeatureUpdate", r.AllowUserPermissionFeatureUpdate?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "MoveTagId", HttpMethod.Post, url, query, new { });
    }
}
