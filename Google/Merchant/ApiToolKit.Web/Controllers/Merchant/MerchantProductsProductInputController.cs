using ApiToolKit.Application.DTOs.Merchant.ProductsProductInput;
using ApiToolKit.Application.Interfaces.Services.Merchant;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Merchant;

/// <summary>
/// productInputs (v1)
/// </summary>
[ApiController]
[Route("api/merchant/products")]
[Produces("application/json")]
public class MerchantProductsProductInputController : ControllerBase
{
    private readonly IMerchantProductsProductInputService _productsProductInputService;

    public MerchantProductsProductInputController(IMerchantProductsProductInputService productsProductInputService)
    {
        _productsProductInputService = productsProductInputService;
    }

    /// <summary>Uploads a product input to your Merchant Center account.</summary>
    [HttpPost("accounts/{accountId}/productInputs/insert")]
    public async Task<IActionResult> InsertProductInputAsync([FromRoute] string accountId, [FromBody] InsertProductInputRequest request)
    {
        request.AccountId = accountId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productsProductInputService.InsertProductInputAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Deletes a product input from your Merchant Center account.</summary>
    [HttpDelete("accounts/{accountId}/productInputs/{productInputId}")]
    public async Task<IActionResult> DeleteProductInputAsync([FromQuery] string accountKey, [FromRoute] string accountId, [FromRoute] string productInputId, [FromQuery] string? dataSource = null)
    {
        if (string.IsNullOrEmpty(accountKey)) return BadRequest("accountKey is required.");
        var request = new DeleteProductInputRequest { AccountKey = accountKey, AccountId = accountId, ProductInputId = productInputId, DataSource = dataSource };
        var result = await _productsProductInputService.DeleteProductInputAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Updates the existing product input in your Merchant Center account.</summary>
    [HttpPatch("accounts/{accountId}/productInputs/{productInputId}")]
    public async Task<IActionResult> UpdateProductInputAsync([FromRoute] string accountId, [FromRoute] string productInputId, [FromBody] UpdateProductInputRequest request)
    {
        request.AccountId = accountId;
        request.ProductInputId = productInputId;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _productsProductInputService.UpdateProductInputAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
