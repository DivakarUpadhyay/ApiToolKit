using ApiToolKit.Application.DTOs.Merchant.ReviewsProductReviewAlpha;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantReviewsProductReviewAlphaService
{
    Task<Result<object>> DeleteProductReviewAsync(DeleteProductReviewRequest request);
    Task<Result<object>> ListProductReviewsAsync(ListProductReviewsRequest request);
    Task<Result<object>> GetProductReviewAsync(GetProductReviewRequest request);
    Task<Result<object>> InsertProductReviewAsync(InsertProductReviewRequest request);
}
