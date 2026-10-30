using ApiToolKit.Application.DTOs.DisplayVideo.CustomBiddingAlgorithm;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoCustomBiddingAlgorithmService : DisplayVideoServiceBase, IDisplayVideoCustomBiddingAlgorithmService
{
    public DisplayVideoCustomBiddingAlgorithmService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UploadRulesCustomBiddingAlgorithmAsync(UploadRulesCustomBiddingAlgorithmRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}:uploadRules";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "UploadRulesCustomBiddingAlgorithm", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> ListCustomBiddingAlgorithmsAsync(ListCustomBiddingAlgorithmsRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/customBiddingAlgorithms";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "ListCustomBiddingAlgorithms", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetCustomBiddingAlgorithmAsync(GetCustomBiddingAlgorithmRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "GetCustomBiddingAlgorithm", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UploadScriptCustomBiddingAlgorithmAsync(UploadScriptCustomBiddingAlgorithmRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}:uploadScript";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        return await SendAsync(r.AccountKey, "UploadScriptCustomBiddingAlgorithm", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> CreateCustomBiddingAlgorithmAsync(CreateCustomBiddingAlgorithmRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/customBiddingAlgorithms";
        return await SendAsync(r.AccountKey, "CreateCustomBiddingAlgorithm", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> UpdateCustomBiddingAlgorithmAsync(UpdateCustomBiddingAlgorithmRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/customBiddingAlgorithms/{Esc(r.CustomBiddingAlgorithmId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateCustomBiddingAlgorithm", HttpMethod.Patch, url, query, r.Body);
    }
}
