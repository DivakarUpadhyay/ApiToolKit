using ApiToolKit.Application.DTOs.CampaignManager.Report;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.CampaignManager;

public interface ICampaignManagerReportService
{
    Task<Result<object>> RunReportAsync(RunReportRequest request);
    Task<Result<object>> DeleteReportAsync(DeleteReportRequest request);
    Task<Result<object>> ListReportsAsync(ListReportsRequest request);
    Task<Result<object>> GetReportAsync(GetReportRequest request);
    Task<Result<object>> UpdateReportAsync(UpdateReportRequest request);
    Task<Result<object>> InsertReportAsync(InsertReportRequest request);
}
