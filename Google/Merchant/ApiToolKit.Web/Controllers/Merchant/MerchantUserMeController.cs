using ApiToolKit.Application.DTOs.Merchant.UserMe;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// users / me (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantUserMeController : ControllerBase
{
    private readonly IMerchantUserMeService _userMeService;

    public MerchantUserMeController(IMerchantUserMeService userMeService)
    {
        _userMeService = userMeService;
    }

    /// <summary>Updates the user that is represented by the caller from pending to verified.</summary>
    [HttpPatch("accounts/{accountId}/users/me/verifySelf")]
    public async Task<IActionResult> VerifySelfUserAsync([FromRoute] string accountId, [FromBody] VerifySelfUserRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userMeService.VerifySelfUserAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
