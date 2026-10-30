using ApiToolKit.Application.DTOs.DisplayVideo.NegativeKeywordListNegativeKeyword;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoNegativeKeywordListNegativeKeywordService : DisplayVideoServiceBase, IDisplayVideoNegativeKeywordListNegativeKeywordService
{
    public DisplayVideoNegativeKeywordListNegativeKeywordService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> BulkEditNegativeKeywordAsync(BulkEditNegativeKeywordRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}/negativeKeywords:bulkEdit";
        return await SendAsync(r.AccountKey, "BulkEditNegativeKeyword", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> DeleteNegativeKeywordAsync(DeleteNegativeKeywordRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}/negativeKeywords/{Esc(r.NegativeKeywordId)}";
        return await SendAsync(r.AccountKey, "DeleteNegativeKeyword", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListNegativeKeywordsAsync(ListNegativeKeywordsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}/negativeKeywords";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "ListNegativeKeywords", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ReplaceNegativeKeywordAsync(ReplaceNegativeKeywordRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}/negativeKeywords:replace";
        return await SendAsync(r.AccountKey, "ReplaceNegativeKeyword", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> CreateNegativeKeywordAsync(CreateNegativeKeywordRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/negativeKeywordLists/{Esc(r.NegativeKeywordListId)}/negativeKeywords";
        return await SendAsync(r.AccountKey, "CreateNegativeKeyword", HttpMethod.Post, url, null, r.Body);
    }
}
