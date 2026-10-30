using ApiToolKit.Application.DTOs.TagManager.Environment;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerEnvironmentService : TagManagerServiceBase, ITagManagerEnvironmentService
{
    public TagManagerEnvironmentService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListEnvironmentsAsync(ListEnvironmentsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments";
        return await SendAsync(r.AccountKey, "ListEnvironments", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetEnvironmentAsync(GetEnvironmentRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments/{r.EnvironmentId}";
        return await SendAsync(r.AccountKey, "GetEnvironment", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateEnvironmentAsync(CreateEnvironmentRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments";
        return await SendAsync(r.AccountKey, "CreateEnvironment", HttpMethod.Post, url, null, new { name = r.Name, description = r.Description, url = r.Url, enableDebug = r.EnableDebug, containerVersionId = r.ContainerVersionId });
    }

    public async Task<Result<object>> UpdateEnvironmentAsync(UpdateEnvironmentRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments/{r.EnvironmentId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateEnvironment", HttpMethod.Put, url, query, new { name = r.Name, description = r.Description, url = r.Url, enableDebug = r.EnableDebug, containerVersionId = r.ContainerVersionId });
    }

    public async Task<Result<object>> DeleteEnvironmentAsync(DeleteEnvironmentRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments/{r.EnvironmentId}";
        return await SendAsync(r.AccountKey, "DeleteEnvironment", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ReauthorizeEnvironmentAsync(ReauthorizeEnvironmentRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/environments/{r.EnvironmentId}:reauthorize";
        return await SendAsync(r.AccountKey, "ReauthorizeEnvironment", HttpMethod.Post, url, null, new { });
    }
}
