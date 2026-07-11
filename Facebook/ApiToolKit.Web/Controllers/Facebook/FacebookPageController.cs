using ApiToolKit.Application.DTOs.Facebook.Page;
using ApiToolKit.Application.DTOs.Facebook.Post;
using ApiToolKit.Application.DTOs.Facebook.Photo;
using ApiToolKit.Application.DTOs.Facebook.Video;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/page")]
[Produces("application/json")]
public class FacebookPageController : ControllerBase
{
    private readonly IFacebookPageService _pageService;

    public FacebookPageController(IFacebookPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>Get page information by page ID</summary>
    [HttpGet("{pageId}/info")]
    public async Task<IActionResult> GetPageInfoAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageInfoAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all pages managed by a user</summary>
    [HttpGet("user/{userId}/pages")]
    public async Task<IActionResult> GetPagesByUserAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPagesByUserAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get page-specific access token</summary>
    [HttpGet("{pageId}/access-token")]
    public async Task<IActionResult> GetPageAccessTokenAsync(string pageId, [FromQuery] string userAccessToken)
    {
        var result = await _pageService.GetPageAccessTokenAsync(pageId, userAccessToken);
        return Ok(result);
    }

    /// <summary>Update page settings</summary>
    [HttpPut("{pageId}/settings")]
    public async Task<IActionResult> UpdatePageSettingsAsync([FromBody] PageSettingsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.UpdatePageSettingsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get list of available page categories</summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetPageCategoryListAsync([FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageCategoryListAsync(accessToken);
        return Ok(result);
    }

    /// <summary>Create a post on the page</summary>
    [HttpPost("{pageId}/post")]
    public async Task<IActionResult> CreatePagePostAsync([FromBody] CreatePagePostRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.CreatePagePostAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get page's post feed</summary>
    [HttpPost("{pageId}/feed")]
    public async Task<IActionResult> GetPageFeedAsync([FromBody] GetFeedRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPageFeedAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get photos published on the page</summary>
    [HttpPost("{pageId}/photos")]
    public async Task<IActionResult> GetPagePhotosAsync([FromBody] GetPhotosRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPagePhotosAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get videos published on the page</summary>
    [HttpPost("{pageId}/videos")]
    public async Task<IActionResult> GetPageVideosAsync([FromBody] GetVideosRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPageVideosAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get events associated with the page</summary>
    [HttpGet("{pageId}/events")]
    public async Task<IActionResult> GetPageEventsAsync(string pageId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _pageService.GetPageEventsAsync(pageId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Update page profile picture</summary>
    [HttpPut("{pageId}/picture")]
    public async Task<IActionResult> UpdatePagePictureAsync([FromBody] PagePictureRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.UpdatePagePictureAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get roles assigned to users on the page</summary>
    [HttpGet("{pageId}/roles")]
    public async Task<IActionResult> GetPageRolesAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageRolesAsync(pageId, accessToken);
        return Ok(result);
    }

    // ── Tabs ──────────────────────────────────────────────────────────────────

    /// <summary>Get all tabs for a page</summary>
    [HttpGet("{pageId}/tabs")]
    public async Task<IActionResult> GetPageTabsAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageTabsAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Add an app tab to a page</summary>
    [HttpPost("{pageId}/tabs")]
    public async Task<IActionResult> AddPageTabAsync([FromBody] AddPageTabRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.AddPageTabAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Update a page tab</summary>
    [HttpPut("{pageId}/tabs/{tabId}")]
    public async Task<IActionResult> UpdatePageTabAsync([FromBody] UpdatePageTabRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.UpdatePageTabAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a tab from a page</summary>
    [HttpDelete("{pageId}/tabs/{tabId}")]
    public async Task<IActionResult> DeletePageTabAsync(string pageId, string tabId, [FromQuery] string accessToken)
    {
        var result = await _pageService.DeletePageTabAsync(pageId, tabId, accessToken);
        return Ok(result);
    }

    // ── Milestones ────────────────────────────────────────────────────────────

    /// <summary>Get all milestones for a page</summary>
    [HttpGet("{pageId}/milestones")]
    public async Task<IActionResult> GetPageMilestonesAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageMilestonesAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a milestone on a page</summary>
    [HttpPost("{pageId}/milestones")]
    public async Task<IActionResult> CreatePageMilestoneAsync([FromBody] CreatePageMilestoneRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.CreatePageMilestoneAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a page milestone</summary>
    [HttpDelete("{pageId}/milestones/{milestoneId}")]
    public async Task<IActionResult> DeletePageMilestoneAsync(string pageId, string milestoneId, [FromQuery] string accessToken)
    {
        var result = await _pageService.DeletePageMilestoneAsync(pageId, milestoneId, accessToken);
        return Ok(result);
    }

    // ── Blocked Users ─────────────────────────────────────────────────────────

    /// <summary>Get users blocked from a page</summary>
    [HttpGet("{pageId}/blocked")]
    public async Task<IActionResult> GetPageBlockedUsersAsync(string pageId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _pageService.GetPageBlockedUsersAsync(pageId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Block a user from a page</summary>
    [HttpPost("{pageId}/blocked")]
    public async Task<IActionResult> BlockUserFromPageAsync([FromBody] BlockUserRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.BlockUserFromPageAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Unblock a user from a page</summary>
    [HttpDelete("{pageId}/blocked/{userId}")]
    public async Task<IActionResult> UnblockUserFromPageAsync(string pageId, string userId, [FromQuery] string accessToken)
    {
        var result = await _pageService.UnblockUserFromPageAsync(pageId, userId, accessToken);
        return Ok(result);
    }

    // ── CTA ───────────────────────────────────────────────────────────────────

    /// <summary>Get page call-to-action button</summary>
    [HttpGet("{pageId}/cta")]
    public async Task<IActionResult> GetPageCtaAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageCtaAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Create page call-to-action button</summary>
    [HttpPost("{pageId}/cta")]
    public async Task<IActionResult> CreatePageCtaAsync([FromBody] CreatePageCtaRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.CreatePageCtaAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a page call-to-action</summary>
    [HttpDelete("{pageId}/cta/{ctaId}")]
    public async Task<IActionResult> DeletePageCtaAsync(string pageId, string ctaId, [FromQuery] string accessToken)
    {
        var result = await _pageService.DeletePageCtaAsync(pageId, ctaId, accessToken);
        return Ok(result);
    }

    // ── Visitor Posts ─────────────────────────────────────────────────────────

    /// <summary>Get posts made by visitors on a page</summary>
    [HttpPost("{pageId}/visitor-posts")]
    public async Task<IActionResult> GetPageVisitorPostsAsync([FromBody] PageVisitorPostsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPageVisitorPostsAsync(request, accessToken);
        return Ok(result);
    }

    // ── Labels ────────────────────────────────────────────────────────────────

    /// <summary>Get labels for a page</summary>
    [HttpGet("{pageId}/labels")]
    public async Task<IActionResult> GetPageLabelsAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _pageService.GetPageLabelsAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a label for a page</summary>
    [HttpPost("{pageId}/labels")]
    public async Task<IActionResult> CreatePageLabelAsync([FromBody] CreatePageLabelRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.CreatePageLabelAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a page label</summary>
    [HttpDelete("{pageId}/labels/{labelId}")]
    public async Task<IActionResult> DeletePageLabelAsync(string pageId, string labelId, [FromQuery] string accessToken)
    {
        var result = await _pageService.DeletePageLabelAsync(pageId, labelId, accessToken);
        return Ok(result);
    }

    /// <summary>Assign a label to a page user</summary>
    [HttpPost("{pageId}/labels/assign")]
    public async Task<IActionResult> AssignPageLabelAsync([FromBody] AssignPageLabelRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.AssignPageLabelAsync(request, accessToken);
        return Ok(result);
    }

    // ── Published / Scheduled / Promotable Posts ──────────────────────────────

    /// <summary>Get published posts for a page</summary>
    [HttpPost("{pageId}/published-posts")]
    public async Task<IActionResult> GetPagePublishedPostsAsync([FromBody] PagePublishedPostsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPagePublishedPostsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get scheduled posts for a page</summary>
    [HttpPost("{pageId}/scheduled-posts")]
    public async Task<IActionResult> GetPageScheduledPostsAsync([FromBody] PageScheduledPostsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPageScheduledPostsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get promotable posts for a page</summary>
    [HttpPost("{pageId}/promotable-posts")]
    public async Task<IActionResult> GetPagePromotablePostsAsync([FromBody] PagePromotablePostsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _pageService.GetPagePromotablePostsAsync(request, accessToken);
        return Ok(result);
    }
}
