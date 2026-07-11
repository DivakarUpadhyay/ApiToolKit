using ApiToolKit.Application.DTOs.Shopee.Ads;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopee;

public interface IShopeeAdsService
{
    Task<Result<GetRecommendKeywordResponse>> GetRecommendKeywordAsync(GetRecommendKeywordRequest request);
    Task<Result<CreateCampaignResponse>> CreateCampaignAsync(CreateCampaignRequest request);
    Task<Result<GetCampaignListResponse>> GetCampaignListAsync(GetCampaignListRequest request);
    Task<Result<GetCampaignResponse>> GetCampaignAsync(GetCampaignRequest request);
    Task<Result<UpdateCampaignResponse>> UpdateCampaignAsync(UpdateCampaignRequest request);
    Task<Result<DeleteCampaignResponse>> DeleteCampaignAsync(DeleteCampaignRequest request);
    Task<Result<CreateAdGroupResponse>> CreateAdGroupAsync(CreateAdGroupRequest request);
    Task<Result<GetAdGroupListResponse>> GetAdGroupListAsync(GetAdGroupListRequest request);
    Task<Result<UpdateAdGroupResponse>> UpdateAdGroupAsync(UpdateAdGroupRequest request);
    Task<Result<CreateAdResponse>> CreateAdAsync(CreateAdRequest request);
    Task<Result<GetAdListResponse>> GetAdListAsync(GetAdListRequest request);
    Task<Result<UpdateAdResponse>> UpdateAdAsync(UpdateAdRequest request);
    Task<Result<GetAdDailyPerformanceResponse>> GetAdDailyPerformanceAsync(GetAdDailyPerformanceRequest request);
    Task<Result<GetCampaignDailyPerformanceResponse>> GetCampaignDailyPerformanceAsync(GetCampaignDailyPerformanceRequest request);
    Task<Result<AddKeywordResponse>> AddKeywordAsync(AddKeywordRequest request);
    Task<Result<GetKeywordListResponse>> GetKeywordListAsync(GetKeywordListRequest request);
    Task<Result<UpdateKeywordResponse>> UpdateKeywordAsync(UpdateKeywordRequest request);
    Task<Result<DeleteKeywordResponse>> DeleteKeywordAsync(DeleteKeywordRequest request);
}
