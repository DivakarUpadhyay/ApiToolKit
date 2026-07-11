using ApiToolKit.Application.DTOs.Shopify.Analytics;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyPaymentsService
{
    Task<Result<ListPayoutsResponse>> ListPayoutsAsync(string shopDomain, string? status = null, int? limit = null);
    Task<Result<PayoutResponse>> GetPayoutAsync(string shopDomain, string payoutId);
    Task<Result<ListDisputesResponse>> ListDisputesAsync(string shopDomain, string? initiatedAt = null, int? limit = null);
    Task<Result<DisputeResponse>> GetDisputeAsync(string shopDomain, string disputeId);
    Task<Result<ListBalanceTransactionsResponse>> ListBalanceTransactionsAsync(string shopDomain, string? payoutId = null, int? limit = null);
}
