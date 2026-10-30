using ApiToolKit.Application.DTOs.Merchant.Region;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantRegionService
{
    Task<Result<object>> DeleteRegionAsync(DeleteRegionRequest request);
    Task<Result<object>> BatchCreateRegionsAsync(BatchCreateRegionsRequest request);
    Task<Result<object>> UpdateRegionAsync(UpdateRegionRequest request);
    Task<Result<object>> ListRegionsAsync(ListRegionsRequest request);
    Task<Result<object>> BatchUpdateRegionsAsync(BatchUpdateRegionsRequest request);
    Task<Result<object>> BatchDeleteRegionsAsync(BatchDeleteRegionsRequest request);
    Task<Result<object>> GetRegionAsync(GetRegionRequest request);
    Task<Result<object>> CreateRegionAsync(CreateRegionRequest request);
}
