using ApiToolKit.Application.DTOs.Merchant.ReportsReport;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantReportsReportService
{
    Task<Result<object>> SearchReportAsync(SearchReportRequest request);
}
