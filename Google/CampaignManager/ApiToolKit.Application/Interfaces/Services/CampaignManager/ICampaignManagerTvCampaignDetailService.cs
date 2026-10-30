using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignDetail;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerTvCampaignDetailService
{
    Task<Result<object>> GetTvCampaignDetailAsync(GetTvCampaignDetailRequest request);
}
