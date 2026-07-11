using ApiToolKit.Application.DTOs.Shopee.GlobalProduct;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeGlobalProductService : IShopeeGlobalProductService
{
    private readonly IShopeeApiClient _client;

    public ShopeeGlobalProductService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<GetGlobalCategoryListResponse>> GetGlobalCategoryListAsync(GetGlobalCategoryListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["language"] = request.Language };
            var response = await _client.GetAsync<GetGlobalCategoryListResponse>("/api/v2/global_product/get_category", queryParams);
            if (response == null) return Result<GetGlobalCategoryListResponse>.Fail("No response from Shopee API.");
            return Result<GetGlobalCategoryListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetGlobalCategoryListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCategoryAttributeResponse>> GetCategoryAttributeAsync(GetCategoryAttributeRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["category_id"] = request.CategoryId.ToString(),
                ["language"] = request.Language
            };
            var response = await _client.GetAsync<GetCategoryAttributeResponse>("/api/v2/global_product/get_attributes", queryParams);
            if (response == null) return Result<GetCategoryAttributeResponse>.Fail("No response from Shopee API.");
            return Result<GetCategoryAttributeResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetCategoryAttributeResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetBrandListResponse>> GetBrandListAsync(GetBrandListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["category_id"] = request.CategoryId.ToString(),
                ["offset"] = request.Offset.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["status"] = request.Status.ToString()
            };
            var response = await _client.GetAsync<GetBrandListResponse>("/api/v2/global_product/get_brand_list", queryParams);
            if (response == null) return Result<GetBrandListResponse>.Fail("No response from Shopee API.");
            return Result<GetBrandListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetBrandListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddGlobalItemResponse>> AddGlobalItemAsync(AddGlobalItemRequest request)
    {
        try
        {
            var body = new
            {
                original_name = request.OriginalName,
                description = request.Description,
                original_sku = request.OriginalSku,
                price = request.Price,
                stock = request.Stock,
                image = request.Image,
                category_id = request.CategoryId,
                condition = request.Condition,
                item_dangerous = request.ItemDangerous,
                logistic_info = request.LogisticInfo,
                attribute_list = request.AttributeList
            };
            var response = await _client.PostAsync<AddGlobalItemResponse>("/api/v2/global_product/add_item", body);
            if (response == null) return Result<AddGlobalItemResponse>.Fail("No response from Shopee API.");
            return Result<AddGlobalItemResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddGlobalItemResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CopyItemResponse>> CopyItemAsync(CopyItemRequest request)
    {
        try
        {
            var body = new { item_id = request.ItemId, shop_id_list = request.ShopIdList };
            var response = await _client.PostAsync<CopyItemResponse>("/api/v2/global_product/copy_item", body);
            if (response == null) return Result<CopyItemResponse>.Fail("No response from Shopee API.");
            return Result<CopyItemResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CopyItemResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteGlobalItemResponse>> DeleteGlobalItemAsync(DeleteGlobalItemRequest request)
    {
        try
        {
            var body = new { global_item_id_list = request.GlobalItemIdList };
            var response = await _client.PostAsync<DeleteGlobalItemResponse>("/api/v2/global_product/delete_item", body);
            if (response == null) return Result<DeleteGlobalItemResponse>.Fail("No response from Shopee API.");
            return Result<DeleteGlobalItemResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteGlobalItemResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetGlobalItemListResponse>> GetGlobalItemListAsync(GetGlobalItemListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["offset"] = request.Offset.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["item_status"] = request.ItemStatus
            };
            var response = await _client.GetAsync<GetGlobalItemListResponse>("/api/v2/global_product/get_item_list", queryParams);
            if (response == null) return Result<GetGlobalItemListResponse>.Fail("No response from Shopee API.");
            return Result<GetGlobalItemListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetGlobalItemListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetGlobalItemDetailResponse>> GetGlobalItemDetailAsync(GetGlobalItemDetailRequest request)
    {
        try
        {
            var body = new { global_item_id_list = request.GlobalItemIdList };
            var response = await _client.PostAsync<GetGlobalItemDetailResponse>("/api/v2/global_product/get_item_detail", body);
            if (response == null) return Result<GetGlobalItemDetailResponse>.Fail("No response from Shopee API.");
            return Result<GetGlobalItemDetailResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetGlobalItemDetailResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateGlobalItemResponse>> UpdateGlobalItemAsync(UpdateGlobalItemRequest request)
    {
        try
        {
            var body = new
            {
                global_item_id = request.GlobalItemId,
                original_name = request.OriginalName,
                description = request.Description,
                price = request.Price,
                stock = request.Stock,
                image = request.Image
            };
            var response = await _client.PostAsync<UpdateGlobalItemResponse>("/api/v2/global_product/update_item", body);
            if (response == null) return Result<UpdateGlobalItemResponse>.Fail("No response from Shopee API.");
            return Result<UpdateGlobalItemResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateGlobalItemResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<SearchGlobalItemResponse>> SearchGlobalItemAsync(SearchGlobalItemRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["keyword"] = request.Keyword,
                ["category_id"] = request.CategoryId.ToString(),
                ["offset"] = request.Offset.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<SearchGlobalItemResponse>("/api/v2/global_product/search_item", queryParams);
            if (response == null) return Result<SearchGlobalItemResponse>.Fail("No response from Shopee API.");
            return Result<SearchGlobalItemResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<SearchGlobalItemResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<InitTierVariationResponse>> InitTierVariationAsync(InitTierVariationRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, tier_variation = request.TierVariation };
            var response = await _client.PostAsync<InitTierVariationResponse>("/api/v2/global_product/init_tier_variation", body);
            if (response == null) return Result<InitTierVariationResponse>.Fail("No response from Shopee API.");
            return Result<InitTierVariationResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<InitTierVariationResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateTierVariationResponse>> UpdateTierVariationAsync(UpdateTierVariationRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, tier_variation = request.TierVariation };
            var response = await _client.PostAsync<UpdateTierVariationResponse>("/api/v2/global_product/update_tier_variation", body);
            if (response == null) return Result<UpdateTierVariationResponse>.Fail("No response from Shopee API.");
            return Result<UpdateTierVariationResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateTierVariationResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetModelListResponse>> GetModelListAsync(GetModelListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["global_item_id"] = request.GlobalItemId.ToString() };
            var response = await _client.GetAsync<GetModelListResponse>("/api/v2/global_product/get_model_list", queryParams);
            if (response == null) return Result<GetModelListResponse>.Fail("No response from Shopee API.");
            return Result<GetModelListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetModelListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateModelResponse>> UpdateModelAsync(UpdateModelRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, global_model_list = request.GlobalModelList };
            var response = await _client.PostAsync<UpdateModelResponse>("/api/v2/global_product/update_model", body);
            if (response == null) return Result<UpdateModelResponse>.Fail("No response from Shopee API.");
            return Result<UpdateModelResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateModelResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddGlobalModelResponse>> AddGlobalModelAsync(AddGlobalModelRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, global_model_list = request.GlobalModelList };
            var response = await _client.PostAsync<AddGlobalModelResponse>("/api/v2/global_product/add_model", body);
            if (response == null) return Result<AddGlobalModelResponse>.Fail("No response from Shopee API.");
            return Result<AddGlobalModelResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddGlobalModelResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteGlobalModelResponse>> DeleteGlobalModelAsync(DeleteGlobalModelRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, global_model_id_list = request.GlobalModelIdList };
            var response = await _client.PostAsync<DeleteGlobalModelResponse>("/api/v2/global_product/delete_model", body);
            if (response == null) return Result<DeleteGlobalModelResponse>.Fail("No response from Shopee API.");
            return Result<DeleteGlobalModelResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteGlobalModelResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetGlobalItemInfoResponse>> GetGlobalItemInfoAsync(GetGlobalItemInfoRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["global_item_id"] = request.GlobalItemId.ToString() };
            var response = await _client.GetAsync<GetGlobalItemInfoResponse>("/api/v2/global_product/get_item_info", queryParams);
            if (response == null) return Result<GetGlobalItemInfoResponse>.Fail("No response from Shopee API.");
            return Result<GetGlobalItemInfoResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetGlobalItemInfoResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateGlobalItemStatusResponse>> UpdateGlobalItemStatusAsync(UpdateGlobalItemStatusRequest request)
    {
        try
        {
            var body = new { global_item_id_list = request.GlobalItemIdList, status = request.Status };
            var response = await _client.PostAsync<UpdateGlobalItemStatusResponse>("/api/v2/global_product/update_item_status", body);
            if (response == null) return Result<UpdateGlobalItemStatusResponse>.Fail("No response from Shopee API.");
            return Result<UpdateGlobalItemStatusResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateGlobalItemStatusResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetShopItemInfoResponse>> GetShopItemInfoAsync(GetShopItemInfoRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["global_item_id"] = request.GlobalItemId.ToString(),
                ["shop_id"] = request.ShopId.ToString()
            };
            var response = await _client.GetAsync<GetShopItemInfoResponse>("/api/v2/global_product/get_shop_item_info", queryParams);
            if (response == null) return Result<GetShopItemInfoResponse>.Fail("No response from Shopee API.");
            return Result<GetShopItemInfoResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetShopItemInfoResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetChannelListResponse>> GetChannelListAsync(GetChannelListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["global_item_id"] = request.GlobalItemId.ToString() };
            var response = await _client.GetAsync<GetChannelListResponse>("/api/v2/global_product/get_channel_list", queryParams);
            if (response == null) return Result<GetChannelListResponse>.Fail("No response from Shopee API.");
            return Result<GetChannelListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetChannelListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateGlobalItemLogisticResponse>> UpdateGlobalItemLogisticAsync(UpdateGlobalItemLogisticRequest request)
    {
        try
        {
            var body = new { global_item_id = request.GlobalItemId, logistic_info = request.LogisticInfo };
            var response = await _client.PostAsync<UpdateGlobalItemLogisticResponse>("/api/v2/global_product/update_item_logistic", body);
            if (response == null) return Result<UpdateGlobalItemLogisticResponse>.Fail("No response from Shopee API.");
            return Result<UpdateGlobalItemLogisticResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateGlobalItemLogisticResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BatchUpdatePriceResponse>> BatchUpdatePriceAsync(BatchUpdatePriceRequest request)
    {
        try
        {
            var body = new { global_item_id_list = request.GlobalItemIdList, price = request.Price };
            var response = await _client.PostAsync<BatchUpdatePriceResponse>("/api/v2/global_product/batch_update_price", body);
            if (response == null) return Result<BatchUpdatePriceResponse>.Fail("No response from Shopee API.");
            return Result<BatchUpdatePriceResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<BatchUpdatePriceResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<BatchUpdateStockResponse>> BatchUpdateStockAsync(BatchUpdateStockRequest request)
    {
        try
        {
            var body = new { global_item_id_list = request.GlobalItemIdList, stock = request.Stock };
            var response = await _client.PostAsync<BatchUpdateStockResponse>("/api/v2/global_product/batch_update_stock", body);
            if (response == null) return Result<BatchUpdateStockResponse>.Fail("No response from Shopee API.");
            return Result<BatchUpdateStockResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<BatchUpdateStockResponse>.Fail(ex.Message);
        }
    }
}
