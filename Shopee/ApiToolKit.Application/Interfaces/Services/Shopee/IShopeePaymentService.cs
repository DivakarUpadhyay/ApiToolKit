using ApiToolKit.Application.DTOs.Shopee.Payment;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopee;
public interface IShopeePaymentService
{
    Task<Result<object>> GetEscrowListAsync(GetEscrowListRequest request);
    Task<Result<object>> GetEscrowDetailAsync(GetEscrowDetailRequest request);
    Task<Result<object>> GetEscrowDetailBatchAsync(long shopId, List<string> orderSnList);
    Task<Result<object>> GetWalletTransactionListAsync(GetWalletTransactionListRequest request);
    Task<Result<object>> GetPaymentMethodListAsync(GetPaymentMethodListRequest request);
    Task<Result<object>> GetShopInstallmentStatusAsync(GetInstallmentStatusRequest request);
    Task<Result<bool>> SetShopInstallmentStatusAsync(SetInstallmentStatusRequest request);
    Task<Result<object>> GetItemInstallmentStatusAsync(GetItemInstallmentRequest request);
    Task<Result<bool>> SetItemInstallmentStatusAsync(SetItemInstallmentRequest request);
    Task<Result<bool>> GenerateIncomeReportAsync(GenerateIncomeReportRequest request);
    Task<Result<object>> GetIncomeReportAsync(GetIncomeReportRequest request);
    Task<Result<object>> GetBillingTransactionInfoAsync(GetBillingTransactionRequest request);
    Task<Result<object>> GetPayoutDetailAsync(GetPayoutDetailRequest request);
    Task<Result<object>> GetPayoutInfoAsync(GetPayoutInfoRequest request);
}
