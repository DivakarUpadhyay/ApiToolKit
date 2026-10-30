using ApiToolKit.Application.DTOs.DisplayVideo.ReachForecast;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoReachForecastService : DisplayVideoServiceBase, IDisplayVideoReachForecastService
{
    public DisplayVideoReachForecastService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> RetrievePlannableProductsReachForecastAsync(RetrievePlannableProductsReachForecastRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/reachForecast:retrievePlannableProducts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "plannableLocationId", r.PlannableLocationId);
        return await SendAsync(r.AccountKey, "RetrievePlannableProductsReachForecast", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GenerateReachForecastAsync(GenerateReachForecastRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/reachForecast:generateReachForecast";
        return await SendAsync(r.AccountKey, "GenerateReachForecast", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> RetrievePlannableUserListsReachForecastAsync(RetrievePlannableUserListsReachForecastRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/reachForecast:retrievePlannableUserLists";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        Add(query, "filter", r.Filter);
        return await SendAsync(r.AccountKey, "RetrievePlannableUserListsReachForecast", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> RetrievePlannableUserInterestsReachForecastAsync(RetrievePlannableUserInterestsReachForecastRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/reachForecast:retrievePlannableUserInterests";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "productCategory", r.ProductCategory);
        return await SendAsync(r.AccountKey, "RetrievePlannableUserInterestsReachForecast", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> RetrievePlannableLocationsReachForecastAsync(RetrievePlannableLocationsReachForecastRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/advertisers/{Esc(r.AdvertiserId)}/reachForecast:retrievePlannableLocations";
        return await SendAsync(r.AccountKey, "RetrievePlannableLocationsReachForecast", HttpMethod.Get, url);
    }
}
