using ApiToolKit.Application.DTOs.Facebook.WhatsAppCatalog;
using ApiToolKit.Application.Interfaces.Services.Facebook;
using Microsoft.AspNetCore.Mvc;
namespace ApiToolKit.Web.Controllers.Facebook;

[ApiController]
[Route("api/facebook/whatsapp/catalog")]
[Produces("application/json")]
public class FacebookWhatsAppCatalogController : ControllerBase
{
    private readonly IFacebookWhatsAppCatalogService _service;
    public FacebookWhatsAppCatalogController(IFacebookWhatsAppCatalogService service) => _service = service;

    /// <summary>Get the product catalogs connected to a WhatsApp Business Account.</summary>
    [HttpGet("{wabaId}/catalogs")]
    public async Task<IActionResult> GetCatalogs(string wabaId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCatalogsAsync(wabaId, accessToken);
        return Ok(result);
    }

    /// <summary>Connect an existing catalog to a WhatsApp Business Account.</summary>
    [HttpPost("{wabaId}/catalogs/{catalogId}")]
    public async Task<IActionResult> ConnectCatalog(string wabaId, string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.ConnectCatalogAsync(wabaId, catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Disconnect a catalog from a WhatsApp Business Account.</summary>
    [HttpDelete("{wabaId}/catalogs/{catalogId}")]
    public async Task<IActionResult> DisconnectCatalog(string wabaId, string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.DisconnectCatalogAsync(wabaId, catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>List the catalogs owned by a Meta Business portfolio.</summary>
    [HttpGet("businesses/{businessId}/catalogs")]
    public async Task<IActionResult> GetBusinessCatalogs(string businessId, [FromQuery] string accessToken)
    {
        var result = await _service.GetBusinessCatalogsAsync(businessId, accessToken);
        return Ok(result);
    }

    /// <summary>Create a catalog owned by a Meta Business portfolio (connect it to the WABA afterwards).</summary>
    [HttpPost("businesses/{businessId}/catalogs")]
    public async Task<IActionResult> CreateCatalog(string businessId, [FromBody] CreateWabaCatalogRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.CreateCatalogAsync(businessId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Update a product in a WhatsApp Business catalog.</summary>
    [HttpPost("products/{productId}")]
    public async Task<IActionResult> UpdateCatalogProduct(string productId, [FromBody] UpdateWabaCatalogProductRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.UpdateCatalogProductAsync(productId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific WhatsApp Business catalog by ID.</summary>
    [HttpGet("catalogs/{catalogId}")]
    public async Task<IActionResult> GetCatalog(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCatalogAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Get all products in a WhatsApp Business catalog.</summary>
    [HttpGet("catalogs/{catalogId}/products")]
    public async Task<IActionResult> GetCatalogProducts(string catalogId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCatalogProductsAsync(catalogId, accessToken);
        return Ok(result);
    }

    /// <summary>Add a new product to a WhatsApp Business catalog.</summary>
    [HttpPost("catalogs/{catalogId}/products")]
    public async Task<IActionResult> AddCatalogProduct(string catalogId, [FromBody] CreateWabaCatalogProductRequest request, [FromQuery] string accessToken)
    {
        var result = await _service.AddCatalogProductAsync(catalogId, request, accessToken);
        return Ok(result);
    }

    /// <summary>Get details of a specific WhatsApp Business catalog product.</summary>
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> GetCatalogProduct(string productId, [FromQuery] string accessToken)
    {
        var result = await _service.GetCatalogProductAsync(productId, accessToken);
        return Ok(result);
    }

    /// <summary>Delete a product from a WhatsApp Business catalog.</summary>
    [HttpDelete("products/{productId}")]
    public async Task<IActionResult> DeleteCatalogProduct(string productId, [FromQuery] string accessToken)
    {
        var result = await _service.DeleteCatalogProductAsync(productId, accessToken);
        return Ok(result);
    }
}
