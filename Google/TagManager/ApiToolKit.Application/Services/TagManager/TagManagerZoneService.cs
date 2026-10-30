using ApiToolKit.Application.DTOs.TagManager.Zone;
using ApiToolKit.Application.Interfaces.Services.TagManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.TagManager;

namespace ApiToolKit.Application.Services.TagManager;

public class TagManagerZoneService : TagManagerServiceBase, ITagManagerZoneService
{
    public TagManagerZoneService(ITagManagerApiClient apiClient, ITagManagerAuthService authService, ITagManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListZonesAsync(ListZonesRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListZones", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetZoneAsync(GetZoneRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones/{r.ZoneId}";
        return await SendAsync(r.AccountKey, "GetZone", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateZoneAsync(CreateZoneRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones";
        return await SendAsync(r.AccountKey, "CreateZone", HttpMethod.Post, url, null, new { name = r.Name, childContainer = r.ChildContainer, boundary = r.Boundary, typeRestriction = r.TypeRestriction, notes = r.Notes });
    }

    public async Task<Result<object>> UpdateZoneAsync(UpdateZoneRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones/{r.ZoneId}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "UpdateZone", HttpMethod.Put, url, query, new { name = r.Name, childContainer = r.ChildContainer, boundary = r.Boundary, typeRestriction = r.TypeRestriction, notes = r.Notes });
    }

    public async Task<Result<object>> DeleteZoneAsync(DeleteZoneRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones/{r.ZoneId}";
        return await SendAsync(r.AccountKey, "DeleteZone", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> RevertZoneAsync(RevertZoneRequest r)
    {
        var url = $"https://tagmanager.googleapis.com/tagmanager/v2/accounts/{r.AccountId}/containers/{r.ContainerId}/workspaces/{r.WorkspaceId}/zones/{r.ZoneId}:revert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "fingerprint", r.Fingerprint);
        return await SendAsync(r.AccountKey, "RevertZone", HttpMethod.Post, url, query, new { });
    }
}
