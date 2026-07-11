using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookPostService
{
    Task<Result<CreatePostResponse>> CreatePostAsync(CreatePostRequest request, string accessToken);
    Task<Result<PostResponse>> GetPostAsync(string postId, string accessToken);
    Task<Result<bool>> UpdatePostAsync(UpdatePostRequest request, string accessToken);
    Task<Result<bool>> DeletePostAsync(string postId, string accessToken);
    Task<Result<FeedResponse>> GetFeedAsync(GetFeedRequest request, string accessToken);
    Task<Result<GetCommentsResponse>> GetPostCommentsAsync(GetCommentsRequest request, string accessToken);
    Task<Result<GetReactionsResponse>> GetPostReactionsAsync(GetReactionsRequest request, string accessToken);
    Task<Result<bool>> PublishScheduledPostAsync(string postId, string accessToken);
}
