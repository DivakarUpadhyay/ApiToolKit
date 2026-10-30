using ApiToolKit.Application.DTOs.BusinessProfile.Review;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.BusinessProfile;

public interface IBusinessProfileReviewService
{
    Task<Result<object>> ListReviewsAsync(ListReviewsRequest request);
    Task<Result<object>> GetReviewAsync(GetReviewRequest request);
    Task<Result<object>> ReplyToReviewAsync(ReplyToReviewRequest request);
    Task<Result<object>> DeleteReviewReplyAsync(DeleteReviewReplyRequest request);
    Task<Result<object>> BatchGetReviewsAsync(BatchGetReviewsRequest request);
}
