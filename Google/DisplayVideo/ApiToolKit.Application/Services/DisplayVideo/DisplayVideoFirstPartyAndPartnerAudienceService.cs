using ApiToolKit.Application.DTOs.DisplayVideo.FirstPartyAndPartnerAudience;
using ApiToolKit.Application.Interfaces.Services.DisplayVideo;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.DisplayVideo;

namespace ApiToolKit.Application.Services.DisplayVideo;

public class DisplayVideoFirstPartyAndPartnerAudienceService : DisplayVideoServiceBase, IDisplayVideoFirstPartyAndPartnerAudienceService
{
    public DisplayVideoFirstPartyAndPartnerAudienceService(IDisplayVideoApiClient apiClient, IDisplayVideoAuthService authService, IDisplayVideoAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> EditCustomerMatchMembersFirstPartyAndPartnerAudienceAsync(EditCustomerMatchMembersFirstPartyAndPartnerAudienceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/firstPartyAndPartnerAudiences/{Esc(r.FirstPartyAndPartnerAudienceId)}:editCustomerMatchMembers";
        return await SendAsync(r.AccountKey, "EditCustomerMatchMembersFirstPartyAndPartnerAudience", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> ListFirstPartyAndPartnerAudiencesAsync(ListFirstPartyAndPartnerAudiencesRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/firstPartyAndPartnerAudiences";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "filter", r.Filter);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "partnerId", r.PartnerId);
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListFirstPartyAndPartnerAudiences", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetFirstPartyAndPartnerAudienceAsync(GetFirstPartyAndPartnerAudienceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/firstPartyAndPartnerAudiences/{Esc(r.FirstPartyAndPartnerAudienceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "partnerId", r.PartnerId);
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "GetFirstPartyAndPartnerAudience", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> UpdateFirstPartyAndPartnerAudienceAsync(UpdateFirstPartyAndPartnerAudienceRequest r)
    {
        var url = $"https://displayvideo.googleapis.com/v4/firstPartyAndPartnerAudiences/{Esc(r.FirstPartyAndPartnerAudienceId)}";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        Add(query, "updateMask", r.UpdateMask);
        return await SendAsync(r.AccountKey, "UpdateFirstPartyAndPartnerAudience", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> CreateFirstPartyAndPartnerAudienceAsync(CreateFirstPartyAndPartnerAudienceRequest r)
    {
        var url = "https://displayvideo.googleapis.com/v4/firstPartyAndPartnerAudiences";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "advertiserId", r.AdvertiserId);
        return await SendAsync(r.AccountKey, "CreateFirstPartyAndPartnerAudience", HttpMethod.Post, url, query, r.Body);
    }
}
