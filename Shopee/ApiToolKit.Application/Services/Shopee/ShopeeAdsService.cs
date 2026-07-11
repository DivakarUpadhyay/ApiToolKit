using ApiToolKit.Application.DTOs.Shopee.Ads;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeAdsService : IShopeeAdsService
{
    private readonly IShopeeApiClient _client;

    public ShopeeAdsService(IShopeeApiClient client)
    {
        _client = client;
    }

    public async Task<Result<GetRecommendKeywordResponse>> GetRecommendKeywordAsync(GetRecommendKeywordRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["item_id"] = request.ItemId.ToString() };
            var response = await _client.GetAsync<GetRecommendKeywordResponse>("/api/v2/ads/get_recommend_keyword", queryParams);
            if (response == null) return Result<GetRecommendKeywordResponse>.Fail("No response from Shopee API.");
            return Result<GetRecommendKeywordResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetRecommendKeywordResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CreateCampaignResponse>> CreateCampaignAsync(CreateCampaignRequest request)
    {
        try
        {
            var body = new
            {
                name = request.Name,
                campaign_type = request.CampaignType,
                budget_type = request.BudgetType,
                budget = request.Budget,
                start_time = request.StartTime,
                end_time = request.EndTime
            };
            var response = await _client.PostAsync<CreateCampaignResponse>("/api/v2/ads/create_campaign", body);
            if (response == null) return Result<CreateCampaignResponse>.Fail("No response from Shopee API.");
            return Result<CreateCampaignResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CreateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCampaignListResponse>> GetCampaignListAsync(GetCampaignListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["status"] = request.Status
            };
            var response = await _client.GetAsync<GetCampaignListResponse>("/api/v2/ads/get_campaign_list", queryParams);
            if (response == null) return Result<GetCampaignListResponse>.Fail("No response from Shopee API.");
            return Result<GetCampaignListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCampaignResponse>> GetCampaignAsync(GetCampaignRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["campaign_id"] = request.CampaignId.ToString() };
            var response = await _client.GetAsync<GetCampaignResponse>("/api/v2/ads/get_campaign", queryParams);
            if (response == null) return Result<GetCampaignResponse>.Fail("No response from Shopee API.");
            return Result<GetCampaignResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateCampaignResponse>> UpdateCampaignAsync(UpdateCampaignRequest request)
    {
        try
        {
            var body = new
            {
                campaign_id = request.CampaignId,
                name = request.Name,
                budget = request.Budget,
                start_time = request.StartTime,
                end_time = request.EndTime,
                status = request.Status
            };
            var response = await _client.PostAsync<UpdateCampaignResponse>("/api/v2/ads/update_campaign", body);
            if (response == null) return Result<UpdateCampaignResponse>.Fail("No response from Shopee API.");
            return Result<UpdateCampaignResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteCampaignResponse>> DeleteCampaignAsync(DeleteCampaignRequest request)
    {
        try
        {
            var body = new { campaign_id_list = request.CampaignIdList };
            var response = await _client.PostAsync<DeleteCampaignResponse>("/api/v2/ads/delete_campaign", body);
            if (response == null) return Result<DeleteCampaignResponse>.Fail("No response from Shopee API.");
            return Result<DeleteCampaignResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CreateAdGroupResponse>> CreateAdGroupAsync(CreateAdGroupRequest request)
    {
        try
        {
            var body = new
            {
                campaign_id = request.CampaignId,
                name = request.Name,
                bid_type = request.BidType,
                bid = request.Bid,
                status = request.Status
            };
            var response = await _client.PostAsync<CreateAdGroupResponse>("/api/v2/ads/create_adgroup", body);
            if (response == null) return Result<CreateAdGroupResponse>.Fail("No response from Shopee API.");
            return Result<CreateAdGroupResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CreateAdGroupResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAdGroupListResponse>> GetAdGroupListAsync(GetAdGroupListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetAdGroupListResponse>("/api/v2/ads/get_adgroup_list", queryParams);
            if (response == null) return Result<GetAdGroupListResponse>.Fail("No response from Shopee API.");
            return Result<GetAdGroupListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetAdGroupListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateAdGroupResponse>> UpdateAdGroupAsync(UpdateAdGroupRequest request)
    {
        try
        {
            var body = new
            {
                adgroup_id = request.AdgroupId,
                name = request.Name,
                bid = request.Bid,
                status = request.Status
            };
            var response = await _client.PostAsync<UpdateAdGroupResponse>("/api/v2/ads/update_adgroup", body);
            if (response == null) return Result<UpdateAdGroupResponse>.Fail("No response from Shopee API.");
            return Result<UpdateAdGroupResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateAdGroupResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CreateAdResponse>> CreateAdAsync(CreateAdRequest request)
    {
        try
        {
            var body = new
            {
                adgroup_id = request.AdgroupId,
                item_id = request.ItemId,
                model_id = request.ModelId,
                bid = request.Bid
            };
            var response = await _client.PostAsync<CreateAdResponse>("/api/v2/ads/create_ad", body);
            if (response == null) return Result<CreateAdResponse>.Fail("No response from Shopee API.");
            return Result<CreateAdResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<CreateAdResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAdListResponse>> GetAdListAsync(GetAdListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["adgroup_id"] = request.AdgroupId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetAdListResponse>("/api/v2/ads/get_ad_list", queryParams);
            if (response == null) return Result<GetAdListResponse>.Fail("No response from Shopee API.");
            return Result<GetAdListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetAdListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateAdResponse>> UpdateAdAsync(UpdateAdRequest request)
    {
        try
        {
            var body = new { ad_id = request.AdId, bid = request.Bid, status = request.Status };
            var response = await _client.PostAsync<UpdateAdResponse>("/api/v2/ads/update_ad", body);
            if (response == null) return Result<UpdateAdResponse>.Fail("No response from Shopee API.");
            return Result<UpdateAdResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateAdResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAdDailyPerformanceResponse>> GetAdDailyPerformanceAsync(GetAdDailyPerformanceRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["ad_id"] = request.AdId.ToString(),
                ["start_date"] = request.StartDate,
                ["end_date"] = request.EndDate
            };
            var response = await _client.GetAsync<GetAdDailyPerformanceResponse>("/api/v2/ads/get_ad_daily_performance", queryParams);
            if (response == null) return Result<GetAdDailyPerformanceResponse>.Fail("No response from Shopee API.");
            return Result<GetAdDailyPerformanceResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetAdDailyPerformanceResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCampaignDailyPerformanceResponse>> GetCampaignDailyPerformanceAsync(GetCampaignDailyPerformanceRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["start_date"] = request.StartDate,
                ["end_date"] = request.EndDate
            };
            var response = await _client.GetAsync<GetCampaignDailyPerformanceResponse>("/api/v2/ads/get_campaign_daily_performance", queryParams);
            if (response == null) return Result<GetCampaignDailyPerformanceResponse>.Fail("No response from Shopee API.");
            return Result<GetCampaignDailyPerformanceResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignDailyPerformanceResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddKeywordResponse>> AddKeywordAsync(AddKeywordRequest request)
    {
        try
        {
            var body = new { adgroup_id = request.AdgroupId, keyword_list = request.KeywordList };
            var response = await _client.PostAsync<AddKeywordResponse>("/api/v2/ads/add_keyword", body);
            if (response == null) return Result<AddKeywordResponse>.Fail("No response from Shopee API.");
            return Result<AddKeywordResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<AddKeywordResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetKeywordListResponse>> GetKeywordListAsync(GetKeywordListRequest request)
    {
        try
        {
            var queryParams = new Dictionary<string, string>
            {
                ["adgroup_id"] = request.AdgroupId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var response = await _client.GetAsync<GetKeywordListResponse>("/api/v2/ads/get_keyword_list", queryParams);
            if (response == null) return Result<GetKeywordListResponse>.Fail("No response from Shopee API.");
            return Result<GetKeywordListResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<GetKeywordListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateKeywordResponse>> UpdateKeywordAsync(UpdateKeywordRequest request)
    {
        try
        {
            var body = new { adgroup_id = request.AdgroupId, keyword_list = request.KeywordList };
            var response = await _client.PostAsync<UpdateKeywordResponse>("/api/v2/ads/update_keyword", body);
            if (response == null) return Result<UpdateKeywordResponse>.Fail("No response from Shopee API.");
            return Result<UpdateKeywordResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<UpdateKeywordResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteKeywordResponse>> DeleteKeywordAsync(DeleteKeywordRequest request)
    {
        try
        {
            var body = new { adgroup_id = request.AdgroupId, keyword_list = request.KeywordList };
            var response = await _client.PostAsync<DeleteKeywordResponse>("/api/v2/ads/delete_keyword", body);
            if (response == null) return Result<DeleteKeywordResponse>.Fail("No response from Shopee API.");
            return Result<DeleteKeywordResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            return Result<DeleteKeywordResponse>.Fail(ex.Message);
        }
    }
}
