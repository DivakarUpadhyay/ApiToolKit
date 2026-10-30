using ApiToolKit.Application.DTOs.CampaignManager.Conversion;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerConversionService : CampaignManagerServiceBase, ICampaignManagerConversionService
{
    public CampaignManagerConversionService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> BatchupdateConversionAsync(BatchupdateConversionRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/conversions/batchupdate";
        return await SendAsync(r.AccountKey, "BatchupdateConversion", HttpMethod.Post, url, null, r.Body);
    }

    public async Task<Result<object>> BatchinsertConversionAsync(BatchinsertConversionRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.ProfileId)}/conversions/batchinsert";
        return await SendAsync(r.AccountKey, "BatchinsertConversion", HttpMethod.Post, url, null, r.Body);
    }
}
