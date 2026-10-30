using ApiToolKit.Application.DTOs.CampaignManager.ConnectionType;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerConnectionTypeService : CampaignManagerServiceBase, ICampaignManagerConnectionTypeService
{
    public CampaignManagerConnectionTypeService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListConnectionTypesAsync(ListConnectionTypesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/connectionTypes";
        return await SendAsync(r.AccountKey, "ListConnectionTypes", HttpMethod.Get, url);
    }

    public async Task<Result<object>> GetConnectionTypeAsync(GetConnectionTypeRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/connectionTypes/{Esc(r.ConnectionTypeId)}";
        return await SendAsync(r.AccountKey, "GetConnectionType", HttpMethod.Get, url);
    }
}
