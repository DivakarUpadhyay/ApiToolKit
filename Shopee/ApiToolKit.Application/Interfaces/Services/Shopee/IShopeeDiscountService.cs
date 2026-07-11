using ApiToolKit.Application.DTOs.Shopee.Discount;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeDiscountService
{
    Task<Result<DiscountResponse>> AddDiscountAsync(AddDiscountRequest request);
    Task<Result<bool>> DeleteDiscountAsync(DeleteDiscountRequest request);
    Task<Result<DiscountResponse>> UpdateDiscountAsync(UpdateDiscountRequest request);
    Task<Result<bool>> EndDiscountAsync(EndDiscountRequest request);
    Task<Result<DiscountResponse>> GetDiscountAsync(GetDiscountRequest request);
    Task<Result<DiscountListResponse>> GetDiscountListAsync(GetDiscountListRequest request);
    Task<Result<bool>> AddDiscountItemAsync(AddDiscountItemRequest request);
    Task<Result<bool>> DeleteDiscountItemAsync(DeleteDiscountItemRequest request);
    Task<Result<bool>> UpdateDiscountItemAsync(UpdateDiscountItemRequest request);
}
