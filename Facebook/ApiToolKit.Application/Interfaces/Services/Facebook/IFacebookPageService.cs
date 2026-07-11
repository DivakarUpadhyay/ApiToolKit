using ApiToolKit.Application.DTOs.Facebook.Page;
using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Application.DTOs.Facebook.Event;
using ApiToolKit.Domain.Common;

namespace ApiToolKit.Application.Interfaces.Services.Facebook;

public interface IFacebookPageService
{
    Task<Result<PageResponse>> GetPageInfoAsync(string pageId, string accessToken);
    Task<Result<GetPagesResponse>> GetPagesByUserAsync(string userId, string accessToken);
    Task<Result<PageResponse>> GetPageAccessTokenAsync(string pageId, string userAccessToken);
    Task<Result<bool>> UpdatePageSettingsAsync(PageSettingsRequest request, string accessToken);
    Task<Result<object>> GetPageCategoryListAsync(string accessToken);
    Task<Result<CreatePagePostResponse>> CreatePagePostAsync(CreatePagePostRequest request, string accessToken);
    Task<Result<FeedResponse>> GetPageFeedAsync(GetFeedRequest request, string accessToken);
    Task<Result<GetPhotosResponse>> GetPagePhotosAsync(GetPhotosRequest request, string accessToken);
    Task<Result<GetVideosResponse>> GetPageVideosAsync(GetVideosRequest request, string accessToken);
    Task<Result<List<EventResponse>>> GetPageEventsAsync(string pageId, string accessToken, int limit);
    Task<Result<bool>> UpdatePagePictureAsync(PagePictureRequest request, string accessToken);
    Task<Result<object>> GetPageRolesAsync(string pageId, string accessToken);

    // Tabs
    Task<Result<List<PageTabResponse>>> GetPageTabsAsync(string pageId, string accessToken);
    Task<Result<bool>> AddPageTabAsync(AddPageTabRequest request, string accessToken);
    Task<Result<bool>> UpdatePageTabAsync(UpdatePageTabRequest request, string accessToken);
    Task<Result<bool>> DeletePageTabAsync(string pageId, string tabId, string accessToken);

    // Milestones
    Task<Result<List<PageMilestoneResponse>>> GetPageMilestonesAsync(string pageId, string accessToken);
    Task<Result<object>> CreatePageMilestoneAsync(CreatePageMilestoneRequest request, string accessToken);
    Task<Result<bool>> DeletePageMilestoneAsync(string pageId, string milestoneId, string accessToken);

    // Blocked Users
    Task<Result<List<PageBlockedUserResponse>>> GetPageBlockedUsersAsync(string pageId, string accessToken, int limit);
    Task<Result<bool>> BlockUserFromPageAsync(BlockUserRequest request, string accessToken);
    Task<Result<bool>> UnblockUserFromPageAsync(string pageId, string userId, string accessToken);

    // CTA
    Task<Result<List<PageCtaResponse>>> GetPageCtaAsync(string pageId, string accessToken);
    Task<Result<object>> CreatePageCtaAsync(CreatePageCtaRequest request, string accessToken);
    Task<Result<bool>> DeletePageCtaAsync(string pageId, string ctaId, string accessToken);

    // Visitor Posts
    Task<Result<FeedResponse>> GetPageVisitorPostsAsync(PageVisitorPostsRequest request, string accessToken);

    // Labels
    Task<Result<List<PageLabelResponse>>> GetPageLabelsAsync(string pageId, string accessToken);
    Task<Result<object>> CreatePageLabelAsync(CreatePageLabelRequest request, string accessToken);
    Task<Result<bool>> DeletePageLabelAsync(string pageId, string labelId, string accessToken);
    Task<Result<bool>> AssignPageLabelAsync(AssignPageLabelRequest request, string accessToken);

    // Published & Scheduled Posts
    Task<Result<FeedResponse>> GetPagePublishedPostsAsync(PagePublishedPostsRequest request, string accessToken);
    Task<Result<FeedResponse>> GetPageScheduledPostsAsync(PageScheduledPostsRequest request, string accessToken);
    Task<Result<FeedResponse>> GetPagePromotablePostsAsync(PagePromotablePostsRequest request, string accessToken);
}
