using ApiToolKit.Application.DTOs.CampaignManager.ReportData;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerReportDataService
{
    Task<Result<object>> QueryReportDataAsync(QueryReportDataRequest request);
}
