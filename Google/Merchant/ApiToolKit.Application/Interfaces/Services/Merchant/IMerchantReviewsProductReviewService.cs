using ApiToolKit.Application.DTOs.Merchant.ReviewsProductReview;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantReviewsProductReviewService
{
    Task<Result<object>> GetProductReviewAsync(GetProductReviewRequest request);
    Task<Result<object>> ListProductReviewsAsync(ListProductReviewsRequest request);
    Task<Result<object>> InsertProductReviewAsync(InsertProductReviewRequest request);
    Task<Result<object>> DeleteProductReviewAsync(DeleteProductReviewRequest request);
}
