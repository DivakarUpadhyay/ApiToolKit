using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordList;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoNegativeKeywordListService : DisplayVideoServiceBase, IDisplayVideoNegativeKeywordListService
{
    public DisplayVideoNegativeKeywordListService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteNegativeKeywordListAsync(DeleteNegativeKeywordListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}";
        return await SendAsync(r.AccountKey, "DeleteNegativeKeywordList", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListNegativeKeywordListsAsync(ListNegativeKeywordListsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListNegativeKeywordLists", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateNegativeKeywordListAsync(UpdateNegativeKeywordListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateNegativeKeywordList", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetNegativeKeywordListAsync(GetNegativeKeywordListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}";
        return await SendAsync(r.AccountKey, "GetNegativeKeywordList", HttpMethod.Get, url);
    }

    public async Task<Result<object>> CreateNegativeKeywordListAsync(CreateNegativeKeywordListRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists";
        return await SendAsync(r.AccountKey, "CreateNegativeKeywordList", HttpMethod.Post, url, null, r.Body);
    }
}
