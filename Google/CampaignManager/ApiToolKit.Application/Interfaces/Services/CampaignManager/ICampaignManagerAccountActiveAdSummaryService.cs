using ApiToolKit.Application.DTOs.CampaignManager.AccountActiveAdSummary;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerAccountActiveAdSummaryService
{
    Task<Result<object>> GetAccountActiveAdSummaryAsync(GetAccountActiveAdSummaryRequest request);
}
