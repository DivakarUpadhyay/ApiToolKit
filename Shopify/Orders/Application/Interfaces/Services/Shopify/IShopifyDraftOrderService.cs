using ApiToolKit.Application.DTOs.Shopify.Orders;
using ApiToolKit.Domain.Common;
namespace ApiToolKit.Application.Interfaces.Services.Shopify;
public interface IShopifyDraftOrderService
{
    Task<Result<ListDraftOrdersResponse>> ListDraftOrdersAsync(string shopDomain, int? limit = null);
    Task<Result<DraftOrderResponse>> GetDraftOrderAsync(string shopDomain, string draftOrderId);
    Task<Result<int>> CountDraftOrdersAsync(string shopDomain);
    Task<Result<DraftOrderResponse>> CreateDraftOrderAsync(string shopDomain, CreateDraftOrderRequest request);
    Task<Result<DraftOrderResponse>> UpdateDraftOrderAsync(string shopDomain, string draftOrderId, UpdateDraftOrderRequest request);
    Task<Result<DraftOrderResponse>> CompleteDraftOrderAsync(string shopDomain, string draftOrderId);
    Task<Result<bool>> DeleteDraftOrderAsync(string shopDomain, string draftOrderId);
}
