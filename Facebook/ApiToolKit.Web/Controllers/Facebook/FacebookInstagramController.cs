using ApiToolKit.Application.DTOs.Facebook.Instagram;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/instagram")]
[Produces("application/json")]
public class FacebookInstagramController : ControllerBase
{
    private readonly IFacebookInstagramService _instagramService;

    public FacebookInstagramController(IFacebookInstagramService instagramService)
    {
        _instagramService = instagramService;
    }

    /// <summary>Get Instagram account details by IG user ID</summary>
    [HttpGet("{igUserId}/account")]
    public async Task<IActionResult> GetInstagramAccountAsync(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.GetInstagramAccountAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Get Instagram account linked to a Facebook page</summary>
    [HttpGet("page/{pageId}/instagram")]
    public async Task<IActionResult> GetLinkedInstagramAccountAsync(string pageId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.GetLinkedInstagramAccountAsync(pageId, accessToken);
        return Ok(result);
    }

    /// <summary>Create an Instagram media container</summary>
    [HttpPost("media")]
    public async Task<IActionResult> CreateMediaAsync([FromBody] CreateMediaRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.CreateMediaAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Publish a previously created media container</summary>
    [HttpPost("media/publish")]
    public async Task<IActionResult> PublishMediaAsync([FromBody] PublishMediaRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.PublishMediaAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get media objects published by an IG user</summary>
    [HttpPost("media/list")]
    public async Task<IActionResult> GetMediaAsync([FromBody] GetMediaRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetMediaAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get insights for a specific Instagram media object</summary>
    [HttpPost("media/insights")]
    public async Task<IActionResult> GetMediaInsightsAsync([FromBody] GetMediaInsightsRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetMediaInsightsAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Search for a hashtag ID</summary>
    [HttpPost("hashtag/search")]
    public async Task<IActionResult> GetHashtagAsync([FromBody] GetHashtagRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetHashtagAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get top or recent media for a hashtag</summary>
    [HttpPost("hashtag/media")]
    public async Task<IActionResult> GetHashtagMediaAsync([FromBody] GetHashtagMediaRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetHashtagMediaAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get comments on an Instagram media object</summary>
    [HttpGet("media/{mediaId}/comments")]
    public async Task<IActionResult> GetIgCommentsAsync(string mediaId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _instagramService.GetIgCommentsAsync(mediaId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Reply to an Instagram comment</summary>
    [HttpPost("comment/{commentId}/reply")]
    public async Task<IActionResult> ReplyIgCommentAsync(string commentId, [FromQuery] string message, [FromQuery] string accessToken)
    {
        var result = await _instagramService.ReplyIgCommentAsync(commentId, message, accessToken);
        return Ok(result);
    }

    /// <summary>Delete an Instagram comment</summary>
    [HttpDelete("comment/{commentId}")]
    public async Task<IActionResult> DeleteIgCommentAsync(string commentId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.DeleteIgCommentAsync(commentId, accessToken);
        return Ok(result);
    }

    /// <summary>Get public data of an Instagram business account by username</summary>
    [HttpPost("business-discovery")]
    public async Task<IActionResult> GetBusinessDiscoveryAsync([FromBody] BusinessDiscoveryRequest request, [FromQuery] string accessToken)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetBusinessDiscoveryAsync(request, accessToken);
        return Ok(result);
    }

    /// <summary>Get account-level insights for an IG user</summary>
    [HttpGet("{igUserId}/insights")]
    public async Task<IActionResult> GetIgAccountInsightsAsync(string igUserId, [FromQuery] List<string> metrics, [FromQuery] string period, [FromQuery] string accessToken, [FromQuery] long? since = null, [FromQuery] long? until = null)
    {
        var result = await _instagramService.GetIgAccountInsightsAsync(igUserId, metrics, period, accessToken, since, until);
        return Ok(result);
    }

    /// <summary>Create an Instagram Story (image or video)</summary>
    [HttpPost("story")]
    public async Task<IActionResult> CreateStoryAsync([FromBody] CreateStoryRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.CreateStoryAsync(request);
        return Ok(result);
    }

    /// <summary>Create an Instagram Reel</summary>
    [HttpPost("reel")]
    public async Task<IActionResult> CreateReelAsync([FromBody] CreateReelRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.CreateReelAsync(request);
        return Ok(result);
    }

    /// <summary>Create a Carousel post (multiple images/videos)</summary>
    [HttpPost("carousel")]
    public async Task<IActionResult> CreateCarouselAsync([FromBody] CreateCarouselRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.CreateCarouselAsync(request);
        return Ok(result);
    }

    /// <summary>Check media container publishing status</summary>
    [HttpPost("container/status")]
    public async Task<IActionResult> GetContainerStatusAsync([FromBody] ContainerStatusRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetContainerStatusAsync(request);
        return Ok(result);
    }

    /// <summary>Check daily content publishing limit (max 50/day)</summary>
    [HttpPost("publishing-limit")]
    public async Task<IActionResult> GetPublishingLimitAsync([FromBody] PublishingLimitRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetPublishingLimitAsync(request);
        return Ok(result);
    }

    /// <summary>Get media where your account is @mentioned</summary>
    [HttpPost("mentions")]
    public async Task<IActionResult> GetMentionsAsync([FromBody] GetMentionsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetMentionsAsync(request);
        return Ok(result);
    }

    /// <summary>Get media where your account is tagged</summary>
    [HttpPost("tagged-media")]
    public async Task<IActionResult> GetTaggedMediaAsync([FromBody] GetTaggedMediaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetTaggedMediaAsync(request);
        return Ok(result);
    }

    /// <summary>Send Instagram Direct Message to a user</summary>
    [HttpPost("dm/send")]
    public async Task<IActionResult> SendIgDmAsync([FromBody] SendIgDmRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.SendIgDmAsync(request);
        return Ok(result);
    }

    /// <summary>Get Instagram DM conversations</summary>
    [HttpPost("dm/conversations")]
    public async Task<IActionResult> GetIgConversationsAsync([FromBody] GetIgConversationsRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetIgConversationsAsync(request);
        return Ok(result);
    }

    /// <summary>Get active Stories for an IG user</summary>
    [HttpGet("{igUserId}/stories")]
    public async Task<IActionResult> GetStoriesAsync(string igUserId, [FromQuery] string accessToken, [FromQuery] string fields = "id,media_type,media_url,timestamp,permalink", [FromQuery] int limit = 10)
    {
        var result = await _instagramService.GetStoriesAsync(igUserId, accessToken, fields, limit);
        return Ok(result);
    }

    /// <summary>Get live media broadcasts for an IG user</summary>
    [HttpGet("{igUserId}/live-media")]
    public async Task<IActionResult> GetLiveMediaAsync(string igUserId, [FromQuery] string accessToken, [FromQuery] string fields = "id,media_type,status,stream_url,timestamp", [FromQuery] int limit = 10)
    {
        var result = await _instagramService.GetLiveMediaAsync(igUserId, accessToken, fields, limit);
        return Ok(result);
    }

    /// <summary>Get children of a Carousel Album media object</summary>
    [HttpGet("media/{mediaId}/children")]
    public async Task<IActionResult> GetMediaChildrenAsync(string mediaId, [FromQuery] string accessToken, [FromQuery] string fields = "id,media_type,media_url,permalink,timestamp")
    {
        var result = await _instagramService.GetMediaChildrenAsync(mediaId, accessToken, fields);
        return Ok(result);
    }

    /// <summary>Delete an Instagram media object (non-ads, Stories, Reels, carousels)</summary>
    [HttpDelete("media/{mediaId}")]
    public async Task<IActionResult> DeleteMediaAsync(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.DeleteMediaAsync(mediaId, accessToken);
        return Ok(result);
    }

    /// <summary>Enable or disable comments on an Instagram media object</summary>
    [HttpPost("media/{mediaId}/toggle-comments")]
    public async Task<IActionResult> ToggleCommentsAsync(string mediaId, [FromQuery] bool commentsEnabled, [FromQuery] string accessToken)
    {
        var result = await _instagramService.ToggleCommentsAsync(mediaId, commentsEnabled, accessToken);
        return Ok(result);
    }

    /// <summary>Get hashtags recently searched by this IG user (last 7 days)</summary>
    [HttpGet("{igUserId}/recently-searched-hashtags")]
    public async Task<IActionResult> GetRecentlySearchedHashtagsAsync(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.GetRecentlySearchedHashtagsAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a comment where this IG user is @mentioned</summary>
    [HttpGet("{igUserId}/mentioned-comment")]
    public async Task<IActionResult> GetMentionedCommentAsync(string igUserId, [FromQuery] string commentId, [FromQuery] string accessToken, [FromQuery] string fields = "id,text,timestamp")
    {
        var result = await _instagramService.GetMentionedCommentAsync(igUserId, commentId, fields, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a media object where this IG user is @mentioned in a caption</summary>
    [HttpGet("{igUserId}/mentioned-media")]
    public async Task<IActionResult> GetMentionedMediaAsync(string igUserId, [FromQuery] string mediaId, [FromQuery] string accessToken, [FromQuery] string fields = "id,caption,media_type,media_url,permalink,timestamp")
    {
        var result = await _instagramService.GetMentionedMediaAsync(igUserId, mediaId, fields, accessToken);
        return Ok(result);
    }

    /// <summary>Get a specific Instagram media object by ID</summary>
    [HttpGet("media/{mediaId}/detail")]
    public async Task<IActionResult> GetMediaByIdAsync(string mediaId, [FromQuery] string accessToken, [FromQuery] string fields = "id,caption,media_type,media_url,thumbnail_url,permalink,timestamp,like_count,comments_count,shortcode")
    {
        var result = await _instagramService.GetMediaByIdAsync(mediaId, fields, accessToken);
        return Ok(result);
    }

    /// <summary>Hide or unhide a specific Instagram comment</summary>
    [HttpPost("comment/{commentId}/hide")]
    public async Task<IActionResult> HideCommentAsync(string commentId, [FromQuery] bool hide, [FromQuery] string accessToken)
    {
        var result = await _instagramService.HideCommentAsync(commentId, hide, accessToken);
        return Ok(result);
    }

    /// <summary>Get replies to a specific Instagram comment</summary>
    [HttpGet("comment/{commentId}/replies")]
    public async Task<IActionResult> GetCommentRepliesAsync(string commentId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _instagramService.GetCommentRepliesAsync(commentId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Get a single Instagram comment by ID</summary>
    [HttpGet("comment/{commentId}")]
    public async Task<IActionResult> GetSingleCommentAsync(string commentId, [FromQuery] string accessToken)
    {
        var result = await _instagramService.GetSingleCommentAsync(commentId, accessToken);
        return Ok(result);
    }

    /// <summary>Add a top-level comment to an Instagram media object</summary>
    [HttpPost("media/{mediaId}/comment")]
    public async Task<IActionResult> AddCommentAsync(string mediaId, [FromBody] AddCommentRequest request)
    {
        request.MediaId = mediaId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.AddCommentAsync(request);
        return Ok(result);
    }

    /// <summary>Get published media of another Instagram business account via Business Discovery</summary>
    [HttpPost("business-discovery/media")]
    public async Task<IActionResult> GetBusinessDiscoveryMediaAsync([FromBody] BusinessDiscoveryMediaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetBusinessDiscoveryMediaAsync(request);
        return Ok(result);
    }

    /// <summary>Subscribe your app to Instagram webhook notifications</summary>
    [HttpPost("webhook/subscribe")]
    public async Task<IActionResult> SubscribeInstagramWebhookAsync([FromBody] IgWebhookSubscribeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.SubscribeInstagramWebhookAsync(request);
        return Ok(result);
    }

    /// <summary>Get messages within an Instagram DM conversation</summary>
    [HttpPost("conversation/messages")]
    public async Task<IActionResult> GetConversationMessagesAsync([FromBody] GetConversationMessagesRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _instagramService.GetConversationMessagesAsync(request);
        return Ok(result);
    }
}
