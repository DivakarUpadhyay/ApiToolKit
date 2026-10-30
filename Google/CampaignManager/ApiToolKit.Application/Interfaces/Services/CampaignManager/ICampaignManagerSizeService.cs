using ApiToolKit.Application.DTOs.CampaignManager.Size;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerSizeService
{
    Task<Result<object>> ListSizesAsync(ListSizesRequest request);
    Task<Result<object>> InsertSizeAsync(InsertSizeRequest request);
    Task<Result<object>> GetSizeAsync(GetSizeRequest request);
}
