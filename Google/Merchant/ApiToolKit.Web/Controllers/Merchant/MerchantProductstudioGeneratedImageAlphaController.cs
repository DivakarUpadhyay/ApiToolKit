using ApiToolKit.Application.DTOs.Merchant.ProductstudioGeneratedImageAlpha;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// generatedImages (Alpha)
/// </summary>
[ApiController]
[Route("api/merchant/productstudio-v1alpha")]
[Produces("application/json")]
public class MerchantProductstudioGeneratedImageAlphaController : ControllerBase
{
    private readonly IMerchantProductstudioGeneratedImageAlphaService _productstudioGeneratedImageAlphaService;

    public MerchantProductstudioGeneratedImageAlphaController(IMerchantProductstudioGeneratedImageAlphaService productstudioGeneratedImageAlphaService)
    {
        _productstudioGeneratedImageAlphaService = productstudioGeneratedImageAlphaService;
    }

    /// <summary>UpscaleProductImage generates a new image where the resolution of the original image is enhanced.</summary>
    [HttpPost("accounts/{accountId}/generatedImages/upscaleProductImage")]
    public async Task<IActionResult> UpscaleProductImageGeneratedImageAsync([FromRoute] string accountId, [FromBody] UpscaleProductImageGeneratedImageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productstudioGeneratedImageAlphaService.UpscaleProductImageGeneratedImageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>RemoveProductImageBackground generates a new image where the background of the original image is removed.</summary>
    [HttpPost("accounts/{accountId}/generatedImages/removeProductImageBackground")]
    public async Task<IActionResult> RemoveProductImageBackgroundGeneratedImageAsync([FromRoute] string accountId, [FromBody] RemoveProductImageBackgroundGeneratedImageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productstudioGeneratedImageAlphaService.RemoveProductImageBackgroundGeneratedImageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>GenerateProductImageBackground generates a new image where the background of the original image is replaced by an AI generated scene based on provided product information and a ...</summary>
    [HttpPost("accounts/{accountId}/generatedImages/generateProductImageBackground")]
    public async Task<IActionResult> GenerateProductImageBackgroundGeneratedImageAsync([FromRoute] string accountId, [FromBody] GenerateProductImageBackgroundGeneratedImageRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productstudioGeneratedImageAlphaService.GenerateProductImageBackgroundGeneratedImageAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
