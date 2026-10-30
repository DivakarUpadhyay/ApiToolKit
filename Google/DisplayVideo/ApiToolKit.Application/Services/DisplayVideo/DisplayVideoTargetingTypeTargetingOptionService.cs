using ApiToolKit.Application.DTOs.DisplayVideo.TargetingTypeTargetingOption;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoTargetingTypeTargetingOptionService : DisplayVideoServiceBase, IDisplayVideoTargetingTypeTargetingOptionService
{
    public DisplayVideoTargetingTypeTargetingOptionService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetTargetingOptionAsync(GetTargetingOptionRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/targetingTypes/{Esc(r.TargetingTypeId)}/targetingOptions/{Esc(r.TargetingOptionId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetTargetingOption", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> SearchTargetingOptionAsync(SearchTargetingOptionRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/targetingTypes/{Esc(r.TargetingTypeId)}/targetingOptions:search";
        return await SendAsync(r.AccountKey, "SearchTargetingOption", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListTargetingOptionsAsync(ListTargetingOptionsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/targetingTypes/{Esc(r.TargetingTypeId)}/targetingOptions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "filter", r.Filter);
        Add(query, "orderBy", r.OrderBy);
        return await SendAsync(r.AccountKey, "ListTargetingOptions", HttpMethod.Get, url, query);
    }
}
