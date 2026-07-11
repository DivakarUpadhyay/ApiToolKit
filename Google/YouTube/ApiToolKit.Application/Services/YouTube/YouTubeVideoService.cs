using ApiToolKit.Application.DTOs.YouTube.Video;
using ApiToolKit.Application.Interfaces.Services.YouTube;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.YouTube;
using ApiToolKit.Domain.Interfaces.Repositories.YouTube;
using Newtonsoft.Json.Linq;

namespace ApiToolKit.Application.Services.YouTube;

public class YouTubeVideoService : IYouTubeVideoService
{
    private readonly IYouTubeApiClient _apiClient;
    private readonly IYouTubeVideoRepository _videoRepo;

    public YouTubeVideoService(IYouTubeApiClient apiClient, IYouTubeVideoRepository videoRepo)
    {
        _apiClient = apiClient;
        _videoRepo = videoRepo;
    }

    public async Task<Result<VideoListResponse>> ListVideosAsync(ListVideosRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails,statistics,status",
            ["type"] = "video",
            ["maxResults"] = request.PageSize.ToString(),
            ["order"] = request.Order
        };

        if (!string.IsNullOrEmpty(request.ChannelId))
            queryParams["channelId"] = request.ChannelId;
        if (!string.IsNullOrEmpty(request.PageToken))
            queryParams["pageToken"] = request.PageToken;
        if (request.PublishedAfter.HasValue)
            queryParams["publishedAfter"] = request.PublishedAfter.Value.ToString("o");
        if (request.PublishedBefore.HasValue)
            queryParams["publishedBefore"] = request.PublishedBefore.Value.ToString("o");

        var response = await _apiClient.GetAsync<dynamic>("search", accessToken, queryParams);
        if (response == null) return Result<VideoListResponse>.Fail("No response from YouTube API.");

        var videos = new List<VideoResponse>();
        var items = response?.items;
        if (items != null)
        {
            foreach (var item in items)
            {
                videos.Add(MapVideoResponse(item));
            }
        }

        var pageInfo = response?.pageInfo;
        var result = new VideoListResponse
        {
            Videos = videos,
            NextPageToken = (string?)response?.nextPageToken,
            TotalResults = (int)(pageInfo?.totalResults ?? 0),
            ResultsPerPage = (int)(pageInfo?.resultsPerPage ?? 0)
        };

