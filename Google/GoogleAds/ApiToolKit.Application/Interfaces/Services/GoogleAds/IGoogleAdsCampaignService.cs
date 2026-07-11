using ApiToolKit.Application.DTOs.GoogleAds.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsCampaignService
{
    Task<Result<CampaignListResponse>> ListCampaignsAsync(ListCampaignsRequest request);
    Task<Result<CampaignResponse>> GetCampaignAsync(string customerId, string resourceName);
    Task<Result<MutateOperationResponse>> CreateCampaignAsync(CreateCampaignRequest request);
    Task<Result<MutateOperationResponse>> UpdateCampaignAsync(UpdateCampaignRequest request);
    Task<Result<bool>> RemoveCampaignAsync(string customerId, string resourceName);
}
