using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookCommentService
{
    Task<Result<CreateCommentResponse>> CreateCommentAsync(CreateCommentRequest request, string accessToken);
    Task<Result<CommentResponse>> GetCommentAsync(string commentId, string accessToken);
    Task<Result<GetCommentsResponse>> GetCommentsAsync(GetCommentsRequest request, string accessToken);
    Task<Result<bool>> UpdateCommentAsync(UpdateCommentRequest request, string accessToken);
    Task<Result<bool>> DeleteCommentAsync(string commentId, string accessToken);
    Task<Result<bool>> HideCommentAsync(HideCommentRequest request, string accessToken);
    Task<Result<GetCommentsResponse>> GetCommentRepliesAsync(string commentId, string accessToken, int limit);
    Task<Result<bool>> LikeCommentAsync(string commentId, string accessToken);
}
