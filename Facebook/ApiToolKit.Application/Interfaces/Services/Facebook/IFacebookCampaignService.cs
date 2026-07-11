using ApiToolKit.Application.DTOs.Facebook.Campaign;
using ApiToolKit.Application.DTOs.Facebook.AdSet;
using ApiToolKit.Application.DTOs.Facebook.Ad;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookCampaignService
{
    Task<Result<CreateCampaignResponse>> CreateCampaignAsync(CreateCampaignRequest request, string accessToken);
    Task<Result<CampaignResponse>> GetCampaignAsync(string campaignId, string accessToken);
    Task<Result<GetCampaignsResponse>> GetCampaignsAsync(GetCampaignsRequest request, string accessToken);
    Task<Result<bool>> UpdateCampaignAsync(UpdateCampaignRequest request, string accessToken);
    Task<Result<bool>> DeleteCampaignAsync(string campaignId, string accessToken);
    Task<Result<GetAdSetsResponse>> GetCampaignAdSetsAsync(string campaignId, string accessToken, int limit);
    Task<Result<GetAdsResponse>> GetCampaignAdsAsync(string campaignId, string accessToken, int limit);
}
