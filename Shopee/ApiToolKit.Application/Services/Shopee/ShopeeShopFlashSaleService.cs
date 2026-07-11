using ApiToolKit.Application.DTOs.Shopee.ShopFlashSale;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeShopFlashSaleService : IShopeeShopFlashSaleService
{
    private readonly IShopeeApiClient _client;

    public ShopeeShopFlashSaleService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<GetTimeSlotIdResponse>> GetTimeSlotIdAsync(GetTimeSlotIdRequest request)
    {
        try
        {
            var body = new { start_time = request.StartTime, end_time = request.EndTime };
            var response = await _client.PostAsync<GetTimeSlotIdResponse>("/api/v2/shop_flash_sale/get_time_slot_id", body);
            if (response == null) return Result<GetTimeSlotIdResponse>.Fail("No response from Shopee API.");
            return Result<GetTimeSlotIdResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetTimeSlotIdResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CreateFlashSaleResponse>> CreateFlashSaleAsync(CreateFlashSaleRequest request)
    {
        try
        {
            var body = new { timeslot_id = request.TimeslotId };
            var response = await _client.PostAsync<CreateFlashSaleResponse>("/api/v2/shop_flash_sale/create_flash_sale", body);
            if (response == null) return Result<CreateFlashSaleResponse>.Fail("No response from Shopee API.");
            return Result<CreateFlashSaleResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CreateFlashSaleResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddFlashSaleItemsResponse>> AddFlashSaleItemsAsync(AddFlashSaleItemsRequest request)
    {
        try
        {
            var body = new { flash_sale_id = request.FlashSaleId, items = request.Items };
            var response = await _client.PostAsync<AddFlashSaleItemsResponse>("/api/v2/shop_flash_sale/add_flash_sale_items", body);
            if (response == null) return Result<AddFlashSaleItemsResponse>.Fail("No response from Shopee API.");
            return Result<AddFlashSaleItemsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddFlashSaleItemsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateFlashSaleResponse>> UpdateFlashSaleAsync(UpdateFlashSaleRequest request)
    {
        try
        {
            var body = new { flash_sale_id = request.FlashSaleId, status = request.Status };
            var response = await _client.PostAsync<UpdateFlashSaleResponse>("/api/v2/shop_flash_sale/update_flash_sale", body);
            if (response == null) return Result<UpdateFlashSaleResponse>.Fail("No response from Shopee API.");
            return Result<UpdateFlashSaleResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateFlashSaleResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteFlashSaleResponse>> DeleteFlashSaleAsync(DeleteFlashSaleRequest request)
    {
        try
        {
            var body = new { flash_sale_id = request.FlashSaleId };
            var response = await _client.PostAsync<DeleteFlashSaleResponse>("/api/v2/shop_flash_sale/delete_flash_sale", body);
            if (response == null) return Result<DeleteFlashSaleResponse>.Fail("No response from Shopee API.");
            return Result<DeleteFlashSaleResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteFlashSaleResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetFlashSaleListResponse>> GetFlashSaleListAsync(GetFlashSaleListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["filter"] = request.Filter
            };
            var response = await _client.GetAsync<GetFlashSaleListResponse>("/api/v2/shop_flash_sale/get_flash_sale_list", queryParams);
            if (response == null) return Result<GetFlashSaleListResponse>.Fail("No response from Shopee API.");
            return Result<GetFlashSaleListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetFlashSaleListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetFlashSaleResponse>> GetFlashSaleAsync(GetFlashSaleRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["flash_sale_id"] = request.FlashSaleId.ToString() };
            var response = await _client.GetAsync<GetFlashSaleResponse>("/api/v2/shop_flash_sale/get_flash_sale", queryParams);
            if (response == null) return Result<GetFlashSaleResponse>.Fail("No response from Shopee API.");
            return Result<GetFlashSaleResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetFlashSaleResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateFlashSaleItemsResponse>> UpdateFlashSaleItemsAsync(UpdateFlashSaleItemsRequest request)
    {
        try
        {
            var body = new { flash_sale_id = request.FlashSaleId, items = request.Items };
            var response = await _client.PostAsync<UpdateFlashSaleItemsResponse>("/api/v2/shop_flash_sale/update_flash_sale_items", body);
            if (response == null) return Result<UpdateFlashSaleItemsResponse>.Fail("No response from Shopee API.");
            return Result<UpdateFlashSaleItemsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateFlashSaleItemsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteFlashSaleItemsResponse>> DeleteFlashSaleItemsAsync(DeleteFlashSaleItemsRequest request)
    {
        try
        {
            var body = new { flash_sale_id = request.FlashSaleId, item_id_list = request.ItemIdList };
            var response = await _client.PostAsync<DeleteFlashSaleItemsResponse>("/api/v2/shop_flash_sale/delete_flash_sale_items", body);
            if (response == null) return Result<DeleteFlashSaleItemsResponse>.Fail("No response from Shopee API.");
            return Result<DeleteFlashSaleItemsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteFlashSaleItemsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetFlashSaleItemsResponse>> GetFlashSaleItemsAsync(GetFlashSaleItemsRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["flash_sale_id"] = request.FlashSaleId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetFlashSaleItemsResponse>("/api/v2/shop_flash_sale/get_flash_sale_items", queryParams);
            if (response == null) return Result<GetFlashSaleItemsResponse>.Fail("No response from Shopee API.");
            return Result<GetFlashSaleItemsResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetFlashSaleItemsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetItemCriteriaResponse>> GetItemCriteriaAsync(GetItemCriteriaRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["item_id_list"] = string.Join(",", request.ItemIdList) };
            var response = await _client.GetAsync<GetItemCriteriaResponse>("/api/v2/shop_flash_sale/get_item_criteria", queryParams);
            if (response == null) return Result<GetItemCriteriaResponse>.Fail("No response from Shopee API.");
            return Result<GetItemCriteriaResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetItemCriteriaResponse>.Fail(ex.Message);
        }
    }
}
