using ApiToolKit.Application.DTOs.Shopee.ShopCategory;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeShopCategoryService
{
    Task<Result<AddShopCategoryResponse>> AddShopCategoryAsync(AddShopCategoryRequest request);
    Task<Result<GetShopCategoryListResponse>> GetShopCategoryListAsync(GetShopCategoryListRequest request);
    Task<Result<DeleteShopCategoryResponse>> DeleteShopCategoryAsync(DeleteShopCategoryRequest request);
    Task<Result<UpdateShopCategoryResponse>> UpdateShopCategoryAsync(UpdateShopCategoryRequest request);
    Task<Result<AddItemsOfShopCategoryResponse>> AddItemsOfShopCategoryAsync(AddItemsOfShopCategoryRequest request);
    Task<Result<GetItemsOfShopCategoryResponse>> GetItemsOfShopCategoryAsync(GetItemsOfShopCategoryRequest request);
    Task<Result<DeleteItemsOfShopCategoryResponse>> DeleteItemsOfShopCategoryAsync(DeleteItemsOfShopCategoryRequest request);
}
