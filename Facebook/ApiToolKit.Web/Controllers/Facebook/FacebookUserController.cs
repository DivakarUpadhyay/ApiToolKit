using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/user")]
[Produces("application/json")]
public class FacebookUserController : ControllerBase
{
    private readonly IFacebookUserService _userService;

    public FacebookUserController(IFacebookUserService userService)
    {
        _userService = userService;
    }

    /// <summary>Get user profile information</summary>
    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfileAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserProfileAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get list of user's friends</summary>
    [HttpGet("{userId}/friends")]
    public async Task<IActionResult> GetUserFriendsAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserFriendsAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get user's granted permissions</summary>
    [HttpGet("{userId}/permissions")]
    public async Task<IActionResult> GetUserPermissionsAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserPermissionsAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get user's profile picture</summary>
    [HttpGet("{userId}/picture")]
    public async Task<IActionResult> GetUserPictureAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserPictureAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get user's news feed</summary>
    [HttpGet("{userId}/feed")]
    public async Task<IActionResult> GetUserFeedAsync(string userId, [FromQuery] string accessToken, [FromQuery] int limit = 25)
    {
        var result = await _userService.GetUserFeedAsync(userId, accessToken, limit);
        return Ok(result);
    }

    /// <summary>Get pages managed by the user</summary>
    [HttpGet("{userId}/pages")]
    public async Task<IActionResult> GetUserPagesAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserPagesAsync(userId, accessToken);
        return Ok(result);
    }

    /// <summary>Get ad accounts associated with the user</summary>
    [HttpGet("{userId}/ad-accounts")]
    public async Task<IActionResult> GetUserAdAccountsAsync(string userId, [FromQuery] string accessToken)
    {
        var result = await _userService.GetUserAdAccountsAsync(userId, accessToken);
        return Ok(result);
    }
}
