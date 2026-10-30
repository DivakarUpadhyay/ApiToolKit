using ApiToolKit.Application.DTOs.CampaignManager.City;
using ApiToolKit.Application.Interfaces.Services.CampaignManager;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.CampaignManager;

namespace ApiToolKit.Application.Services.CampaignManager;

public class CampaignManagerCityService : CampaignManagerServiceBase, ICampaignManagerCityService
{
    public CampaignManagerCityService(ICampaignManagerApiClient apiClient, ICampaignManagerAuthService authService, ICampaignManagerAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListCitiesAsync(ListCitiesRequest r)
    {
        var url = $"https://dfareporting.googleapis.com/dfareporting/v5/userprofiles/{Esc(r.UserprofileId)}/cities";
        var query = new List<KeyValuePair<string, string?>>();
        AddMany(query, "regionDartIds", r.RegionDartIds);
        Add(query, "namePrefix", r.NamePrefix);
        AddMany(query, "dartIds", r.DartIds);
        AddMany(query, "countryDartIds", r.CountryDartIds);
        return await SendAsync(r.AccountKey, "ListCities", HttpMethod.Get, url, query);
    }
}
