using ApiToolKit.Application.DTOs.CampaignManager.Country;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCountryService
{
    Task<Result<object>> GetCountryAsync(GetCountryRequest request);
    Task<Result<object>> ListCountriesAsync(ListCountriesRequest request);
}
