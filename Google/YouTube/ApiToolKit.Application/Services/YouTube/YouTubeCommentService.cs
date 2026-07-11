using ApiToolKit.Application.DTOs.YouTube.Comment;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeCommentService : IYouTubeCommentService
{
    private readonly IYouTubeApiClient _apiClient;

    public YouTubeCommentService(IYouTubeApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Result<CommentListResponse>> ListCommentThreadsAsync(ListCommentThreadsRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,replies",
            ["maxResults"] = request.PageSize.ToString(),
            ["order"] = request.Order
        };

        if (!string.IsNullOrEmpty(request.VideoId))
            queryParams["videoId"] = request.VideoId;
        else if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["allThreadsRelatedToChannelId"] = request.ChannelId;
        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;

        var response = await _apiClient.GetAsync<dynamic>("commentThreads", accessToken, queryParams);
        if (response == null) return Result<CommentListResponse>.Fail("No response from YouTube API.");

        var threads = new List<CommentThreadResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
                threads.Add(MapCommentThread(item));
        }

        var pageInfo = response?.pageInfo;
        return Result<CommentListResponse>.Ok(new CommentListResponse
        {
            CommentThreads = threads,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0)
        });
    }

    public async Task<Result<CommentThreadResponse>> InsertCommentAsync(InsertCommentRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                videoId = request.VideoId,
                topLevelComment = new
                {
                    snippet = new { textOriginal = request.TextOriginal }
                }
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("commentThreads?part=snippet", body, accessToken);
        if (response == null) return Result<CommentThreadResponse>.Fail("No response from YouTube API.");

        return Result<CommentThreadResponse>.Ok(MapCommentThread(response));
    }

    public async Task<Result<CommentResponse>> ReplyCommentAsync(ReplyCommentRequest request, string accessToken)
    {
        var body = new
        {
            snippet = new
            {
                parentId = request.ParentId,
                textOriginal = request.TextOriginal
            }
        };

        var response = await _apiClient.PostAsync<dynamic>("comments?part=snippet", body, accessToken);
        if (response == null) return Result<CommentResponse>.Fail("No response from YouTube API.");

        return Result<CommentResponse>.Ok(MapComment(response));
    }

    public async Task<Result<CommentResponse>> UpdateCommentAsync(UpdateCommentRequest request, string accessToken)
    {
        var body = new
        {
            id = request.CommentId,
            snippet = new { textOriginal = request.TextOriginal }
        };

        var response = await _apiClient.PutAsync<dynamic>("comments?part=snippet", body, accessToken);
        if (response == null) return Result<CommentResponse>.Fail("No response from YouTube API.");

        return Result<CommentResponse>.Ok(MapComment(response));
    }

    public async Task<Result<bool>> DeleteCommentAsync(DeleteCommentRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"comments?id={request.CommentId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete comment.");
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> ModerateCommentAsync(ModerationRequest request, string accessToken)
    {
        var body = new { };
        await _apiClient.PostAsync<dynamic>($"comments/setModerationStatus?id={request.CommentId}&moderationStatus={request.ModerationStatus}", body, accessToken);
        return Result<bool>.Ok(true);
    }

    private static CommentThreadResponse MapCommentThread(dynamic item)
    {
        var snippet = item?.snippet;
        var topLevelComment = snippet?.topLevelComment;

        return new CommentThreadResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            VideoId = (string?)snippet?.videoId ?? string.Empty,
            TopLevelComment = MapComment(topLevelComment),
            TotalReplyCount = (int)(snippet?.totalReplyCount ?? 0),
            CanReply = (bool)(snippet?.canReply ?? false)
        };
    }

    private static CommentResponse MapComment(dynamic item)
    {
        var snippet = item?.snippet;
        var authorChannelId = snippet?.authorChannelId;

        return new CommentResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            TextDisplay = (string?)snippet?.textDisplay ?? string.Empty,
            TextOriginal = (string?)snippet?.textOriginal ?? string.Empty,
            AuthorDisplayName = (string?)snippet?.authorDisplayName ?? string.Empty,
            AuthorChannelId = (string?)authorChannelId?.value ?? string.Empty,
            LikeCount = (int)(snippet?.likeCount ?? 0),
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            UpdatedAt = snippet?.updatedAt != null ? (DateTime)snippet.updatedAt : DateTime.MinValue,
            ParentId = (string?)snippet?.parentId
        };
    }
}
