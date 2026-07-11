using ApiToolKit.Application.DTOs.Facebook.Page;
using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Application.DTOs.Facebook.Event;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using ApiToolKit.Domain.Common;
using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using Newtonsoft.Json;

namespace ApiToolKit.Application.Services.Facebook;

public class FacebookPageService : IFacebookPageService
{
    private readonly IFacebookAuthRepository _authRepo;
    private readonly IFacebookPageRepository _pageRepo;
    private readonly IFacebookApiClient _apiClient;

    public FacebookPageService(IFacebookAuthRepository authRepo, IFacebookPageRepository pageRepo, IFacebookApiClient apiClient)
    {
        _authRepo = authRepo;
        _pageRepo = pageRepo;
        _apiClient = apiClient;
    }

    public async Task<Result<PageResponse>> GetPageInfoAsync(string pageId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PageResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,category,about,description,followers_count,fan_count,website,phone,emails,picture,cover",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{pageId}", queryParams);
        if (response == null) return Result<PageResponse>.Fail("No response from Facebook API.");

        PagePictureWrapper? pictureWrapper = null;
        if (response?.picture?.data != null)
        {
            pictureWrapper = new PagePictureWrapper
            {
                Data = new PagePictureData
                {
                    Url = response.picture.data.url?.ToString(),
                    Width = (int)(response.picture.data.width ?? 0),
                    Height = (int)(response.picture.data.height ?? 0),
                    IsSilhouette = (bool)(response.picture.data.is_silhouette ?? false)
                }
            };
        }

        PageCoverData? coverData = null;
        if (response?.cover != null)
        {
            coverData = new PageCoverData
            {
                Source = response.cover.source?.ToString(),
                Id = response.cover.id?.ToString()
            };
        }

        var page = new PageResponse
        {
            Id = response?.id?.ToString() ?? string.Empty,
            Name = response?.name?.ToString() ?? string.Empty,
            Category = response?.category?.ToString(),
            About = response?.about?.ToString(),
            Description = response?.description?.ToString(),
            Followers = response?.followers_count ?? 0,
            Likes = response?.fan_count ?? 0,
            Website = response?.website?.ToString(),
            Phone = response?.phone?.ToString(),
            Picture = pictureWrapper,
            Cover = coverData
        };

