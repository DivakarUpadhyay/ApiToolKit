using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookCommentService : IFacebookCommentService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookCommentRepository _commentRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookCommentService(IFacebookAuthRepository authRepo, IFacebookCommentRepository commentRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _commentRepo = commentRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CreateCommentResponse>> CreateCommentAsync(CreateCommentRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateCommentResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?> { ["message"] = request.Message };
        if (request.AttachmentUrl != null) body["attachment_url"] = request.AttachmentUrl;
        if (request.ParentId != null) body["parent_id"] = request.ParentId;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.ObjectId}/comments", body, accessToken);
        if (response == null) return Result<CreateCommentResponse>.Fail("No response from Facebook API.");

        string commentId = response?.id?.ToString() ?? string.Empty;

        await _commentRepo.SaveCommentAsync(new FacebookComment
        {
            CommentId = commentId,
            ObjectId = request.ObjectId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _commentRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateComment",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateCommentResponse>.Ok(new CreateCommentResponse { Id = commentId });
    }

    public async Task<Result<CommentResponse>> GetCommentAsync(string commentId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CommentResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,from,created_time,can_remove,can_hide,comment_count,like_count",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{commentId}", queryParams);
        if (response == null) return Result<CommentResponse>.Fail("No response from Facebook API.");

        var comment = JsonConvert.DeserializeObject<CommentResponse>(JsonConvert.SerializeObject(response)) ?? new CommentResponse();
        return Result<CommentResponse>.Ok(comment);
    }

    public async Task<Result<GetCommentsResponse>> GetCommentsAsync(GetCommentsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetCommentsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,from,created_time,can_remove,can_hide,comment_count,like_count",
            ["limit"] = request.Limit.ToString(),
            ["summary"] = request.Summary.ToString().ToLower(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;
        if (request.Filter != null) queryParams["filter"] = request.Filter;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.ObjectId}/comments", queryParams);
        if (response == null) return Result<GetCommentsResponse>.Fail("No response from Facebook API.");

        var comments = JsonConvert.DeserializeObject<List<CommentResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<CommentResponse>();
        var summary = new CommentSummary { TotalCount = response?.summary?.total_count ?? 0 };

        return Result<GetCommentsResponse>.Ok(new GetCommentsResponse { Data = comments, Summary = summary });
    }

    public async Task<Result<bool>> UpdateCommentAsync(UpdateCommentRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { message = request.Message };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.CommentId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteCommentAsync(string commentId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{commentId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _commentRepo.DeleteCommentAsync(commentId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> HideCommentAsync(HideCommentRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { is_hidden = request.IsHidden };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.CommentId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<GetCommentsResponse>> GetCommentRepliesAsync(string commentId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetCommentsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,from,created_time,can_remove,like_count",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{commentId}/comments", queryParams);
        if (response == null) return Result<GetCommentsResponse>.Fail("No response from Facebook API.");

        var replies = JsonConvert.DeserializeObject<List<CommentResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<CommentResponse>();

        return Result<GetCommentsResponse>.Ok(new GetCommentsResponse { Data = replies });
    }

    public async Task<Result<bool>> LikeCommentAsync(string commentId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.PostAsync<dynamic>($"/{commentId}/likes", new { }, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }
}
