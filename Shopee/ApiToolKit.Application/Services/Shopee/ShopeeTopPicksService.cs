using ApiToolKit.Application.DTOs.Shopee.TopPicks;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeTopPicksService : IShopeeTopPicksService
{
    private readonly IShopeeApiClient _client;

    public ShopeeTopPicksService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<GetTopPicksListResponse>> GetTopPicksListAsync(GetTopPicksListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetTopPicksListResponse>("/api/v2/top_picks/get_top_picks_list", queryParams);
            if (response == null) return Result<GetTopPicksListResponse>.Fail("No response from Shopee API.");
            return Result<GetTopPicksListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetTopPicksListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddTopPicksResponse>> AddTopPicksAsync(AddTopPicksRequest request)
    {
        try
        {
            var body = new { name = request.Name, item_id_list = request.ItemIdList };
            var response = await _client.PostAsync<AddTopPicksResponse>("/api/v2/top_picks/add_top_picks", body);
            if (response == null) return Result<AddTopPicksResponse>.Fail("No response from Shopee API.");
            return Result<AddTopPicksResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddTopPicksResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateTopPicksResponse>> UpdateTopPicksAsync(UpdateTopPicksRequest request)
    {
        try
        {
            var body = new { top_picks_id = request.TopPicksId, name = request.Name, item_id_list = request.ItemIdList, is_activated = request.IsActivated };
            var response = await _client.PostAsync<UpdateTopPicksResponse>("/api/v2/top_picks/update_top_picks", body);
            if (response == null) return Result<UpdateTopPicksResponse>.Fail("No response from Shopee API.");
            return Result<UpdateTopPicksResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateTopPicksResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteTopPicksResponse>> DeleteTopPicksAsync(DeleteTopPicksRequest request)
    {
        try
        {
            var body = new { top_picks_id = request.TopPicksId };
            var response = await _client.PostAsync<DeleteTopPicksResponse>("/api/v2/top_picks/delete_top_picks", body);
            if (response == null) return Result<DeleteTopPicksResponse>.Fail("No response from Shopee API.");
            return Result<DeleteTopPicksResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteTopPicksResponse>.Fail(ex.Message);
        }
    }
}
