using ApiToolKit.Application.DTOs.DisplayVideo.FloodlightGroupFloodlightActivity;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoFloodlightGroupFloodlightActivityService : DisplayVideoServiceBase, IDisplayVideoFloodlightGroupFloodlightActivityService
{
    public DisplayVideoFloodlightGroupFloodlightActivityService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListFloodlightActivitiesAsync(ListFloodlightActivitiesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/floodlightGroups/{Esc(r.FloodlightGroupId)}/floodlightActivities";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListFloodlightActivities", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetFloodlightActivityAsync(GetFloodlightActivityRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/floodlightGroups/{Esc(r.FloodlightGroupId)}/floodlightActivities/{Esc(r.FloodlightActivityId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetFloodlightActivity", HttpMethod.Get, url, query);
    }
}
