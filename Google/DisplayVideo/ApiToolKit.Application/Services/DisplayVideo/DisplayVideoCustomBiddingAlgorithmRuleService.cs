using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithmRule;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCustomBiddingAlgorithmRuleService : DisplayVideoServiceBase, IDisplayVideoCustomBiddingAlgorithmRuleService
{
    public DisplayVideoCustomBiddingAlgorithmRuleService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> CreateRuleAsync(CreateRuleRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/rules";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "CreateRule", HttpMethod.Post, url, query, r.Body);
    }

    public async Task<Result<object>> GetRuleAsync(GetRuleRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/rules/{Esc(r.RuleId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetRule", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListRulesAsync(ListRulesRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}/rules";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListRules", HttpMethod.Get, url, query);
    }
}
