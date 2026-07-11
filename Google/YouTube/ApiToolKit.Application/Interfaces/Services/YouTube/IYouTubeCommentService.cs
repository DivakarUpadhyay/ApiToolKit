using ApiToolKit.Application.DTOs.YouTube.Comment;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.YouTube;

public interface IYouTubeCommentService
{
    Task<Result<CommentListResponse>> ListCommentThreadsAsync(ListCommentThreadsRequest request, string accessToken);
    Task<Result<CommentThreadResponse>> InsertCommentAsync(InsertCommentRequest request, string accessToken);
    Task<Result<CommentResponse>> ReplyCommentAsync(ReplyCommentRequest request, string accessToken);
    Task<Result<CommentResponse>> UpdateCommentAsync(UpdateCommentRequest request, string accessToken);
    Task<Result<bool>> DeleteCommentAsync(DeleteCommentRequest request, string accessToken);
    Task<Result<bool>> ModerateCommentAsync(ModerationRequest request, string accessToken);
}
