using ApiToolKit.Application.DTOs.GoogleAds.Report;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.GoogleAds;

public interface IGoogleAdsReportService
{
    Task<Result<ReportResponse>> RunReportAsync(RunReportRequest request);
}
