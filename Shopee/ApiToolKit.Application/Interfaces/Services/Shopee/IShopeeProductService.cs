using ApiToolKit.Application.DTOs.Shopee.Product;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeProductService
{
    Task<Result<ItemListResponse>> GetItemListAsync(GetItemListRequest request);
    Task<Result<List<ItemResponse>>> GetItemBaseInfoAsync(GetItemBaseInfoRequest request);
    Task<Result<ItemResponse>> AddItemAsync(AddItemRequest request);
    Task<Result<ItemResponse>> UpdateItemAsync(UpdateItemRequest request);
    Task<Result<bool>> UpdateItemPriceAsync(UpdatePriceRequest request);
    Task<Result<bool>> UpdateItemStockAsync(UpdateStockRequest request);
    Task<Result<bool>> DeleteItemAsync(DeleteItemRequest request);
    Task<Result<object>> GetCategoryAsync(GetCategoryRequest request);
    Task<Result<object>> GetAttributesAsync(GetAttributesRequest request);
    Task<Result<object>> GetBrandsAsync(long shopId, long categoryId, string language);
    Task<Result<object>> GetModelListAsync(long shopId, long itemId);
    Task<Result<bool>> InitTierVariationAsync(long shopId, long itemId, object tierVariation);
}
