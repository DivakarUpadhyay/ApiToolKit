using ApiToolKit.Application.DTOs.Shopee.Ams;
using ApiToolKit.Application.Interfaces.Services.Shopee;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Shopee;

public class ShopeeAmsService : IShopeeAmsService
{
    private readonly IShopeeApiClient _apiClient;

    public ShopeeAmsService(IShopeeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<GetAffiliateCampaignListResponse>> GetAffiliateCampaignListAsync(GetAffiliateCampaignListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["status"] = request.Status
            };
            var r = await _apiClient.GetAsync<GetAffiliateCampaignListResponse>("/api/v2/ams/get_affiliate_campaign_list", q);
            return r == null ? Result<GetAffiliateCampaignListResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateCampaignListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateCampaignListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<CreateAffiliateCampaignResponse>> CreateAffiliateCampaignAsync(CreateAffiliateCampaignRequest request)
    {
        try
        {
            var body = new
            {
                name = request.Name,
                campaign_type = request.CampaignType,
                commission_rate = request.CommissionRate,
                start_time = request.StartTime,
                end_time = request.EndTime,
                budget = request.Budget,
                description = request.Description
            };
            var r = await _apiClient.PostAsync<CreateAffiliateCampaignResponse>("/api/v2/ams/create_affiliate_campaign", body);
            return r == null ? Result<CreateAffiliateCampaignResponse>.Fail("No response from Shopee API.") : Result<CreateAffiliateCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<CreateAffiliateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAffiliateCampaignResponse>> GetAffiliateCampaignAsync(GetAffiliateCampaignRequest request)
    {
        try
        {
            var q = new Dictionary<string, string> { ["campaign_id"] = request.CampaignId.ToString() };
            var r = await _apiClient.GetAsync<GetAffiliateCampaignResponse>("/api/v2/ams/get_affiliate_campaign", q);
            return r == null ? Result<GetAffiliateCampaignResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateAffiliateCampaignResponse>> UpdateAffiliateCampaignAsync(UpdateAffiliateCampaignRequest request)
    {
        try
        {
            var body = new
            {
                campaign_id = request.CampaignId,
                name = request.Name,
                commission_rate = request.CommissionRate,
                budget = request.Budget,
                end_time = request.EndTime,
                status = request.Status,
                description = request.Description
            };
            var r = await _apiClient.PostAsync<UpdateAffiliateCampaignResponse>("/api/v2/ams/update_affiliate_campaign", body);
            return r == null ? Result<UpdateAffiliateCampaignResponse>.Fail("No response from Shopee API.") : Result<UpdateAffiliateCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<UpdateAffiliateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<DeleteAffiliateCampaignResponse>> DeleteAffiliateCampaignAsync(DeleteAffiliateCampaignRequest request)
    {
        try
        {
            var body = new { campaign_id_list = request.CampaignIdList };
            var r = await _apiClient.PostAsync<DeleteAffiliateCampaignResponse>("/api/v2/ams/delete_affiliate_campaign", body);
            return r == null ? Result<DeleteAffiliateCampaignResponse>.Fail("No response from Shopee API.") : Result<DeleteAffiliateCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<DeleteAffiliateCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<AddItemToCampaignResponse>> AddItemToCampaignAsync(AddItemToCampaignRequest request)
    {
        try
        {
            var body = new { campaign_id = request.CampaignId, item_id_list = request.ItemIdList };
            var r = await _apiClient.PostAsync<AddItemToCampaignResponse>("/api/v2/ams/add_item_to_campaign", body);
            return r == null ? Result<AddItemToCampaignResponse>.Fail("No response from Shopee API.") : Result<AddItemToCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<AddItemToCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<RemoveItemFromCampaignResponse>> RemoveItemFromCampaignAsync(RemoveItemFromCampaignRequest request)
    {
        try
        {
            var body = new { campaign_id = request.CampaignId, item_id_list = request.ItemIdList };
            var r = await _apiClient.PostAsync<RemoveItemFromCampaignResponse>("/api/v2/ams/remove_item_from_campaign", body);
            return r == null ? Result<RemoveItemFromCampaignResponse>.Fail("No response from Shopee API.") : Result<RemoveItemFromCampaignResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<RemoveItemFromCampaignResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCampaignItemListResponse>> GetCampaignItemListAsync(GetCampaignItemListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString()
            };
            var r = await _apiClient.GetAsync<GetCampaignItemListResponse>("/api/v2/ams/get_campaign_item_list", q);
            return r == null ? Result<GetCampaignItemListResponse>.Fail("No response from Shopee API.") : Result<GetCampaignItemListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignItemListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAffiliateOrderListResponse>> GetAffiliateOrderListAsync(GetAffiliateOrderListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["start_time"] = request.StartTime.ToString(),
                ["end_time"] = request.EndTime.ToString()
            };
            var r = await _apiClient.GetAsync<GetAffiliateOrderListResponse>("/api/v2/ams/get_affiliate_order_list", q);
            return r == null ? Result<GetAffiliateOrderListResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateOrderListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateOrderListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAffiliateListResponse>> GetAffiliateListAsync(GetAffiliateListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["status"] = request.Status
            };
            var r = await _apiClient.GetAsync<GetAffiliateListResponse>("/api/v2/ams/get_affiliate_list", q);
            return r == null ? Result<GetAffiliateListResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAffiliateResponse>> GetAffiliateAsync(GetAffiliateRequest request)
    {
        try
        {
            var q = new Dictionary<string, string> { ["affiliate_id"] = request.AffiliateId.ToString() };
            var r = await _apiClient.GetAsync<GetAffiliateResponse>("/api/v2/ams/get_affiliate", q);
            return r == null ? Result<GetAffiliateResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ApproveAffiliateResponse>> ApproveAffiliateAsync(ApproveAffiliateRequest request)
    {
        try
        {
            var body = new { affiliate_id_list = request.AffiliateIdList };
            var r = await _apiClient.PostAsync<ApproveAffiliateResponse>("/api/v2/ams/approve_affiliate", body);
            return r == null ? Result<ApproveAffiliateResponse>.Fail("No response from Shopee API.") : Result<ApproveAffiliateResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<ApproveAffiliateResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<RejectAffiliateResponse>> RejectAffiliateAsync(RejectAffiliateRequest request)
    {
        try
        {
            var body = new { affiliate_id_list = request.AffiliateIdList, reason = request.Reason };
            var r = await _apiClient.PostAsync<RejectAffiliateResponse>("/api/v2/ams/reject_affiliate", body);
            return r == null ? Result<RejectAffiliateResponse>.Fail("No response from Shopee API.") : Result<RejectAffiliateResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<RejectAffiliateResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAffiliateLinkListResponse>> GetAffiliateLinkListAsync(GetAffiliateLinkListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["campaign_id"] = request.CampaignId.ToString()
            };
            var r = await _apiClient.GetAsync<GetAffiliateLinkListResponse>("/api/v2/ams/get_affiliate_link_list", q);
            return r == null ? Result<GetAffiliateLinkListResponse>.Fail("No response from Shopee API.") : Result<GetAffiliateLinkListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAffiliateLinkListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCampaignPerformanceResponse>> GetCampaignPerformanceAsync(GetCampaignPerformanceRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["campaign_id"] = request.CampaignId.ToString(),
                ["start_date"] = request.StartDate,
                ["end_date"] = request.EndDate
            };
            var r = await _apiClient.GetAsync<GetCampaignPerformanceResponse>("/api/v2/ams/get_campaign_performance", q);
            return r == null ? Result<GetCampaignPerformanceResponse>.Fail("No response from Shopee API.") : Result<GetCampaignPerformanceResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetCampaignPerformanceResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetCommissionSettlementListResponse>> GetCommissionSettlementListAsync(GetCommissionSettlementListRequest request)
    {
        try
        {
            var q = new Dictionary<string, string>
            {
                ["page_no"] = request.PageNo.ToString(),
                ["page_size"] = request.PageSize.ToString(),
                ["start_time"] = request.StartTime.ToString(),
                ["end_time"] = request.EndTime.ToString(),
                ["status"] = request.Status
            };
            var r = await _apiClient.GetAsync<GetCommissionSettlementListResponse>("/api/v2/ams/get_commission_settlement_list", q);
            return r == null ? Result<GetCommissionSettlementListResponse>.Fail("No response from Shopee API.") : Result<GetCommissionSettlementListResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetCommissionSettlementListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<GetAmsConfigResponse>> GetAmsConfigAsync(GetAmsConfigRequest request)
    {
        try
        {
            var r = await _apiClient.GetAsync<GetAmsConfigResponse>("/api/v2/ams/get_ams_config", new Dictionary<string, string>());
            return r == null ? Result<GetAmsConfigResponse>.Fail("No response from Shopee API.") : Result<GetAmsConfigResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<GetAmsConfigResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<UpdateAmsConfigResponse>> UpdateAmsConfigAsync(UpdateAmsConfigRequest request)
    {
        try
        {
            var body = new { default_commission_rate = request.DefaultCommissionRate };
            var r = await _apiClient.PostAsync<UpdateAmsConfigResponse>("/api/v2/ams/update_ams_config", body);
            return r == null ? Result<UpdateAmsConfigResponse>.Fail("No response from Shopee API.") : Result<UpdateAmsConfigResponse>.Ok(r);
        }
        catch (Exception ex)
        {
            return Result<UpdateAmsConfigResponse>.Fail(ex.Message);
        }
    }
}
