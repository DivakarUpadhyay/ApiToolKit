using ApiToolKit.Application.DTOs.CampaignManager.PostalCode;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerPostalCodeService
{
    Task<Result<object>> ListPostalCodesAsync(ListPostalCodesRequest request);
    Task<Result<object>> GetPostalCodeAsync(GetPostalCodeRequest request);
}
