using ApiToolKit.Application.DTOs.Shopee.BundleDeal;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopee;
public interface IShopeeBundleDealService
{
    Task<Result<BundleDealResponse>> AddBundleDealAsync(AddBundleDealRequest request);
    Task<Result<BundleDealResponse>> UpdateBundleDealAsync(UpdateBundleDealRequest request);
    Task<Result<bool>> DeleteBundleDealAsync(DeleteBundleDealRequest request);
    Task<Result<bool>> EndBundleDealAsync(EndBundleDealRequest request);
    Task<Result<BundleDealResponse>> GetBundleDealAsync(GetBundleDealRequest request);
    Task<Result<BundleDealListResponse>> GetBundleDealListAsync(GetBundleDealListRequest request);
    Task<Result<object>> GetBundleDealItemAsync(long shopId, long bundleDealId);
    Task<Result<bool>> AddBundleDealItemAsync(AddBundleDealItemRequest request);
    Task<Result<bool>> DeleteBundleDealItemAsync(DeleteBundleDealItemRequest request);
    Task<Result<bool>> UpdateBundleDealItemAsync(UpdateBundleDealItemRequest request);
}
