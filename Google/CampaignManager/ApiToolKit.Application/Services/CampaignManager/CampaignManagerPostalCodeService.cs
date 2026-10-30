using ApiToolKit.Application.DTOs.CampaignManager.PostalCode;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerPostalCodeService : CampaignManagerServiceBase, ICampaignManagerPostalCodeService
{
    public CampaignManagerPostalCodeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListPostalCodesAsync(ListPostalCodesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/postalCodes";
        return await SendAsync(r.AccountKey, "ListPostalCodes", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetPostalCodeAsync(GetPostalCodeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/postalCodes/{Esc(r.PostalCodeId)}";
        return await SendAsync(r.AccountKey, "GetPostalCode", HttpMethod.Get, url);
    }
}
