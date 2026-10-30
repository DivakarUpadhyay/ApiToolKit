using ApiToolKit.Application.DTOs.DisplayVideo.GoogleAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoGoogleAudienceService : DisplayVideoServiceBase, IDisplayVideoGoogleAudienceService
{
    public DisplayVideoGoogleAudienceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListGoogleAudiencesAsync(ListGoogleAudiencesRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/googleAudiences";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "filter", r.Filter);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "ListGoogleAudiences", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetGoogleAudienceAsync(GetGoogleAudienceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/googleAudiences/{Esc(r.GoogleAudienceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetGoogleAudience", HttpMethod.Get, url, query);
    }
}