        return Result<VideoListResponse>.Ok(result);
    }

    public async Task<Result<VideoResponse>> GetVideoAsync(GetVideoRequest request, string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["part"] = "snippet,contentDetails,statistics,status",
            ["id"] = request.VideoId
        };

        var response = await _apiClient.GetAsync<dynamic>("videos", accessToken, queryParams);
        if (response == null) return Result<VideoResponse>.Fail("No response from YouTube API.");

        var items = response?.items;
        if (items == null || items.Count == 0)
            return Result<VideoResponse>.Fail("Video not found.");

        return Result<VideoResponse>.Ok(MapVideoResponse(items[0]));
    }

    public async Task<Result<VideoResponse>> UpdateVideoAsync(UpdateVideoRequest request, string accessToken)
    {
        var body = new
        {
            id = request.VideoId,
            snippet = new
            {
                title = request.Title,
                description = request.Description,
                tags = request.Tags,
                categoryId = request.CategoryId,
                defaultLanguage = request.DefaultLanguage
            },
            status = new { privacyStatus = request.Status }
        };

        var response = await _apiClient.PutAsync<dynamic>("videos?part=snippet,status", body, accessToken);
        if (response == null) return Result<VideoResponse>.Fail("No response from YouTube API.");

        var mapped = MapVideoResponse(response);

        await _videoRepo.UpdateVideoAsync(new YouTubeVideo
        {
            VideoId = request.VideoId,
            Title = request.Title ?? string.Empty,
            Description = request.Description ?? string.Empty,
            Status = request.Status ?? string.Empty
        });

        return Result<VideoResponse>.Ok(mapped);
    }

    public async Task<Result<bool>> DeleteVideoAsync(DeleteVideoRequest request, string accessToken)
    {
        var success = await _apiClient.DeleteAsync($"videos?id={request.VideoId}", accessToken);
        if (!success) return Result<bool>.Fail("Failed to delete video.");

        await _videoRepo.DeleteVideoAsync(request.VideoId);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> RateVideoAsync(RateVideoRequest request, string accessToken)
    {
        var body = new { };
        var success = await _apiClient.PostAsync<dynamic>($"videos/rate?id={request.VideoId}&rating={request.Rating}", body, accessToken);
        return Result<bool>.Ok(true);
    }

    public async Task<Result<UploadVideoResult>> UploadVideoAsync(UploadVideoMetadata metadata, Stream videoStream, string fileName, string mimeType, string accessToken)
    {
        try
        {
            var body = new
            {
                snippet = new
                {
                    title = metadata.Title,
                    description = metadata.Description,
                    tags = metadata.Tags,
                    categoryId = metadata.CategoryId,
                    defaultLanguage = metadata.DefaultLanguage
                },
                status = new { privacyStatus = metadata.PrivacyStatus }
            };

            var response = await _apiClient.UploadVideoAsync<dynamic>(body, videoStream, mimeType, accessToken);
            if (response == null) return Result<UploadVideoResult>.Fail("No response from YouTube upload API.");

            var result = new UploadVideoResult
            {
                Id = (string?)response?.id ?? string.Empty,
                Title = (string?)response?.snippet?.title ?? metadata.Title,
                Status = (string?)response?.status?.privacyStatus ?? metadata.PrivacyStatus,
                UploadStatus = (string?)response?.status?.uploadStatus ?? "uploaded",
                FileName = fileName
            };

            return Result<UploadVideoResult>.Ok(result);
        }
        catch (Exception ex)
        {
            return Result<UploadVideoResult>.Fail($"Upload failed for '{fileName}': {ex.Message}");
        }
    }

    public async Task<Result<BulkUploadResult>> BulkUploadVideosAsync(
        List<(UploadVideoMetadata Metadata, Stream Stream, string FileName, string MimeType)> uploads,
        string accessToken)
    {
        var bulkResult = new BulkUploadResult { Total = uploads.Count };

        foreach (var (metadata, stream, fileName, mimeType) in uploads)
        {
            var uploadResult = await UploadVideoAsync(metadata, stream, fileName, mimeType, accessToken);
            if (uploadResult.Success && uploadResult.Data != null)
            {
                bulkResult.Succeeded++;
                bulkResult.Results.Add(uploadResult.Data);
            }
            else
            {
                bulkResult.Failed++;
                bulkResult.Results.Add(new UploadVideoResult
                {
                    FileName = fileName,
                    Error = uploadResult.Message
                });
            }
        }

        return Result<BulkUploadResult>.Ok(bulkResult);
    }

    private static string GetThumbnailUrl(dynamic? thumbnails)
    {
        if (thumbnails == null) return string.Empty;
        try { return JObject.FromObject(thumbnails)["default"]?["url"]?.ToString() ?? string.Empty; }
        catch { return string.Empty; }
    }

    private static VideoResponse MapVideoResponse(dynamic item)
    {
        var snippet = item?.snippet;
        var statistics = item?.statistics;
        var contentDetails = item?.contentDetails;
        var status = item?.status;

        var tags = new List<string>();
        if (snippet?.tags != null)
        {
            foreach (var tag in snippet.tags)
                tags.Add((string)tag);
        }

        return new VideoResponse
        {
            Id = (string?)item?.id ?? string.Empty,
            Title = (string?)snippet?.title ?? string.Empty,
            Description = (string?)snippet?.description ?? string.Empty,
            ChannelId = (string?)snippet?.channelId ?? string.Empty,
            ChannelTitle = (string?)snippet?.channelTitle ?? string.Empty,
            PublishedAt = snippet?.publishedAt != null ? (DateTime)snippet.publishedAt : DateTime.MinValue,
            Duration = (string?)contentDetails?.duration ?? string.Empty,
            ViewCount = statistics?.viewCount != null ? (long)statistics.viewCount : 0L,
            LikeCount = statistics?.likeCount != null ? (long)statistics.likeCount : 0L,
            DislikeCount = 0L,
            CommentCount = statistics?.commentCount != null ? (long)statistics.commentCount : 0L,
            Status = (string?)status?.privacyStatus ?? string.Empty,
            ThumbnailUrl = GetThumbnailUrl(snippet?.thumbnails),
            Tags = tags,
            CategoryId = (string?)snippet?.categoryId ?? string.Empty,
            LiveBroadcastContent = (string?)snippet?.liveBroadcastContent ?? string.Empty
        };
    }
}
