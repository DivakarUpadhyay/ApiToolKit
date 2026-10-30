using ApiToolKit.Application.DTOs.CampaignManager.Campaign;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerCampaignService
{
    Task<Result<object>> InsertCampaignAsync(InsertCampaignRequest request);
    Task<Result<object>> ListCampaignsAsync(ListCampaignsRequest request);
    Task<Result<object>> GetCampaignAsync(GetCampaignRequest request);
    Task<Result<object>> UpdateCampaignAsync(UpdateCampaignRequest request);
    Task<Result<object>> PatchCampaignAsync(PatchCampaignRequest request);
}
