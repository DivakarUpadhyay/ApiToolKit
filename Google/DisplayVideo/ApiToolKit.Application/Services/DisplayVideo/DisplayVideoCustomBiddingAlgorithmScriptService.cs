using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmScript;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCustomBiddingAlgorithmScriptService : DisplayVideoServiceBase, IDisplayVideoCustomBiddingAlgorithmScriptService
{
    public DisplayVideoCustomBiddingAlgorithmScriptService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListScriptsAsync(ListScriptsRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/scripts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListScripts", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetScriptAsync(GetScriptRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/scripts/{Esc(r.ScriptId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetScript", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateScriptAsync(CreateScriptRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/scripts";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateScript", HttpMethod.Post, url, query, r.Body);
    }
}
