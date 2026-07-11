using ApiToolKit.Application.DTOs.Flipkart.Reports;
using ApiToolKit.Application.Interfaces.Services.Flipkart;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.Flipkart;

public class FlipkartReportService : IFlipkartReportService
{
    private readonly IFlipkartApiClient _api;
    public FlipkartReportService(IFlipkartApiClient api) => _api = api;

    public async Task<Result<ReportRequestResponse>> RequestReportAsync(RequestReportRequest request)
    {
        var body = new { correlation_id = request.CorrelationId, parameters = request.Parameters };
        var result = await _api.PostAsync<ReportRequestResponse>($"/reports/{Uri.EscapeDataString(request.ReportTypeIdentifier)}", body);
        return result != null ? Result<ReportRequestResponse>.Ok(result) : Result<ReportRequestResponse>.Fail("No response.");
    }

    public async Task<Result<ReportDetailResponse>> GetReportDetailAsync(GetReportRequest request)
    {
        var result = await _api.GetAsync<ReportDetailResponse>($"/reports/{Uri.EscapeDataString(request.ReportId)}/detail");
        return result != null ? Result<ReportDetailResponse>.Ok(result) : Result<ReportDetailResponse>.Fail("No response.");
    }
}
