using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Comment;
using ApiToolKit.Application.DTOs.Facebook.Reaction;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookPostService : IFacebookPostService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookPostRepository _postRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookPostService(IFacebookAuthRepository authRepo, IFacebookPostRepository postRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _postRepo = postRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<CreatePostResponse>> CreatePostAsync(CreatePostRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreatePostResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Message != null) body["message"] = request.Message;
        if (request.Link != null) body["link"] = request.Link;
        body["published"] = request.Published;
        if (request.ScheduledPublishTime.HasValue)
            body["scheduled_publish_time"] = request.ScheduledPublishTime.Value;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/feed", body, accessToken);
        if (response == null) return Result<CreatePostResponse>.Fail("No response from Facebook API.");

        string postId = response?.id?.ToString() ?? string.Empty;

        await _postRepo.SavePostAsync(new FacebookPost
        {
            PostId = postId,
            PageId = request.PageId,
            Message = request.Message,
            IsPublished = request.Published,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _postRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "CreatePost",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreatePostResponse>.Ok(new CreatePostResponse { Id = postId });
    }

    public async Task<Result<PostResponse>> GetPostAsync(string postId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PostResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,story,created_time,updated_time,permalink_url,full_picture,from",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{postId}", queryParams);
        if (response == null) return Result<PostResponse>.Fail("No response from Facebook API.");

        var post = JsonConvert.DeserializeObject<PostResponse>(JsonConvert.SerializeObject(response)) ?? new PostResponse();
        return Result<PostResponse>.Ok(post);
    }

    public async Task<Result<bool>> UpdatePostAsync(UpdatePostRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Message != null) body["message"] = request.Message;
        if (request.Published.HasValue) body["published"] = request.Published.Value;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PostId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _postRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "UpdatePost",
            RequestBody = JsonConvert.SerializeObject(request),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeletePostAsync(string postId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{postId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _postRepo.DeletePostAsync(postId);
        await _postRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "DeletePost",
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    public async Task<Result<FeedResponse>> GetFeedAsync(GetFeedRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<FeedResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;
        if (request.Since != null) queryParams["since"] = request.Since;
        if (request.Until != null) queryParams["until"] = request.Until;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/feed", queryParams);
        if (response == null) return Result<FeedResponse>.Fail("No response from Facebook API.");

        var posts = JsonConvert.DeserializeObject<List<PostResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PostResponse>();

        return Result<FeedResponse>.Ok(new FeedResponse { Data = posts });
    }

    public async Task<Result<GetCommentsResponse>> GetPostCommentsAsync(GetCommentsRequest request, string accessToken)
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

    public async Task<Result<GetReactionsResponse>> GetPostReactionsAsync(GetReactionsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetReactionsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,type,pic",
            ["limit"] = request.Limit.ToString(),
            ["summary"] = "true",
            ["access_token"] = accessToken
        };
        if (request.Type != null) queryParams["type"] = request.Type;
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.ObjectId}/reactions", queryParams);
        if (response == null) return Result<GetReactionsResponse>.Fail("No response from Facebook API.");

        var reactions = JsonConvert.DeserializeObject<List<ReactionResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<ReactionResponse>();

        return Result<GetReactionsResponse>.Ok(new GetReactionsResponse { Data = reactions });
    }

    public async Task<Result<bool>> PublishScheduledPostAsync(string postId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { is_published = true };
        var response = await _apiClient.PostAsync<dynamic>($"/{postId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }
}
