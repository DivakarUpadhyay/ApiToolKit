using ApiToolKit.Application.DTOs.CampaignManager.OperatingSystemVersion;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerOperatingSystemVersionService : CampaignManagerServiceBase, ICampaignManagerOperatingSystemVersionService
{
    public CampaignManagerOperatingSystemVersionService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetOperatingSystemVersionAsync(GetOperatingSystemVersionRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/operatingSystemVersions/{Esc(r.OperatingSystemVersionId)}";
        return await SendAsync(r.AccountKey, "GetOperatingSystemVersion", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListOperatingSystemVersionsAsync(ListOperatingSystemVersionsRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/operatingSystemVersions";
        return await SendAsync(r.AccountKey, "ListOperatingSystemVersions", HttpMethod.Get, url);
    }
}
