using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyTransactionService
{
    Task<Result<ListTransactionsResponse>> ListTransactionsAsync(string shopDomain, string orderId);
    Task<Result<TransactionResponse>> GetTransactionAsync(string shopDomain, string orderId, string transactionId);
    Task<Result<int>> CountTransactionsAsync(string shopDomain, string orderId);
    Task<Result<TransactionResponse>> CreateTransactionAsync(string shopDomain, string orderId, CreateTransactionRequest request);
}
