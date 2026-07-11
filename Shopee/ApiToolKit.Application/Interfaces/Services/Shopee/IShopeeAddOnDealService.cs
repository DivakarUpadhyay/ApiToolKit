using ApiToolKit.Application.DTOs.Shopee.AddOnDeal;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopee;
public interface IShopeeAddOnDealService
{
    Task<Result<AddOnDealResponse>> AddAddOnDealAsync(AddAddOnDealRequest request);
    Task<Result<AddOnDealResponse>> UpdateAddOnDealAsync(UpdateAddOnDealRequest request);
    Task<Result<bool>> DeleteAddOnDealAsync(DeleteAddOnDealRequest request);
    Task<Result<bool>> EndAddOnDealAsync(EndAddOnDealRequest request);
    Task<Result<AddOnDealResponse>> GetAddOnDealAsync(GetAddOnDealRequest request);
    Task<Result<AddOnDealListResponse>> GetAddOnDealListAsync(GetAddOnDealListRequest request);
    Task<Result<bool>> AddAddOnDealMainItemAsync(AddOnDealMainItemRequest request);
    Task<Result<bool>> DeleteAddOnDealMainItemAsync(DeleteAddOnItemRequest request);
    Task<Result<bool>> UpdateAddOnDealMainItemAsync(AddOnDealMainItemRequest request);
    Task<Result<object>> GetAddOnDealMainItemAsync(long shopId, long addOnDealId);
    Task<Result<bool>> AddAddOnDealSubItemAsync(AddOnDealSubItemRequest request);
    Task<Result<bool>> DeleteAddOnDealSubItemAsync(DeleteAddOnItemRequest request);
    Task<Result<bool>> UpdateAddOnDealSubItemAsync(UpdateAddOnSubItemRequest request);
    Task<Result<object>> GetAddOnDealSubItemAsync(long shopId, long addOnDealId);
}
