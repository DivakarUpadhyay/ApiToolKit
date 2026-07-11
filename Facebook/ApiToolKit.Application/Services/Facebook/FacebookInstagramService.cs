using ApiToolKit.Application.DTOs.Facebook.Instagram;
using ApiToolKit.Application.DTOs.Facebook.Insight;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookInstagramService : IFacebookInstagramService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookInstagramRepository _igRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookInstagramService(IFacebookAuthRepository authRepo, IFacebookInstagramRepository igRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _igRepo = igRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<InstagramAccountResponse>> GetInstagramAccountAsync(string igUserId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<InstagramAccountResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,username,name,biography,followers_count,follows_count,media_count,profile_picture_url,website,ig_id",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}", queryParams);
        if (response == null) return Result<InstagramAccountResponse>.Fail("No response from Facebook API.");

        var account = JsonConvert.DeserializeObject<InstagramAccountResponse>(JsonConvert.SerializeObject(response)) ?? new InstagramAccountResponse();

        await _igRepo.SaveInstagramAccountAsync(new FacebookInstagram
        {
            IgUserId = igUserId,
            PageId = string.Empty,
            Username = account.Username,
            Name = account.Name,
            Biography = account.Biography,
            Followers = account.FollowersCount,
            MediaCount = account.MediaCount,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        await _igRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "GetInstagramAccount",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<InstagramAccountResponse>.Ok(account);
    }

    public async Task<Result<InstagramAccountResponse>> GetLinkedInstagramAccountAsync(string pageId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<InstagramAccountResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "instagram_business_account{id,username,name,biography,followers_count,follows_count,media_count,profile_picture_url,website}",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{pageId}", queryParams);
        if (response == null) return Result<InstagramAccountResponse>.Fail("No response from Facebook API.");

        var igData = response?.instagram_business_account;
        if (igData == null) return Result<InstagramAccountResponse>.Fail("No Instagram account linked to this page.");

        var account = JsonConvert.DeserializeObject<InstagramAccountResponse>(JsonConvert.SerializeObject(igData)) ?? new InstagramAccountResponse();
        return Result<InstagramAccountResponse>.Ok(account);
    }

    public async Task<Result<CreateMediaResponse>> CreateMediaAsync(CreateMediaRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreateMediaResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?> { ["media_type"] = request.MediaType };
        if (request.ImageUrl != null) body["image_url"] = request.ImageUrl;
        if (request.VideoUrl != null) body["video_url"] = request.VideoUrl;
        if (request.Caption != null) body["caption"] = request.Caption;
        body["is_carousel_item"] = request.IsCarouselItem;
        if (request.LocationId != null) body["location_id"] = request.LocationId;
        if (request.UserTags?.Count > 0) body["user_tags"] = request.UserTags;
        if (request.IsAiGenerated.HasValue) body["is_ai_generated"] = request.IsAiGenerated.Value.ToString().ToLower();
        if (request.IsPaidPartnership.HasValue) body["is_paid_partnership"] = request.IsPaidPartnership.Value.ToString().ToLower();
        if (request.AltText != null) body["alt_text"] = request.AltText;
        if (request.BrandedContentSponsorIds?.Count > 0) body["branded_content_sponsor_ids"] = request.BrandedContentSponsorIds;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media", body, accessToken);
        if (response == null) return Result<CreateMediaResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog
        {
            ApiName = "CreateMedia",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreateMediaResponse>.Ok(new CreateMediaResponse
        {
            Id = response?.id?.ToString() ?? string.Empty,
            CreationId = response?.id?.ToString()
        });
    }

    public async Task<Result<PublishMediaResponse>> PublishMediaAsync(PublishMediaRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PublishMediaResponse>.Fail("Facebook configuration not found.");

        var body = new { creation_id = request.CreationId };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media_publish", body, accessToken);
        if (response == null) return Result<PublishMediaResponse>.Fail("No response from Facebook API.");

        return Result<PublishMediaResponse>.Ok(new PublishMediaResponse { Id = response?.id?.ToString() ?? string.Empty });
    }

    public async Task<Result<List<MediaResponse>>> GetMediaAsync(GetMediaRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<MediaResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}/media", queryParams);
        if (response == null) return Result<List<MediaResponse>>.Fail("No response from Facebook API.");

        var media = JsonConvert.DeserializeObject<List<MediaResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MediaResponse>();

        return Result<List<MediaResponse>>.Ok(media);
    }

    public async Task<Result<List<MediaInsightResponse>>> GetMediaInsightsAsync(GetMediaInsightsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<MediaInsightResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["metric"] = string.Join(",", request.Metrics),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{request.MediaId}/insights", queryParams);
        if (response == null) return Result<List<MediaInsightResponse>>.Fail("No response from Facebook API.");

        var insights = JsonConvert.DeserializeObject<List<MediaInsightResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MediaInsightResponse>();

        return Result<List<MediaInsightResponse>>.Ok(insights);
    }

    public async Task<Result<HashtagResponse>> GetHashtagAsync(GetHashtagRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<HashtagResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["user_id"] = request.IgUserId,
            ["q"] = request.HashtagName,
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/ig_hashtag_search", queryParams);
        if (response == null) return Result<HashtagResponse>.Fail("No response from Facebook API.");

        var data = response?.data;
        string hashtagId = string.Empty;
        if (data != null)
        {
            var dataList = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(data));
            hashtagId = dataList?.FirstOrDefault()?.id?.ToString() ?? string.Empty;
        }

        return Result<HashtagResponse>.Ok(new HashtagResponse { Id = hashtagId });
    }

    public async Task<Result<object>> GetHashtagMediaAsync(GetHashtagMediaRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["user_id"] = request.IgUserId,
            ["fields"] = request.Fields.Count > 0 ? string.Join(",", request.Fields) : "id,caption,media_type,media_url,permalink,timestamp",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.MediaType != null) queryParams["media_type"] = request.MediaType;

        var endpoint = request.MediaType?.ToLower() == "top_media"
            ? $"/{request.HashtagId}/top_media"
            : $"/{request.HashtagId}/recent_media";

        var response = await _apiClient.GetAsync<object>(endpoint, queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<List<IgCommentResponse>>> GetIgCommentsAsync(string mediaId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<IgCommentResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,text,username,timestamp,like_count,hidden",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{mediaId}/comments", queryParams);
        if (response == null) return Result<List<IgCommentResponse>>.Fail("No response from Facebook API.");

        var comments = JsonConvert.DeserializeObject<List<IgCommentResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<IgCommentResponse>();

        return Result<List<IgCommentResponse>>.Ok(comments);
    }

    public async Task<Result<bool>> ReplyIgCommentAsync(string commentId, string message, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { message };
        var response = await _apiClient.PostAsync<dynamic>($"/{commentId}/replies", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> DeleteIgCommentAsync(string commentId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{commentId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        return Result<bool>.Ok(true);
    }

    public async Task<Result<BusinessDiscoveryResponse>> GetBusinessDiscoveryAsync(BusinessDiscoveryRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<BusinessDiscoveryResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = $"business_discovery.fields(id,username,name,biography,followers_count,media_count,profile_picture_url,website){{username={request.Username}}}",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}", queryParams);
        if (response == null) return Result<BusinessDiscoveryResponse>.Fail("No response from Facebook API.");

        var discoveryData = response?.business_discovery;
        if (discoveryData == null) return Result<BusinessDiscoveryResponse>.Fail("Business discovery data not found.");

        var result = JsonConvert.DeserializeObject<BusinessDiscoveryResponse>(JsonConvert.SerializeObject(discoveryData)) ?? new BusinessDiscoveryResponse();
        return Result<BusinessDiscoveryResponse>.Ok(result);
    }

    public async Task<Result<GetPageInsightsResponse>> GetIgAccountInsightsAsync(string igUserId, List<string> metrics, string period, string accessToken, long? since = null, long? until = null)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPageInsightsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["metric"] = string.Join(",", metrics),
            ["period"] = period,
            ["access_token"] = accessToken
        };
        if (since.HasValue) queryParams["since"] = since.Value.ToString();
        if (until.HasValue) queryParams["until"] = until.Value.ToString();

        var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/insights", queryParams);
        if (response == null) return Result<GetPageInsightsResponse>.Fail("No response from Facebook API.");

        var insights = JsonConvert.DeserializeObject<List<PageInsightResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PageInsightResponse>();

        return Result<GetPageInsightsResponse>.Ok(new GetPageInsightsResponse { Data = insights });
    }

    public async Task<Result<MediaContainerResponse>> CreateStoryAsync(CreateStoryRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MediaContainerResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (!string.IsNullOrEmpty(request.ImageUrl)) { body["image_url"] = request.ImageUrl; body["media_type"] = "STORIES"; }
        else if (!string.IsNullOrEmpty(request.VideoUrl)) { body["video_url"] = request.VideoUrl; body["media_type"] = "STORIES"; }
        else return Result<MediaContainerResponse>.Fail("Either imageUrl or videoUrl is required.");

        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media", body, request.AccessToken);
        if (response == null) return Result<MediaContainerResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "CreateStory", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MediaContainerResponse>.Ok(new MediaContainerResponse { Id = response?.id?.ToString() });
    }

    public async Task<Result<MediaContainerResponse>> CreateReelAsync(CreateReelRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MediaContainerResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>
        {
            ["media_type"] = "REELS",
            ["video_url"] = request.VideoUrl,
            ["caption"] = request.Caption,
            ["share_to_feed"] = request.ShareToFeed.ToString().ToLower()
        };
        if (!string.IsNullOrEmpty(request.CoverUrl)) body["cover_url"] = request.CoverUrl;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media", body, request.AccessToken);
        if (response == null) return Result<MediaContainerResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "CreateReel", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MediaContainerResponse>.Ok(new MediaContainerResponse { Id = response?.id?.ToString() });
    }

    public async Task<Result<MediaContainerResponse>> CreateCarouselAsync(CreateCarouselRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MediaContainerResponse>.Fail("Facebook configuration not found.");

        // Step 1: Create individual item containers
        var childrenIds = new List<string>();
        foreach (var item in request.Items)
        {
            var itemBody = new Dictionary<string, object?> { ["is_carousel_item"] = "true" };
            if (!string.IsNullOrEmpty(item.ImageUrl)) itemBody["image_url"] = item.ImageUrl;
            else if (!string.IsNullOrEmpty(item.VideoUrl)) { itemBody["video_url"] = item.VideoUrl; itemBody["media_type"] = "VIDEO"; }
            var itemResponse = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media", itemBody, request.AccessToken);
            string? itemId = itemResponse?.id?.ToString();
            if (itemId != null) childrenIds.Add(itemId);
        }

        // Step 2: Create carousel container
        var carouselBody = new Dictionary<string, object?>
        {
            ["media_type"] = "CAROUSEL",
            ["children"] = string.Join(",", childrenIds),
            ["caption"] = request.Caption
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/media", carouselBody, request.AccessToken);
        if (response == null) return Result<MediaContainerResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "CreateCarousel", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MediaContainerResponse>.Ok(new MediaContainerResponse { Id = response?.id?.ToString() });
    }

    public async Task<Result<ContainerStatusResponse>> GetContainerStatusAsync(ContainerStatusRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<ContainerStatusResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "status_code,status",
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.ContainerId}", queryParams);
        if (response == null) return Result<ContainerStatusResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetContainerStatus", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<ContainerStatusResponse>.Ok(new ContainerStatusResponse
        {
            Id = response?.id?.ToString(),
            StatusCode = response?.status_code?.ToString(),
            Status = response?.status?.ToString()
        });
    }

    public async Task<Result<PublishingLimitResponse>> GetPublishingLimitAsync(PublishingLimitRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PublishingLimitResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "config,quota_usage",
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}/content_publishing_limit", queryParams);
        if (response == null) return Result<PublishingLimitResponse>.Fail("No response from Facebook API.");

        var dataList = response?.data;
        var first = dataList != null ? JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(dataList[0])) : null;
        int? quotaTotal = (int?)first?.config?.quota_total;
        int? quotaUsage = (int?)first?.quota_usage;
        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetPublishingLimit", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<PublishingLimitResponse>.Ok(new PublishingLimitResponse { Config = quotaTotal, Quota100 = quotaUsage, QuotaDuration = "86400 seconds (24 hours)" });
    }

    public async Task<Result<MentionsResponse>> GetMentionsAsync(GetMentionsRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MentionsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "caption,media_type,media_url,timestamp",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}/tags", queryParams);
        if (response == null) return Result<MentionsResponse>.Fail("No response from Facebook API.");

        var data = JsonConvert.DeserializeObject<List<MentionDto>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MentionDto>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetMentions", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MentionsResponse>.Ok(new MentionsResponse { Data = data });
    }

    public async Task<Result<TaggedMediaResponse>> GetTaggedMediaAsync(GetTaggedMediaRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<TaggedMediaResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "caption,media_type,media_url,timestamp",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}/tags", queryParams);
        if (response == null) return Result<TaggedMediaResponse>.Fail("No response from Facebook API.");

        var data = JsonConvert.DeserializeObject<List<MentionDto>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MentionDto>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetTaggedMedia", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<TaggedMediaResponse>.Ok(new TaggedMediaResponse { Data = data });
    }

    public async Task<Result<MediaContainerResponse>> SendIgDmAsync(SendIgDmRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MediaContainerResponse>.Fail("Facebook configuration not found.");

        var body = new { recipient = new { id = request.RecipientIgId }, message = new { text = request.Message } };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.IgUserId}/messages", body, request.AccessToken);
        if (response == null) return Result<MediaContainerResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "SendIgDm", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MediaContainerResponse>.Ok(new MediaContainerResponse { Id = response?.id?.ToString() });
    }

    public async Task<Result<IgConversationsResponse>> GetIgConversationsAsync(GetIgConversationsRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<IgConversationsResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,updated_time,message_count",
            ["platform"] = "instagram",
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}/conversations", queryParams);
        if (response == null) return Result<IgConversationsResponse>.Fail("No response from Facebook API.");

        var data = JsonConvert.DeserializeObject<List<IgConversationDto>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<IgConversationDto>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetIgConversations", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<IgConversationsResponse>.Ok(new IgConversationsResponse { Data = data });
    }

    public async Task<Result<List<MediaResponse>>> GetStoriesAsync(string igUserId, string accessToken, string fields, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<MediaResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = fields,
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{igUserId}/stories", queryParams);
        if (response == null) return Result<List<MediaResponse>>.Fail("No response from Facebook API.");

        var stories = JsonConvert.DeserializeObject<List<MediaResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MediaResponse>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetStories", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<List<MediaResponse>>.Ok(stories);
    }

    public async Task<Result<object>> GetLiveMediaAsync(string igUserId, string accessToken, string fields, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = fields,
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<object>($"/{igUserId}/live_media", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetLiveMedia", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<object>.Ok(response);
    }

    public async Task<Result<List<MediaResponse>>> GetMediaChildrenAsync(string mediaId, string accessToken, string fields)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<MediaResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = fields,
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{mediaId}/children", queryParams);
        if (response == null) return Result<List<MediaResponse>>.Fail("No response from Facebook API.");

        var children = JsonConvert.DeserializeObject<List<MediaResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<MediaResponse>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetMediaChildren", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<List<MediaResponse>>.Ok(children);
    }

    public async Task<Result<bool>> DeleteMediaAsync(string mediaId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var response = await _apiClient.DeleteAsync<dynamic>($"/{mediaId}", accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "DeleteMedia", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<bool>.Ok(true);
    }

    public async Task<Result<bool>> ToggleCommentsAsync(string mediaId, bool commentsEnabled, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { comment_enabled = commentsEnabled };
        var response = await _apiClient.PostAsync<dynamic>($"/{mediaId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "ToggleComments", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetRecentlySearchedHashtagsAsync(string igUserId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<object>($"/{igUserId}/recently_searched_hashtags", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetRecentlySearchedHashtags", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetMentionedCommentAsync(string igUserId, string commentId, string fields, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        // Official API: field expansion syntax, not a standalone edge
        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = $"mentioned_comment.comment_id({commentId}){{{fields}}}",
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<object>($"/{igUserId}", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetMentionedComment", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<object>.Ok(response);
    }

    public async Task<Result<object>> GetMentionedMediaAsync(string igUserId, string mediaId, string fields, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        // Official API: field expansion syntax, not a standalone edge
        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = $"mentioned_media.fields({fields}){{{mediaId}}}",
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<object>($"/{igUserId}", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetMentionedMedia", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<object>.Ok(response);
    }

    public async Task<Result<MediaResponse>> GetMediaByIdAsync(string mediaId, string fields, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<MediaResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = fields,
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{mediaId}", queryParams);
        if (response == null) return Result<MediaResponse>.Fail("No response from Facebook API.");

        var media = JsonConvert.DeserializeObject<MediaResponse>(JsonConvert.SerializeObject(response)) ?? new MediaResponse();
        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetMediaById", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<MediaResponse>.Ok(media);
    }

    public async Task<Result<bool>> HideCommentAsync(string commentId, bool hide, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { hide };
        var response = await _apiClient.PostAsync<dynamic>($"/{commentId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "HideComment", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<bool>.Ok(true);
    }

    public async Task<Result<List<IgCommentResponse>>> GetCommentRepliesAsync(string commentId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<IgCommentResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,text,username,timestamp,like_count",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{commentId}/replies", queryParams);
        if (response == null) return Result<List<IgCommentResponse>>.Fail("No response from Facebook API.");

        var replies = JsonConvert.DeserializeObject<List<IgCommentResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<IgCommentResponse>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetCommentReplies", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<List<IgCommentResponse>>.Ok(replies);
    }

    public async Task<Result<IgCommentResponse>> GetSingleCommentAsync(string commentId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<IgCommentResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,text,username,timestamp,like_count,hidden,replies{id,text,username,timestamp}",
            ["access_token"] = accessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{commentId}", queryParams);
        if (response == null) return Result<IgCommentResponse>.Fail("No response from Facebook API.");

        var comment = JsonConvert.DeserializeObject<IgCommentResponse>(JsonConvert.SerializeObject(response)) ?? new IgCommentResponse();
        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetSingleComment", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<IgCommentResponse>.Ok(comment);
    }

    public async Task<Result<AddCommentResponse>> AddCommentAsync(AddCommentRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<AddCommentResponse>.Fail("Facebook configuration not found.");

        var body = new { message = request.Message };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.MediaId}/comments", body, request.AccessToken);
        if (response == null) return Result<AddCommentResponse>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "AddComment", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<AddCommentResponse>.Ok(new AddCommentResponse { Id = response?.id?.ToString() ?? string.Empty });
    }

    public async Task<Result<BusinessDiscoveryMediaResponse>> GetBusinessDiscoveryMediaAsync(BusinessDiscoveryMediaRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<BusinessDiscoveryMediaResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = $"business_discovery.fields(id,username,media.fields(id,caption,media_type,media_url,permalink,timestamp).limit({request.Limit})){{username={request.Username}}}",
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.IgUserId}", queryParams);
        if (response == null) return Result<BusinessDiscoveryMediaResponse>.Fail("No response from Facebook API.");

        var discovery = response?.business_discovery;
        if (discovery == null) return Result<BusinessDiscoveryMediaResponse>.Fail("Business discovery data not found.");

        var media = JsonConvert.DeserializeObject<List<MediaResponse>>(
            JsonConvert.SerializeObject(discovery?.media?.data ?? new List<object>())) ?? new List<MediaResponse>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetBusinessDiscoveryMedia", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<BusinessDiscoveryMediaResponse>.Ok(new BusinessDiscoveryMediaResponse
        {
            Id = discovery?.id?.ToString(),
            Username = discovery?.username?.ToString(),
            Media = media
        });
    }

    public async Task<Result<bool>> SubscribeInstagramWebhookAsync(IgWebhookSubscribeRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new
        {
            @object = "instagram",
            callback_url = request.CallbackUrl,
            verify_token = request.VerifyToken,
            fields = request.Fields
        };
        var response = await _apiClient.PostAsync<dynamic>($"/{request.AppId}/subscriptions", body, request.AccessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "SubscribeInstagramWebhook", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<bool>.Ok(true);
    }

    public async Task<Result<ConversationMessagesResponse>> GetConversationMessagesAsync(GetConversationMessagesRequest request)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<ConversationMessagesResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,message,from,created_time",
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = request.AccessToken
        };
        var response = await _apiClient.GetAsync<dynamic>($"/{request.ConversationId}/messages", queryParams);
        if (response == null) return Result<ConversationMessagesResponse>.Fail("No response from Facebook API.");

        var messages = JsonConvert.DeserializeObject<List<IgMessageDto>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<IgMessageDto>();

        await _igRepo.SaveApiLogAsync(new FacebookApiLog { ApiName = "GetConversationMessages", IsSuccess = true, StatusCode = 200, CreatedAt = DateTime.UtcNow });
        return Result<ConversationMessagesResponse>.Ok(new ConversationMessagesResponse { Data = messages });
    }
}
