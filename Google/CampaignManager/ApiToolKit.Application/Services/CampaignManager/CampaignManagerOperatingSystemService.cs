using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystem;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerOperatingSystemService : CampaignManagerServiceBase, ICampaignManagerOperatingSystemService
{
    public CampaignManagerOperatingSystemService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetOperatingSystemAsync(GetOperatingSystemRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/operatingSystems/{Esc(r.OperatingSystemId)}";
        return await SendAsync(r.AccountKey, "GetOperatingSystem", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListOperatingSystemsAsync(ListOperatingSystemsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/operatingSystems";
        return await SendAsync(r.AccountKey, "ListOperatingSystems", HttpMethod.Get, url);
    }
}
