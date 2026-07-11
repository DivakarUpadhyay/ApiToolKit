using ApiToolKit.Application.DTOs.Shopee.Ams;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeAmsService
{
    Task<Result<GetAffiliateCampaignListResponse>> GetAffiliateCampaignListAsync(GetAffiliateCampaignListRequest request);
    Task<Result<CreateAffiliateCampaignResponse>> CreateAffiliateCampaignAsync(CreateAffiliateCampaignRequest request);
    Task<Result<GetAffiliateCampaignResponse>> GetAffiliateCampaignAsync(GetAffiliateCampaignRequest request);
    Task<Result<UpdateAffiliateCampaignResponse>> UpdateAffiliateCampaignAsync(UpdateAffiliateCampaignRequest request);
    Task<Result<DeleteAffiliateCampaignResponse>> DeleteAffiliateCampaignAsync(DeleteAffiliateCampaignRequest request);
    Task<Result<AddItemToCampaignResponse>> AddItemToCampaignAsync(AddItemToCampaignRequest request);
    Task<Result<RemoveItemFromCampaignResponse>> RemoveItemFromCampaignAsync(RemoveItemFromCampaignRequest request);
    Task<Result<GetCampaignItemListResponse>> GetCampaignItemListAsync(GetCampaignItemListRequest request);
    Task<Result<GetAffiliateOrderListResponse>> GetAffiliateOrderListAsync(GetAffiliateOrderListRequest request);
    Task<Result<GetAffiliateListResponse>> GetAffiliateListAsync(GetAffiliateListRequest request);
    Task<Result<GetAffiliateResponse>> GetAffiliateAsync(GetAffiliateRequest request);
    Task<Result<ApproveAffiliateResponse>> ApproveAffiliateAsync(ApproveAffiliateRequest request);
    Task<Result<RejectAffiliateResponse>> RejectAffiliateAsync(RejectAffiliateRequest request);
    Task<Result<GetAffiliateLinkListResponse>> GetAffiliateLinkListAsync(GetAffiliateLinkListRequest request);
    Task<Result<GetCampaignPerformanceResponse>> GetCampaignPerformanceAsync(GetCampaignPerformanceRequest request);
    Task<Result<GetCommissionSettlementListResponse>> GetCommissionSettlementListAsync(GetCommissionSettlementListRequest request);
    Task<Result<GetAmsConfigResponse>> GetAmsConfigAsync(GetAmsConfigRequest request);
    Task<Result<UpdateAmsConfigResponse>> UpdateAmsConfigAsync(UpdateAmsConfigRequest request);
}
