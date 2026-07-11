using ApiToolKit.Application.DTOs.Flipkart.Reports;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Flipkart;

public interface IFlipkartReportService
{
    Task<Result<ReportRequestResponse>> RequestReportAsync(RequestReportRequest request);
    Task<Result<ReportDetailResponse>> GetReportDetailAsync(GetReportRequest request);
}
