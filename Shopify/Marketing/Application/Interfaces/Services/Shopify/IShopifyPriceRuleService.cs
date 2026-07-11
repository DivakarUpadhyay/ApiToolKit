using ApiToolKit.Application.DTOs.Shopify.Marketing;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Shopify;

public interface IShopifyPriceRuleService
{
    Task<Result<ListPriceRulesResponse>> ListPriceRulesAsync(string shopDomain, int? limit = null);
    Task<Result<PriceRuleResponse>> GetPriceRuleAsync(string shopDomain, string priceRuleId);
    Task<Result<int>> CountPriceRulesAsync(string shopDomain);
    Task<Result<PriceRuleResponse>> CreatePriceRuleAsync(string shopDomain, CreatePriceRuleRequest request);
    Task<Result<PriceRuleResponse>> UpdatePriceRuleAsync(string shopDomain, string priceRuleId, UpdatePriceRuleRequest request);
    Task<Result<bool>> DeletePriceRuleAsync(string shopDomain, string priceRuleId);
}
