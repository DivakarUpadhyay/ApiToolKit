using ApiToolKit.Application.DTOs.Facebook.Threads;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookThreadsService : IFacebookThreadsService
{
    private readonly IThreadsApiClient _apiClient;
    private readonly IFacebookThreadsRepository _repo;
    private readonly IConfiguration _config;

    private string ApiVersion => _config["Threads:ApiVersion"] ?? "v1.0";

    public FacebookThreadsService(IThreadsApiClient apiClient, IFacebookThreadsRepository repo, IConfiguration config)
    {
        _apiClient = apiClient;
        _repo = repo;
        _config = config;
    }

    private async Task LogAsync(string apiName, bool success, int statusCode = 200, string? request = null, string? response = null)
    {
        await _repo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = apiName,
            RequestBody = request ?? string.Empty,
            ResponseBody = response ?? string.Empty,
            IsSuccess = success,
            StatusCode = statusCode,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<Result<ThreadsProfile>> GetProfileAsync(string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsProfile>(
                "/me?fields=id,username,name,threads_profile_picture_url,threads_biography",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetProfile", result != null, 200, null, responseJson);

            return result != null
                ? Result<ThreadsProfile>.Ok(result)
                : Result<ThreadsProfile>.Fail("Failed to retrieve Threads profile.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetProfile", false, 500, null, ex.Message);
            return Result<ThreadsProfile>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsListResponse>> GetPostsAsync(string accessToken, int limit = 20)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsListResponse>(
                $"/me/threads?fields=id,text,timestamp,media_type,shortcode,media_url,permalink&limit={limit}",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetPosts", result != null, 200, null, responseJson);

            return result != null
                ? Result<ThreadsListResponse>.Ok(result)
                : Result<ThreadsListResponse>.Fail("Failed to retrieve Threads posts.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetPosts", false, 500, null, ex.Message);
            return Result<ThreadsListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> CreateTextPostAsync(CreateTextThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var body = new Dictionary<string, object?> { ["media_type"] = "TEXT", ["text"] = request.Text };
            if (!string.IsNullOrEmpty(request.ReplyToId)) body["reply_to_id"] = request.ReplyToId;

            var container = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, body);
            if (container?.Id == null)
            {
                await LogAsync("Threads.CreateTextPost", false, 400, requestJson, "Container creation failed.");
                return Result<ThreadsPublishResponse>.Fail("Failed to create text thread container.");
            }

            var publishBody = new { creation_id = container.Id };
            var published = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, publishBody);
            var responseJson = JsonConvert.SerializeObject(published);

            await LogAsync("Threads.CreateTextPost", published != null, 200, requestJson, responseJson);

            return published != null
                ? Result<ThreadsPublishResponse>.Ok(published)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish text thread.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.CreateTextPost", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> CreateImagePostAsync(CreateMediaThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var body = new Dictionary<string, object?> { ["media_type"] = "IMAGE", ["image_url"] = request.MediaUrl };
            if (!string.IsNullOrEmpty(request.Text)) body["text"] = request.Text;
            if (!string.IsNullOrEmpty(request.ReplyToId)) body["reply_to_id"] = request.ReplyToId;

            var container = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, body);
            if (container?.Id == null)
            {
                await LogAsync("Threads.CreateImagePost", false, 400, requestJson, "Container creation failed.");
                return Result<ThreadsPublishResponse>.Fail("Failed to create image thread container.");
            }

            var publishBody = new { creation_id = container.Id };
            var published = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, publishBody);
            var responseJson = JsonConvert.SerializeObject(published);

            await LogAsync("Threads.CreateImagePost", published != null, 200, requestJson, responseJson);

            return published != null
                ? Result<ThreadsPublishResponse>.Ok(published)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish image thread.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.CreateImagePost", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> CreateVideoPostAsync(CreateMediaThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var body = new Dictionary<string, object?> { ["media_type"] = "VIDEO", ["video_url"] = request.MediaUrl };
            if (!string.IsNullOrEmpty(request.Text)) body["text"] = request.Text;
            if (!string.IsNullOrEmpty(request.ReplyToId)) body["reply_to_id"] = request.ReplyToId;

            var container = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, body);
            if (container?.Id == null)
            {
                await LogAsync("Threads.CreateVideoPost", false, 400, requestJson, "Container creation failed.");
                return Result<ThreadsPublishResponse>.Fail("Failed to create video thread container.");
            }

            var publishBody = new { creation_id = container.Id };
            var published = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, publishBody);
            var responseJson = JsonConvert.SerializeObject(published);

            await LogAsync("Threads.CreateVideoPost", published != null, 200, requestJson, responseJson);

            return published != null
                ? Result<ThreadsPublishResponse>.Ok(published)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish video thread.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.CreateVideoPost", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> CreateCarouselPostAsync(CreateCarouselThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var itemIds = new List<string>();

            foreach (var mediaUrl in request.MediaUrls)
            {
                var itemBody = new Dictionary<string, object?> { ["media_type"] = "IMAGE", ["image_url"] = mediaUrl, ["is_carousel_item"] = true };
                var itemContainer = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, itemBody);
                if (itemContainer?.Id == null)
                {
                    await LogAsync("Threads.CreateCarouselPost", false, 400, requestJson, $"Carousel item container creation failed for URL: {mediaUrl}");
                    return Result<ThreadsPublishResponse>.Fail($"Failed to create carousel item container for URL: {mediaUrl}");
                }
                itemIds.Add(itemContainer.Id);
            }

            var carouselBody = new Dictionary<string, object?> { ["media_type"] = "CAROUSEL", ["children"] = itemIds };
            if (!string.IsNullOrEmpty(request.Text)) carouselBody["text"] = request.Text;

            var container = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, carouselBody);
            if (container?.Id == null)
            {
                await LogAsync("Threads.CreateCarouselPost", false, 400, requestJson, "Carousel container creation failed.");
                return Result<ThreadsPublishResponse>.Fail("Failed to create carousel thread container.");
            }

            var publishBody = new { creation_id = container.Id };
            var published = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, publishBody);
            var responseJson = JsonConvert.SerializeObject(published);

            await LogAsync("Threads.CreateCarouselPost", published != null, 200, requestJson, responseJson);

            return published != null
                ? Result<ThreadsPublishResponse>.Ok(published)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish carousel thread.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.CreateCarouselPost", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> PublishContainerAsync(PublishThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var body = new { creation_id = request.CreationId };
            var result = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, body);
            var responseJson = JsonConvert.SerializeObject(result);

            await LogAsync("Threads.PublishContainer", result != null, 200, requestJson, responseJson);

            return result != null
                ? Result<ThreadsPublishResponse>.Ok(result)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish thread container.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.PublishContainer", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPost>> GetPostAsync(string threadId, string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsPost>(
                $"/{threadId}?fields=id,text,timestamp,media_type,shortcode,media_url,permalink",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetPost", result != null, 200, threadId, responseJson);

            return result != null
                ? Result<ThreadsPost>.Ok(result)
                : Result<ThreadsPost>.Fail("Failed to retrieve thread post.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetPost", false, 500, threadId, ex.Message);
            return Result<ThreadsPost>.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeletePostAsync(string threadId, string accessToken)
    {
        try
        {
            var result = await _apiClient.DeleteAsync<object>($"/{threadId}", accessToken);
            var responseJson = JsonConvert.SerializeObject(result);
            var success = result != null;

            await LogAsync("Threads.DeletePost", success, success ? 200 : 400, threadId, responseJson);

            return success
                ? Result<bool>.Ok(true)
                : Result<bool>.Fail("Failed to delete thread post.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.DeletePost", false, 500, threadId, ex.Message);
            return Result<bool>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishResponse>> ReplyToPostAsync(string threadId, CreateTextThreadRequest request, string accessToken)
    {
        try
        {
            var requestJson = JsonConvert.SerializeObject(request);

            var body = new Dictionary<string, object?> { ["media_type"] = "TEXT", ["text"] = request.Text, ["reply_to_id"] = threadId };

            var container = await _apiClient.PostAsync<ThreadContainerResponse>("/me/threads", accessToken, body);
            if (container?.Id == null)
            {
                await LogAsync("Threads.ReplyToPost", false, 400, requestJson, "Reply container creation failed.");
                return Result<ThreadsPublishResponse>.Fail("Failed to create reply thread container.");
            }

            var publishBody = new { creation_id = container.Id };
            var published = await _apiClient.PostAsync<ThreadsPublishResponse>("/me/threads_publish", accessToken, publishBody);
            var responseJson = JsonConvert.SerializeObject(published);

            await LogAsync("Threads.ReplyToPost", published != null, 200, requestJson, responseJson);

            return published != null
                ? Result<ThreadsPublishResponse>.Ok(published)
                : Result<ThreadsPublishResponse>.Fail("Failed to publish reply thread.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.ReplyToPost", false, 500, JsonConvert.SerializeObject(request), ex.Message);
            return Result<ThreadsPublishResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsListResponse>> GetRepliesAsync(string threadId, string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsListResponse>(
                $"/{threadId}/replies?fields=id,text,timestamp,media_type",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetReplies", result != null, 200, threadId, responseJson);

            return result != null
                ? Result<ThreadsListResponse>.Ok(result)
                : Result<ThreadsListResponse>.Fail("Failed to retrieve thread replies.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetReplies", false, 500, threadId, ex.Message);
            return Result<ThreadsListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsListResponse>> GetConversationAsync(string threadId, string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsListResponse>(
                $"/{threadId}/conversation?fields=id,text,timestamp,media_type",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetConversation", result != null, 200, threadId, responseJson);

            return result != null
                ? Result<ThreadsListResponse>.Ok(result)
                : Result<ThreadsListResponse>.Fail("Failed to retrieve thread conversation.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetConversation", false, 500, threadId, ex.Message);
            return Result<ThreadsListResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsInsightsResponse>> GetInsightsAsync(string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsInsightsResponse>(
                "/me/insights?metric=views,likes,replies,reposts,quotes,followers_count&period=day",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetInsights", result != null, 200, null, responseJson);

            return result != null
                ? Result<ThreadsInsightsResponse>.Ok(result)
                : Result<ThreadsInsightsResponse>.Fail("Failed to retrieve Threads insights.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetInsights", false, 500, null, ex.Message);
            return Result<ThreadsInsightsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsInsightsResponse>> GetPostInsightsAsync(string threadId, string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsInsightsResponse>(
                $"/{threadId}/insights?metric=views,likes,replies,reposts,quotes",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetPostInsights", result != null, 200, threadId, responseJson);

            return result != null
                ? Result<ThreadsInsightsResponse>.Ok(result)
                : Result<ThreadsInsightsResponse>.Fail("Failed to retrieve thread post insights.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetPostInsights", false, 500, threadId, ex.Message);
            return Result<ThreadsInsightsResponse>.Fail(ex.Message);
        }
    }

    public async Task<Result<ThreadsPublishingLimitResponse>> GetPublishingLimitAsync(string accessToken)
    {
        try
        {
            var result = await _apiClient.GetAsync<ThreadsPublishingLimitResponse>(
                "/me/threads_publishing_limit?fields=config,quota_usage,reply_config,reply_quota_usage",
                accessToken);

            var responseJson = JsonConvert.SerializeObject(result);
            await LogAsync("Threads.GetPublishingLimit", result != null, 200, null, responseJson);

            return result != null
                ? Result<ThreadsPublishingLimitResponse>.Ok(result)
                : Result<ThreadsPublishingLimitResponse>.Fail("Failed to retrieve Threads publishing limit.");
        }
        catch (Exception ex)
        {
            await LogAsync("Threads.GetPublishingLimit", false, 500, null, ex.Message);
            return Result<ThreadsPublishingLimitResponse>.Fail(ex.Message);
        }
    }
}
