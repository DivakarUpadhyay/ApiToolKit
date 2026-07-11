using ApiToolKit.Application.DTOs.Shopee.ShopCategory;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeShopCategoryService : IShopeeShopCategoryService
{
    private readonly IShopeeApiClient _client;

    public ShopeeShopCategoryService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<AddShopCategoryResponse>> AddShopCategoryAsync(AddShopCategoryRequest request)
    {
        try
        {
            var body = new { name = request.Name, sort_weight = request.SortWeight };
            var response = await _client.PostAsync<AddShopCategoryResponse>("/api/v2/shop_category/add_shop_category", body);
            if (response == null) return Result<AddShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<AddShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddShopCategoryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetShopCategoryListResponse>> GetShopCategoryListAsync(GetShopCategoryListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetShopCategoryListResponse>("/api/v2/shop_category/get_shop_category_list", queryParams);
            if (response == null) return Result<GetShopCategoryListResponse>.Fail("No response from Shopee API.");
            return Result<GetShopCategoryListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetShopCategoryListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteShopCategoryResponse>> DeleteShopCategoryAsync(DeleteShopCategoryRequest request)
    {
        try
        {
            var body = new { shop_category_id = request.ShopCategoryId };
            var response = await _client.PostAsync<DeleteShopCategoryResponse>("/api/v2/shop_category/delete_shop_category", body);
            if (response == null) return Result<DeleteShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<DeleteShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteShopCategoryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateShopCategoryResponse>> UpdateShopCategoryAsync(UpdateShopCategoryRequest request)
    {
        try
        {
            var body = new { shop_category_id = request.ShopCategoryId, name = request.Name, sort_weight = request.SortWeight, status = request.Status };
            var response = await _client.PostAsync<UpdateShopCategoryResponse>("/api/v2/shop_category/update_shop_category", body);
            if (response == null) return Result<UpdateShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<UpdateShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateShopCategoryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddItemsOfShopCategoryResponse>> AddItemsOfShopCategoryAsync(AddItemsOfShopCategoryRequest request)
    {
        try
        {
            var body = new { shop_category_id = request.ShopCategoryId, item_id_list = request.ItemIdList };
            var response = await _client.PostAsync<AddItemsOfShopCategoryResponse>("/api/v2/shop_category/add_items_of_shop_category", body);
            if (response == null) return Result<AddItemsOfShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<AddItemsOfShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddItemsOfShopCategoryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetItemsOfShopCategoryResponse>> GetItemsOfShopCategoryAsync(GetItemsOfShopCategoryRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["shop_category_id"] = request.ShopCategoryId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetItemsOfShopCategoryResponse>("/api/v2/shop_category/get_items_of_shop_category", queryParams);
            if (response == null) return Result<GetItemsOfShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<GetItemsOfShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetItemsOfShopCategoryResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteItemsOfShopCategoryResponse>> DeleteItemsOfShopCategoryAsync(DeleteItemsOfShopCategoryRequest request)
    {
        try
        {
            var body = new { shop_category_id = request.ShopCategoryId, item_id_list = request.ItemIdList };
            var response = await _client.PostAsync<DeleteItemsOfShopCategoryResponse>("/api/v2/shop_category/delete_items_of_shop_category", body);
            if (response == null) return Result<DeleteItemsOfShopCategoryResponse>.Fail("No response from Shopee API.");
            return Result<DeleteItemsOfShopCategoryResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteItemsOfShopCategoryResponse>.Fail(ex.Message);
        }
    }
}
