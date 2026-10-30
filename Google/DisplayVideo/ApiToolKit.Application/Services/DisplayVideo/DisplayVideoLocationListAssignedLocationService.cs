using ApiToolKit.Application.DTOs.DisplayVideo.LocationListAssignedLocation;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoLocationListAssignedLocationService : DisplayVideoServiceBase, IDisplayVideoLocationListAssignedLocationService
{
    public DisplayVideoLocationListAssignedLocationService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListAssignedLocationsAsync(ListAssignedLocationsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}/assignedLocations";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListAssignedLocations", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateAssignedLocationAsync(CreateAssignedLocationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}/assignedLocations";
        return await SendAsync(r.AccountKey, "CreateAssignedLocation", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteAssignedLocationAsync(DeleteAssignedLocationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}/assignedLocations/{Esc(r.AssignedLocationId)}";
        return await SendAsync(r.AccountKey, "DeleteAssignedLocation", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> BulkEditAssignedLocationAsync(BulkEditAssignedLocationRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/locationLists/{Esc(r.LocationListId)}/assignedLocations:bulkEdit";
        return await SendAsync(r.AccountKey, "BulkEditAssignedLocation", HttpMethod.Post, url, null, r.Body);
    }
}
