using ApiToolKit.Application.DTOs.TagManager.Destination;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerDestinationService : TagManagerServiceBase, ITagManagerDestinationService
{
    public TagManagerDestinationService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListDestinationsAsync(ListDestinationsRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/destinations";
        return await SendAsync(r.AccountKey, "ListDestinations", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetDestinationAsync(GetDestinationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/destinations/{r.DestinationLinkId}";
        return await SendAsync(r.AccountKey, "GetDestination", HttpMethod.Get, url);
    }

    public async Task<Result<object>> LinkDestinationAsync(LinkDestinationRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/destinations:link";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "destinationId", r.DestinationId);
        Add(query, "allowUserPermissionFeatureUpdate", r.AllowUserPermissionFeatureUpdate?.ToString().ToLower());
        return await SendAsync(r.AccountKey, "LinkDestination", HttpMethod.Post, url, query, new { });
    }
}
