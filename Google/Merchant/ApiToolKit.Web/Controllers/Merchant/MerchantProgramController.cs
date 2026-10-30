using ApiToolKit.Application.DTOs.Merchant.Program;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// programs (v1)
/// </summary>
[ApiController]
[Route("api/merchant/accounts")]
[Produces("application/json")]
public class MerchantProgramController : ControllerBase
{
    private readonly IMerchantProgramService _programService;

    public MerchantProgramController(IMerchantProgramService programService)
    {
        _programService = programService;
    }

    /// <summary>Retrieves the specified program for the account.</summary>
    [HttpGet("accounts/{accountId}/programs/{programId}")]
    public async Task<IActionResult> GetProgramAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string programId)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new GetProgramRequest { AccountKey = accountKey, AccountId = accountId, ProgramId = programId };
        var result = await _programService.GetProgramAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Enable participation in the specified program for the account.</summary>
    [HttpPost("accounts/{accountId}/programs/{programId}/enable")]
    public async Task<IActionResult> EnableProgramAsync([FromRoute] string accountId, [FromRoute] string programId, [FromBody] EnableProgramRequest request)
    {
        request.AccountId = accountId;
        request.ProgramId = programId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _programService.EnableProgramAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Disable participation in the specified program for the account.</summary>
    [HttpPost("accounts/{accountId}/programs/{programId}/disable")]
    public async Task<IActionResult> DisableProgramAsync([FromRoute] string accountId, [FromRoute] string programId, [FromBody] DisableProgramRequest request)
    {
        request.AccountId = accountId;
        request.ProgramId = programId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _programService.DisableProgramAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Retrieves all programs for the account.</summary>
    [HttpGet("accounts/{accountId}/programs")]
    public async Task<IActionResult> ListProgramsAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromQuery] int? pageSize = null, [FromQuery] string? pageToken = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new ListProgramsRequest { AccountKey = accountKey, AccountId = accountId, PageSize = pageSize, PageToken = pageToken };
        var result = await _programService.ListProgramsAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
