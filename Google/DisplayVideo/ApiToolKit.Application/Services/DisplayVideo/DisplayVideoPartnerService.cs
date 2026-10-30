using ApiToolKit.Application.DTOs.DisplayVideo.Partner;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoPartnerService : DisplayVideoServiceBase, IDisplayVideoPartnerService
{
    public DisplayVideoPartnerService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> EditAssignedTargetingOptionsPartnerAsync(EditAssignedTargetingOptionsPartnerRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}:editAssignedTargetingOptions";
        return await SendAsync(r.AccountKey, "EditAssignedTargetingOptionsPartner", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListPartnersAsync(ListPartnersRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/partners";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListPartners", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetPartnerAsync(GetPartnerRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/partners/{Esc(r.PartnerId)}";
        return await SendAsync(r.AccountKey, "GetPartner", HttpMethod.Get, url);
    }
}
