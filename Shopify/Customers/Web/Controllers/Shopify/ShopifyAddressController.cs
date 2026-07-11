using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/customers/{customerId}/addresses")]
[Produces("application/json")]
public class ShopifyAddressController : ControllerBase
{
    private readonly IShopifyAddressService _addressService;

    public ShopifyAddressController(IShopifyAddressService addressService)
    {
        _addressService = addressService;
    }

    /// <summary>List all addresses for a customer</summary>
    [HttpGet]
    public async Task<IActionResult> ListAddressesAsync([FromQuery] string shopDomain, string customerId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.ListAddressesAsync(shopDomain, customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific address for a customer</summary>
    [HttpGet("{addressId}")]
    public async Task<IActionResult> GetAddressAsync([FromQuery] string shopDomain, string customerId, string addressId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.GetAddressAsync(shopDomain, customerId, addressId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new address for a customer</summary>
    [HttpPost]
    public async Task<IActionResult> CreateAddressAsync([FromQuery] string shopDomain, string customerId, [FromBody] CreateAddressRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.CreateAddressAsync(shopDomain, customerId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing address for a customer</summary>
    [HttpPut("{addressId}")]
    public async Task<IActionResult> UpdateAddressAsync([FromQuery] string shopDomain, string customerId, string addressId, [FromBody] UpdateAddressRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.UpdateAddressAsync(shopDomain, customerId, addressId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Set an address as the default address for a customer</summary>
    [HttpPut("{addressId}/default")]
    public async Task<IActionResult> SetDefaultAddressAsync([FromQuery] string shopDomain, string customerId, string addressId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.SetDefaultAddressAsync(shopDomain, customerId, addressId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete an address for a customer</summary>
    [HttpDelete("{addressId}")]
    public async Task<IActionResult> DeleteAddressAsync([FromQuery] string shopDomain, string customerId, string addressId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _addressService.DeleteAddressAsync(shopDomain, customerId, addressId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
