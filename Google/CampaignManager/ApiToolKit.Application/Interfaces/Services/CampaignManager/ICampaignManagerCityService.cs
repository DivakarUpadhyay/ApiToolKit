using ApiToolKit.Application.DTOs.CampaignManager.City;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCityService
{
    Task<Result<object>> ListCitiesAsync(ListCitiesRequest request);
}
