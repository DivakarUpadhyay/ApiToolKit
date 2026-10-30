using ApiToolKit.Application.DTOs.Merchant.ReviewsProductReviewAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.Merchant;

namespace ApiToolKit.Application.Services.Merchant;

public class MerchantReviewsProductReviewAlphaService : MerchantServiceBase, IMerchantReviewsProductReviewAlphaService
{
    public MerchantReviewsProductReviewAlphaService(IMerchantApiClient apiClient, IMerchantAuthService authService, IMerchantAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> DeleteProductReviewAsync(DeleteProductReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/productReviews/{Esc(r.ProductReviewId)}";
        return await SendAsync(r.AccountKey, "DeleteProductReview", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> ListProductReviewsAsync(ListProductReviewsRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/productReviews";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListProductReviews", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetProductReviewAsync(GetProductReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/productReviews/{Esc(r.ProductReviewId)}";
        return await SendAsync(r.AccountKey, "GetProductReview", HttpMethod.Get, url);
    }

    public async Task<Result<object>> InsertProductReviewAsync(InsertProductReviewRequest r)
    {
        var url = $"https://merchantapi.googleapis.com/reviews/v1alpha/accounts/{Esc(r.AccountId)}/productReviews:insert";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "dataSource", r.DataSource);
        return await SendAsync(r.AccountKey, "InsertProductReview", HttpMethod.Post, url, query, r.Body);
    }
}
