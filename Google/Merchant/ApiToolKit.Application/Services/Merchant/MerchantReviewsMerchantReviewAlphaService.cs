using ApiToolKit.Application.DTOs.Merchant.ReviewsMerchantReviewAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantReviewsMerchantReviewAlphaService : MerchantServiceBase, IMerchantReviewsMerchantReviewAlphaService
{
    public MerchantReviewsMerchantReviewAlphaService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteMerchantReviewAsync(DeleteMerchantReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/merchantReviews/{Esc(r.MerchantReviewId)}";
        return await SendAsync(r.AccountKey, "DeleteMerchantReview", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> GetMerchantReviewAsync(GetMerchantReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/merchantReviews/{Esc(r.MerchantReviewId)}";
        return await SendAsync(r.AccountKey, "GetMerchantReview", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ListMerchantReviewsAsync(ListMerchantReviewsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/merchantReviews";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListMerchantReviews", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> InsertMerchantReviewAsync(InsertMerchantReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/merchantReviews:insert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dataSource", r.DataSource);
        return await SendAsync(r.AccountKey, "InsertMerchantReview", HttpMethod.Post, url, query, r.Body);
    }
}
