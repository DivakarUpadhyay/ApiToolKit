using ApiToolKit.Application.DTOs.CampaignManager.RemarketingListShare;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerRemarketingListShareService : CampaignManagerServiceBase, ICampaignManagerRemarketingListShareService
{
    public CampaignManagerRemarketingListShareService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> UpdateRemarketingListShareAsync(UpdateRemarketingListShareRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingListShares";
        return await SendAsync(r.AccountKey, "UpdateRemarketingListShare", HttpMethod.Put, url, null, r.Body);
    }

    public async Task<Result<object>> PatchRemarketingListShareAsync(PatchRemarketingListShareRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingListShares";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "id", r.Id);
        return await SendAsync(r.AccountKey, "PatchRemarketingListShare", HttpMethod.Patch, url, query, r.Body);
    }

    public async Task<Result<object>> GetRemarketingListShareAsync(GetRemarketingListShareRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/remarketingListShares/{Esc(r.RemarketingListShareId)}";
        return await SendAsync(r.AccountKey, "GetRemarketingListShare", HttpMethod.Get, url);
    }
}
