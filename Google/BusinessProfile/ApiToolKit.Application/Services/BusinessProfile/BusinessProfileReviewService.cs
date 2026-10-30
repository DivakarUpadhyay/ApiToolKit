using ApiToolKit.Application.DTOs.BusinessProfile.Review;
using ApiToolKit.Application.Interfaces.Services.BusinessProfile;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Interfaces.Repositories.BusinessProfile;

namespace ApiToolKit.Application.Services.BusinessProfile;

public class BusinessProfileReviewService : BusinessProfileServiceBase, IBusinessProfileReviewService
{
    public BusinessProfileReviewService(IBusinessProfileApiClient apiClient, IBusinessProfileAuthService authService, IBusinessProfileAuthRepository authRepo)
        : base(apiClient, authService, authRepo)
    {
    }

    public async Task<Result<object>> ListReviewsAsync(ListReviewsRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/reviews";
        var query = new List<KeyValuePair<string, string?>>();
        Add(query, "orderBy", r.OrderBy);
        Add(query, "pageSize", r.PageSize?.ToString());
        Add(query, "pageToken", r.PageToken);
        return await SendAsync(r.AccountKey, "ListReviews", HttpMethod.Get, url, query);
    }

    public async Task<Result<object>> GetReviewAsync(GetReviewRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/reviews/{r.ReviewId}";
        return await SendAsync(r.AccountKey, "GetReview", HttpMethod.Get, url);
    }

    public async Task<Result<object>> ReplyToReviewAsync(ReplyToReviewRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/reviews/{r.ReviewId}/reply";
        return await SendAsync(r.AccountKey, "ReplyToReview", HttpMethod.Put, url, null, new { comment = r.Comment });
    }

    public async Task<Result<object>> DeleteReviewReplyAsync(DeleteReviewReplyRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations/{r.LocationId}/reviews/{r.ReviewId}/reply";
        return await SendAsync(r.AccountKey, "DeleteReviewReply", HttpMethod.Delete, url);
    }

    public async Task<Result<object>> BatchGetReviewsAsync(BatchGetReviewsRequest r)
    {
        var url = $"https://mybusiness.googleapis.com/v4/accounts/{r.AccountId}/locations:batchGetReviews";
        return await SendAsync(r.AccountKey, "BatchGetReviews", HttpMethod.Post, url, null, new { locationNames = r.LocationNames, pageSize = r.PageSize, pageToken = r.PageToken, orderBy = r.OrderBy, ignoreRatingOnlyReviews = r.IgnoreRatingOnlyReviews });
    }
}
