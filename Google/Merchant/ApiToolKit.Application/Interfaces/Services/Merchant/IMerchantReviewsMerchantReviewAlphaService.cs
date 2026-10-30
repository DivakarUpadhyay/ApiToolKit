using ApiToolKit.Application.DTOs.Merchant.ReviewsMerchantReviewAlpha;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Merchant;

public interface IMerchantReviewsMerchantReviewAlphaService
{
    Task<Result<object>> DeleteMerchantReviewAsync(DeleteMerchantReviewRequest request);
    Task<Result<object>> GetMerchantReviewAsync(GetMerchantReviewRequest request);
    Task<Result<object>> ListMerchantReviewsAsync(ListMerchantReviewsRequest request);
    Task<Result<object>> InsertMerchantReviewAsync(InsertMerchantReviewRequest request);
}
