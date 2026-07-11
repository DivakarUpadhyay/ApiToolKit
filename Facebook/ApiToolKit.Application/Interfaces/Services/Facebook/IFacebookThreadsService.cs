using ApiToolKit.Application.DTOs.Facebook.Threads;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookThreadsService
{
    Task<Result<ThreadsProfile>> GetProfileAsync(string accessToken);
    Task<Result<ThreadsListResponse>> GetPostsAsync(string accessToken, int limit = 20);
    Task<Result<ThreadsPublishResponse>> CreateTextPostAsync(CreateTextThreadRequest request, string accessToken);
    Task<Result<ThreadsPublishResponse>> CreateImagePostAsync(CreateMediaThreadRequest request, string accessToken);
    Task<Result<ThreadsPublishResponse>> CreateVideoPostAsync(CreateMediaThreadRequest request, string accessToken);
    Task<Result<ThreadsPublishResponse>> CreateCarouselPostAsync(CreateCarouselThreadRequest request, string accessToken);
    Task<Result<ThreadsPublishResponse>> PublishContainerAsync(PublishThreadRequest request, string accessToken);
    Task<Result<ThreadsPost>> GetPostAsync(string threadId, string accessToken);
    Task<Result<bool>> DeletePostAsync(string threadId, string accessToken);
    Task<Result<ThreadsPublishResponse>> ReplyToPostAsync(string threadId, CreateTextThreadRequest request, string accessToken);
    Task<Result<ThreadsListResponse>> GetRepliesAsync(string threadId, string accessToken);
    Task<Result<ThreadsListResponse>> GetConversationAsync(string threadId, string accessToken);
    Task<Result<ThreadsInsightsResponse>> GetInsightsAsync(string accessToken);
    Task<Result<ThreadsInsightsResponse>> GetPostInsightsAsync(string threadId, string accessToken);
    Task<Result<ThreadsPublishingLimitResponse>> GetPublishingLimitAsync(string accessToken);
}
