using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyReportService
{
    Task<Result<ListReportsResponse>> ListReportsAsync(string shopDomain, int? limit = null, string? sinceId = null);
    Task<Result<ReportResponse>> GetReportAsync(string shopDomain, string reportId);
    Task<Result<int>> CountReportsAsync(string shopDomain);
    Task<Result<ReportResponse>> CreateReportAsync(string shopDomain, CreateReportRequest request);
    Task<Result<ReportResponse>> UpdateReportAsync(string shopDomain, string reportId, UpdateReportRequest request);
    Task<Result<bool>> DeleteReportAsync(string shopDomain, string reportId);
}
