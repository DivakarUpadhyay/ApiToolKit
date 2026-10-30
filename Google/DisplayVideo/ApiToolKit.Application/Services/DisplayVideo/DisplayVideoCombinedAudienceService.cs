using ApiToolKit.Application.DTOs.DisplayVideo.CombinedAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCombinedAudienceService : DisplayVideoServiceBase, IDisplayVideoCombinedAudienceService
{
    public DisplayVideoCombinedAudienceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListCombinedAudiencesAsync(ListCombinedAudiencesRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/combinedAudiences";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "ListCombinedAudiences", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetCombinedAudienceAsync(GetCombinedAudienceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/combinedAudiences/{Esc(r.CombinedAudienceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetCombinedAudience", HttpMethod.Get, url, query);
    }
}
