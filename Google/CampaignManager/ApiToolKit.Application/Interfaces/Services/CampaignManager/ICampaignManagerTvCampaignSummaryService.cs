using ApiToolKit.Application.DTOs.CampaignManager.TvCampaignSummary;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerTvCampaignSummaryService
{
    Task<Result<object>> ListTvCampaignSummariesAsync(ListTvCampaignSummariesRequest request);
}
