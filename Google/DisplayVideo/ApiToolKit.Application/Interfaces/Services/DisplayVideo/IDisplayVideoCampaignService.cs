using ApiToolKit.Application.DTOs.DisplayVideo.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.DisplayVideo;

public interface IDisplayVideoCampaignService
{
    Task<Result<object>> CreateCampaignAsync(CreateCampaignRequest request);
    Task<Result<object>> ListCampaignsAsync(ListCampaignsRequest request);
    Task<Result<object>> UpdateCampaignAsync(UpdateCampaignRequest request);
    Task<Result<object>> GetCampaignAsync(GetCampaignRequest request);
    Task<Result<object>> DeleteCampaignAsync(DeleteCampaignRequest request);
}
