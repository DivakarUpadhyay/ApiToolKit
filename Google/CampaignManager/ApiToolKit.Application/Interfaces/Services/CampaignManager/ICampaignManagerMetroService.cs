using ApiToolKit.Application.DTOs.CampaignManager.Metro;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerMetroService
{
    Task<Result<object>> ListMetrosAsync(ListMetrosRequest request);
}
