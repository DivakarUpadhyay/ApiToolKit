using ApiToolKit.Application.DTOs.Facebook.InstagramShopping;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/instagram-shopping")]
[Produces("application/json")]
public class FacebookInstagramShoppingController : ControllerBase
{
    private readonly IFacebookInstagramShoppingService _service;
    public FacebookInstagramShoppingController(IFacebookInstagramShoppingService service) => _service = service;

    /// <summary>Get the product catalog linked to an Instagram user account.</summary>
    [HttpGet("{igUserId}/catalog")]
    public async Task<IActionResult> GetLinkedCatalog(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _service.GetLinkedCatalogAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Tag products on an Instagram media object.</summary>
    [HttpPost("media/{mediaId}/product-tags")]
    public async Task<IActionResult> TagProductsOnMedia(string mediaId, [FromBody] TagProductsRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.TagProductsOnMediaAsync(mediaId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get product tags on an Instagram media object.</summary>
    [HttpGet("media/{mediaId}/product-tags")]
    public async Task<IActionResult> GetProductTagsOnMedia(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _service.GetProductTagsOnMediaAsync(mediaId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete all product tags on an Instagram media object.</summary>
    [HttpDelete("media/{mediaId}/product-tags")]
    public async Task<IActionResult> DeleteProductTagsOnMedia(string mediaId, [FromQuery] string accessToken)
    {
        var result = await _service.DeleteProductTagsOnMediaAsync(mediaId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all media tagged with products for an Instagram user.</summary>
    [HttpGet("{igUserId}/tagged-media")]
    public async Task<IActionResult> GetTaggedProductMedia(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _service.GetTaggedProductMediaAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Submit an appeal for a rejected product for an Instagram user.</summary>
    [HttpPost("{igUserId}/appeal")]
    public async Task<IActionResult> AppealProduct(string igUserId, [FromBody] ProductAppealRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.AppealProductAsync(igUserId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Check shopping product tag eligibility for an Instagram user.</summary>
    [HttpGet("{igUserId}/eligibility")]
    public async Task<IActionResult> GetShoppingEligibility(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _service.GetShoppingEligibilityAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all products in an Instagram product catalog.</summary>
    [HttpGet("catalog/{catalogId}/products")]
    public async Task<IActionResult> GetCatalogProducts(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCatalogProductsAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all Instagram shops associated with an Instagram user.</summary>
    [HttpGet("{igUserId}/shops")]
    public async Task<IActionResult> GetIgShops(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _service.GetIgShopsAsync(igUserId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all available product catalogs for an Instagram user.</summary>
    [HttpGet("{igUserId}/available-catalogs")]
    public async Task<IActionResult> GetAvailableCatalogs(string igUserId, [FromQuery] string accessToken)
    {
        var result = await _service.GetAvailableCatalogsAsync(igUserId, accessToken);
        return Ok(result);
    }
}
