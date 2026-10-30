using ApiToolKit.Application.DTOs.TagManager.Transformation;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerTransformationService : TagManagerServiceBase, ITagManagerTransformationService
{
    public TagManagerTransformationService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListTransformationsAsync(ListTransformationsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListTransformations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetTransformationAsync(GetTransformationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations/{r.TransformationId}";
        return await SendAsync(r.AccountKey, "GetTransformation", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateTransformationAsync(CreateTransformationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations";
        return await SendAsync(r.AccountKey, "CreateTransformation", HttpMethod.Post, url, null, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateTransformationAsync(UpdateTransformationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations/{r.TransformationId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateTransformation", HttpMethod.Put, url, query, new { name = r.Name, type = r.Type, parameter = r.Parameter, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteTransformationAsync(DeleteTransformationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations/{r.TransformationId}";
        return await SendAsync(r.AccountKey, "DeleteTransformation", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertTransformationAsync(RevertTransformationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/transformations/{r.TransformationId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertTransformation", HttpMethod.Post, url, query, new { });
    }
}
