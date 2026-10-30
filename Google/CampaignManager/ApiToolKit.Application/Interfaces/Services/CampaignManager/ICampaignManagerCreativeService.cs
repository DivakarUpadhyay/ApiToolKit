using ApiToolKit.Application.DTOs.CampaignManager.Creative;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCreativeService
{
    Task<Result<object>> ListCreativesAsync(ListCreativesRequest request);
    Task<Result<object>> UpdateCreativeAsync(UpdateCreativeRequest request);
    Task<Result<object>> UpdateCreativeAltAsync(UpdateCreativeAltRequest request);
    Task<Result<object>> GetCreativeAsync(GetCreativeRequest request);
    Task<Result<object>> InsertCreativeAsync(InsertCreativeRequest request);
}
