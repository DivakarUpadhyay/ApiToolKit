using ApiToolKit.Application.DTOs.CampaignManager.Country;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCountryService : CampaignManagerServiceBase, ICampaignManagerCountryService
{
    public CampaignManagerCountryService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> GetCountryAsync(GetCountryRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/countries/{Esc(r.CountryId)}";
        return await SendAsync(r.AccountKey, "GetCountry", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListCountriesAsync(ListCountriesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/countries";
        return await SendAsync(r.AccountKey, "ListCountries", HttpMethod.Get, url);
    }
}
