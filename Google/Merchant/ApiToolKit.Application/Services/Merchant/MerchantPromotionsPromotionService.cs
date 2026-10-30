using ApiToolKit.Application.DTOs.Merchant.PromotionsPromotion;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantPromotionsPromotionService : MerchantServiceBase, IMerchantPromotionsPromotionService
{
    public MerchantPromotionsPromotionService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListPromotionsAsync(ListPromotionsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/promotions/v1/accounts/{Esc(r.AccountId)}/promotions";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageToken", r.PageToken);
        Add(query, "pageSize", r.PageSize?.ToString());
        return await SendAsync(r.AccountKey, "ListPromotions", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetPromotionAsync(GetPromotionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/promotions/v1/accounts/{Esc(r.AccountId)}/promotions/{Esc(r.PromotionId)}";
        return await SendAsync(r.AccountKey, "GetPromotion", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertPromotionAsync(InsertPromotionRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/promotions/v1/accounts/{Esc(r.AccountId)}/promotions:insert";
        return await SendAsync(r.AccountKey, "InsertPromotion", HttpMethod.Post, url, null, r.Body);
    }
}
