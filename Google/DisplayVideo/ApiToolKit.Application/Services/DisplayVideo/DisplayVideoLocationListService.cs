using ApiToolKit.Application.DTOs.DisplayVideo.LocationList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoLocationListService : DisplayVideoServiceBase, IDisplayVideoLocationListService
{
    public DisplayVideoLocationListService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateLocationListAsync(UpdateLocationListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateLocationList", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetLocationListAsync(GetLocationListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}";
        return await SendAsync(r.AccountKey, "GetLocationList", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListLocationListsAsync(ListLocationListsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListLocationLists", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateLocationListAsync(CreateLocationListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists";
        return await SendAsync(r.AccountKey, "CreateLocationList", HttpMethod.Post, url, null, r.Body);
    }
}
