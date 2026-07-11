using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyDiscountCodeService
{
    Task<Result<ListDiscountCodesResponse>> ListDiscountCodesAsync(string shopDomain, string priceRuleId, int? limit = null);
    Task<Result<DiscountCodeResponse>> GetDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId);
    Task<Result<int>> CountDiscountCodesAsync(string shopDomain, string priceRuleId);
    Task<Result<DiscountCodeResponse>> CreateDiscountCodeAsync(string shopDomain, string priceRuleId, CreateDiscountCodeRequest request);
    Task<Result<DiscountCodeResponse>> UpdateDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId, UpdateDiscountCodeRequest request);
    Task<Result<bool>> DeleteDiscountCodeAsync(string shopDomain, string priceRuleId, string discountCodeId);
    Task<Result<BatchDiscountCodeResponse>> CreateBatchDiscountCodesAsync(string shopDomain, string priceRuleId, BatchDiscountCodeRequest request);
    Task<Result<BatchDiscountCodeResponse>> GetBatchDiscountCodeStatusAsync(string shopDomain, string priceRuleId, string batchId);
}
