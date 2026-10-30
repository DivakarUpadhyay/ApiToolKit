using ApiToolKit.Application.DTOs.DisplayVideo.CustomList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCustomListService : DisplayVideoServiceBase, IDisplayVideoCustomListService
{
    public DisplayVideoCustomListService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListCustomListsAsync(ListCustomListsRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/customLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "ListCustomLists", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetCustomListAsync(GetCustomListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customLists/{Esc(r.CustomListId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetCustomList", HttpMethod.Get, url, query);
    }
}
