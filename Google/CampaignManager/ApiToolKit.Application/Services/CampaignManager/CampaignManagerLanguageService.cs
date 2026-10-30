using ApiToolKit.Application.DTOs.CampaignManager.Language;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerLanguageService : CampaignManagerServiceBase, ICampaignManagerLanguageService
{
    public CampaignManagerLanguageService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListLanguagesAsync(ListLanguagesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/languages";
        return await SendAsync(r.AccountKey, "ListLanguages", HttpMethod.Get, url);
    }
}