        await _pageRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = pageId,
            ApiName = "GetPageInfo",
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<PageResponse>.Ok(page);
    }

    public async Task<Result<GetPagesResponse>> GetPagesByUserAsync(string userId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPagesResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,category,about,description,followers_count,fan_count,website,phone,access_token",
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{userId}/accounts", queryParams);
        if (response == null) return Result<GetPagesResponse>.Fail("No response from Facebook API.");

        var pages = JsonConvert.DeserializeObject<List<PageResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PageResponse>();

        foreach (var p in pages)
        {
            await _pageRepo.SavePageAsync(new FacebookPage
            {
                PageId = p.Id,
                PageName = p.Name,
                PageCategory = p.Category,
                AccessToken = p.AccessToken ?? string.Empty,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        return Result<GetPagesResponse>.Ok(new GetPagesResponse { Data = pages });
    }

    public async Task<Result<PageResponse>> GetPageAccessTokenAsync(string pageId, string userAccessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<PageResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,access_token",
            ["access_token"] = userAccessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{pageId}", queryParams);
        if (response == null) return Result<PageResponse>.Fail("No response from Facebook API.");

        return Result<PageResponse>.Ok(new PageResponse
        {
            Id = response?.id?.ToString() ?? string.Empty,
            Name = response?.name?.ToString() ?? string.Empty,
            AccessToken = response?.access_token?.ToString()
        });
    }

    public async Task<Result<bool>> UpdatePageSettingsAsync(PageSettingsRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.About != null) body["about"] = request.About;
        if (request.Description != null) body["description"] = request.Description;
        if (request.Website != null) body["website"] = request.Website;
        if (request.Phone != null) body["phone"] = request.Phone;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}", body, accessToken);
        if (response == null) return Result<bool>.Fail("No response from Facebook API.");

        await _pageRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "UpdatePageSettings",
            RequestBody = JsonConvert.SerializeObject(request),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetPageCategoryListAsync(string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<object>($"/fb_page_categories", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    public async Task<Result<CreatePagePostResponse>> CreatePagePostAsync(CreatePagePostRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<CreatePagePostResponse>.Fail("Facebook configuration not found.");

        var body = new Dictionary<string, object?>();
        if (request.Message != null) body["message"] = request.Message;
        if (request.Link != null) body["link"] = request.Link;
        if (request.Picture != null) body["picture"] = request.Picture;

        var response = await _apiClient.PostAsync<dynamic>($"/{request.PageId}/feed", body, accessToken);
        if (response == null) return Result<CreatePagePostResponse>.Fail("No response from Facebook API.");

        await _pageRepo.SaveApiLogAsync(new FacebookApiLog
        {
            PageId = request.PageId,
            ApiName = "CreatePagePost",
            RequestBody = JsonConvert.SerializeObject(request),
            ResponseBody = JsonConvert.SerializeObject(response),
            IsSuccess = true,
            StatusCode = 200,
            CreatedAt = DateTime.UtcNow
        });

        return Result<CreatePagePostResponse>.Ok(new CreatePagePostResponse
        {
            Id = response?.id?.ToString() ?? string.Empty
        });
    }

    public async Task<Result<FeedResponse>> GetPageFeedAsync(GetFeedRequest request, string accessToken)
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

    public async Task<Result<GetPhotosResponse>> GetPagePhotosAsync(GetPhotosRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetPhotosResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/photos", queryParams);
        if (response == null) return Result<GetPhotosResponse>.Fail("No response from Facebook API.");

        var photos = JsonConvert.DeserializeObject<List<PhotoResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<PhotoResponse>();

        return Result<GetPhotosResponse>.Ok(new GetPhotosResponse { Data = photos });
    }

    public async Task<Result<GetVideosResponse>> GetPageVideosAsync(GetVideosRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<GetVideosResponse>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = request.Fields,
            ["limit"] = request.Limit.ToString(),
            ["access_token"] = accessToken
        };
        if (request.After != null) queryParams["after"] = request.After;

        var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/videos", queryParams);
        if (response == null) return Result<GetVideosResponse>.Fail("No response from Facebook API.");

        var videos = JsonConvert.DeserializeObject<List<VideoResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<VideoResponse>();

        return Result<GetVideosResponse>.Ok(new GetVideosResponse { Data = videos });
    }

    public async Task<Result<List<EventResponse>>> GetPageEventsAsync(string pageId, string accessToken, int limit)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<List<EventResponse>>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string>
        {
            ["fields"] = "id,name,description,start_time,end_time,place,cover,attending_count,interested_count,declined_count",
            ["limit"] = limit.ToString(),
            ["access_token"] = accessToken
        };

        var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/events", queryParams);
        if (response == null) return Result<List<EventResponse>>.Fail("No response from Facebook API.");

        var events = JsonConvert.DeserializeObject<List<EventResponse>>(
            JsonConvert.SerializeObject(response?.data ?? new List<object>())) ?? new List<EventResponse>();

        return Result<List<EventResponse>>.Ok(events);
    }

    public async Task<Result<bool>> UpdatePagePictureAsync(PagePictureRequest request, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<bool>.Fail("Facebook configuration not found.");

        var body = new { url = request.Picture, type = request.Type };
        await _apiClient.PostAsync<dynamic>($"/{request.PageId}/picture", body, accessToken);

        return Result<bool>.Ok(true);
    }

    public async Task<Result<object>> GetPageRolesAsync(string pageId, string accessToken)
    {
        var config = await _authRepo.GetConfigAsync();
        if (config == null) return Result<object>.Fail("Facebook configuration not found.");

        var queryParams = new Dictionary<string, string> { ["access_token"] = accessToken };
        var response = await _apiClient.GetAsync<object>($"/{pageId}/roles", queryParams);
        if (response == null) return Result<object>.Fail("No response from Facebook API.");

        return Result<object>.Ok(response);
    }

    // ── Tabs ──────────────────────────────────────────────────────────────────

    public async Task<Result<List<PageTabResponse>>> GetPageTabsAsync(string pageId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = "id,name,link,position,is_non_connection_landing_tab,is_permanent", ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/tabs", queryParams);
            if (response == null) return Result<List<PageTabResponse>>.Fail("No response from Facebook API.");
            var tabs = JsonConvert.DeserializeObject<List<PageTabResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PageTabResponse>();
            return Result<List<PageTabResponse>>.Ok(tabs);
        }
        catch (Exception ex) { return Result<List<PageTabResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> AddPageTabAsync(AddPageTabRequest request, string accessToken)
    {
        try { var body = new Dictionary<string, object?> { ["app_id"] = request.AppId }; await _apiClient.PostAsync<dynamic>($"/{request.PageId}/tabs", body, accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> UpdatePageTabAsync(UpdatePageTabRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?>();
            if (request.CustomName != null) body["custom_name"] = request.CustomName;
            if (request.Position.HasValue) body["position"] = request.Position.Value;
            if (request.IsNonConnectionLandingTab.HasValue) body["is_non_connection_landing_tab"] = request.IsNonConnectionLandingTab.Value;
            await _apiClient.PostAsync<dynamic>($"/{request.PageId}/tabs/{request.TabId}", body, accessToken);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeletePageTabAsync(string pageId, string tabId, string accessToken)
    {
        try { await _apiClient.DeleteAsync<object>($"/{pageId}/tabs/{tabId}", accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    // ── Milestones ────────────────────────────────────────────────────────────

    public async Task<Result<List<PageMilestoneResponse>>> GetPageMilestonesAsync(string pageId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = "id,title,description,start_time,created_time", ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/milestones", queryParams);
            if (response == null) return Result<List<PageMilestoneResponse>>.Fail("No response from Facebook API.");
            var milestones = JsonConvert.DeserializeObject<List<PageMilestoneResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PageMilestoneResponse>();
            return Result<List<PageMilestoneResponse>>.Ok(milestones);
        }
        catch (Exception ex) { return Result<List<PageMilestoneResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<object>> CreatePageMilestoneAsync(CreatePageMilestoneRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["title"] = request.Title, ["start_time"] = request.StartTime };
            if (request.Description != null) body["description"] = request.Description;
            var response = await _apiClient.PostAsync<object>($"/{request.PageId}/milestones", body, accessToken);
            return Result<object>.Ok(response ?? new { });
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeletePageMilestoneAsync(string pageId, string milestoneId, string accessToken)
    {
        try { await _apiClient.DeleteAsync<object>($"/{milestoneId}", accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    // ── Blocked Users ─────────────────────────────────────────────────────────

    public async Task<Result<List<PageBlockedUserResponse>>> GetPageBlockedUsersAsync(string pageId, string accessToken, int limit)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = "id,name", ["limit"] = limit.ToString(), ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/blocked", queryParams);
            if (response == null) return Result<List<PageBlockedUserResponse>>.Fail("No response from Facebook API.");
            var users = JsonConvert.DeserializeObject<List<PageBlockedUserResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PageBlockedUserResponse>();
            return Result<List<PageBlockedUserResponse>>.Ok(users);
        }
        catch (Exception ex) { return Result<List<PageBlockedUserResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> BlockUserFromPageAsync(BlockUserRequest request, string accessToken)
    {
        try { var body = new Dictionary<string, object?> { ["user"] = request.UserId }; await _apiClient.PostAsync<dynamic>($"/{request.PageId}/blocked", body, accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> UnblockUserFromPageAsync(string pageId, string userId, string accessToken)
    {
        try { await _apiClient.DeleteAsync<object>($"/{pageId}/blocked?uid={userId}", accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    // ── CTA ───────────────────────────────────────────────────────────────────

    public async Task<Result<List<PageCtaResponse>>> GetPageCtaAsync(string pageId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = "id,type,web_url", ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/call_to_actions", queryParams);
            if (response == null) return Result<List<PageCtaResponse>>.Fail("No response from Facebook API.");
            var ctas = JsonConvert.DeserializeObject<List<PageCtaResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PageCtaResponse>();
            return Result<List<PageCtaResponse>>.Ok(ctas);
        }
        catch (Exception ex) { return Result<List<PageCtaResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<object>> CreatePageCtaAsync(CreatePageCtaRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["type"] = request.Type };
            if (request.WebUrl != null) body["web_url"] = request.WebUrl;
            if (request.AndroidUrl != null) body["android_url"] = request.AndroidUrl;
            if (request.IphoneUrl != null) body["iphone_url"] = request.IphoneUrl;
            var response = await _apiClient.PostAsync<object>($"/{request.PageId}/call_to_actions", body, accessToken);
            return Result<object>.Ok(response ?? new { });
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeletePageCtaAsync(string pageId, string ctaId, string accessToken)
    {
        try { await _apiClient.DeleteAsync<object>($"/{ctaId}", accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    // ── Visitor Posts ─────────────────────────────────────────────────────────

    public async Task<Result<FeedResponse>> GetPageVisitorPostsAsync(PageVisitorPostsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = request.Fields, ["limit"] = request.Limit.ToString(), ["access_token"] = accessToken };
            if (request.After != null) queryParams["after"] = request.After;
            var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/visitor_posts", queryParams);
            if (response == null) return Result<FeedResponse>.Fail("No response from Facebook API.");
            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PostResponse>();
            return Result<FeedResponse>.Ok(new FeedResponse { Data = posts });
        }
        catch (Exception ex) { return Result<FeedResponse>.Fail(ex.Message); }
    }

    // ── Labels ────────────────────────────────────────────────────────────────

    public async Task<Result<List<PageLabelResponse>>> GetPageLabelsAsync(string pageId, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = "id,name,creation_time", ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{pageId}/labels", queryParams);
            if (response == null) return Result<List<PageLabelResponse>>.Fail("No response from Facebook API.");
            var labels = JsonConvert.DeserializeObject<List<PageLabelResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PageLabelResponse>();
            return Result<List<PageLabelResponse>>.Ok(labels);
        }
        catch (Exception ex) { return Result<List<PageLabelResponse>>.Fail(ex.Message); }
    }

    public async Task<Result<object>> CreatePageLabelAsync(CreatePageLabelRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["name"] = request.Name };
            var response = await _apiClient.PostAsync<object>($"/{request.PageId}/labels", body, accessToken);
            return Result<object>.Ok(response ?? new { });
        }
        catch (Exception ex) { return Result<object>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> DeletePageLabelAsync(string pageId, string labelId, string accessToken)
    {
        try { await _apiClient.DeleteAsync<object>($"/{labelId}", accessToken); return Result<bool>.Ok(true); }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    public async Task<Result<bool>> AssignPageLabelAsync(AssignPageLabelRequest request, string accessToken)
    {
        try
        {
            var body = new Dictionary<string, object?> { ["label_id"] = request.LabelId, ["user_id"] = request.UserId };
            await _apiClient.PostAsync<dynamic>($"/{request.PageId}/labels", body, accessToken);
            return Result<bool>.Ok(true);
        }
        catch (Exception ex) { return Result<bool>.Fail(ex.Message); }
    }

    // ── Published / Scheduled / Promotable Posts ──────────────────────────────

    public async Task<Result<FeedResponse>> GetPagePublishedPostsAsync(PagePublishedPostsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = request.Fields, ["limit"] = request.Limit.ToString(), ["access_token"] = accessToken };
            if (request.After != null) queryParams["after"] = request.After;
            if (request.Since != null) queryParams["since"] = request.Since;
            if (request.Until != null) queryParams["until"] = request.Until;
            var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/published_posts", queryParams);
            if (response == null) return Result<FeedResponse>.Fail("No response from Facebook API.");
            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PostResponse>();
            return Result<FeedResponse>.Ok(new FeedResponse { Data = posts });
        }
        catch (Exception ex) { return Result<FeedResponse>.Fail(ex.Message); }
    }

    public async Task<Result<FeedResponse>> GetPageScheduledPostsAsync(PageScheduledPostsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = request.Fields, ["limit"] = request.Limit.ToString(), ["access_token"] = accessToken };
            if (request.After != null) queryParams["after"] = request.After;
            var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/scheduled_posts", queryParams);
            if (response == null) return Result<FeedResponse>.Fail("No response from Facebook API.");
            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PostResponse>();
            return Result<FeedResponse>.Ok(new FeedResponse { Data = posts });
        }
        catch (Exception ex) { return Result<FeedResponse>.Fail(ex.Message); }
    }

    public async Task<Result<FeedResponse>> GetPagePromotablePostsAsync(PagePromotablePostsRequest request, string accessToken)
    {
        try
        {
            var queryParams = new Dictionary<string, string> { ["fields"] = request.Fields, ["limit"] = request.Limit.ToString(), ["include_hidden"] = request.IncludeHidden.ToString().ToLower(), ["access_token"] = accessToken };
            var response = await _apiClient.GetAsync<dynamic>($"/{request.PageId}/promotable_posts", queryParams);
            if (response == null) return Result<FeedResponse>.Fail("No response from Facebook API.");
            var posts = JsonConvert.DeserializeObject<List<PostResponse>>(JsonConvert.SerializeObject((object?)response?.data)) ?? new List<PostResponse>();
            return Result<FeedResponse>.Ok(new FeedResponse { Data = posts });
        }
        catch (Exception ex) { return Result<FeedResponse>.Fail(ex.Message); }
    }
}
