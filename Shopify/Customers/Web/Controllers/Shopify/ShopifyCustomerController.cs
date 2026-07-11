using ApiToolKit.Application.DTOs.Shopify.Customers;
using ApiToolKit.Application.Interfaces.Services.Shopify;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Shopify;

[ApiController]
[Route("api/shopify/customers")]
[Produces("application/json")]
public class ShopifyCustomerController : ControllerBase
{
    private readonly IShopifyCustomerService _customerService;

    public ShopifyCustomerController(IShopifyCustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>List customers with optional filters</summary>
    [HttpGet]
    public async Task<IActionResult> ListCustomersAsync([FromQuery] string shopDomain, [FromQuery] int? limit = null, [FromQuery] string? email = null, [FromQuery] string? sinceId = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.ListCustomersAsync(shopDomain, limit, email, sinceId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a customer by ID</summary>
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerAsync([FromQuery] string shopDomain, string customerId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.GetCustomerAsync(shopDomain, customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get total count of customers</summary>
    [HttpGet("count")]
    public async Task<IActionResult> CountCustomersAsync([FromQuery] string shopDomain)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.CountCustomersAsync(shopDomain);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Search customers by query (e.g. email:test@example.com)</summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchCustomersAsync([FromQuery] string shopDomain, [FromQuery] string q, [FromQuery] int? limit = null)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        if (string.IsNullOrWhiteSpace(q)) return BadRequest("q (query) is required.");
        var result = await _customerService.SearchCustomersAsync(shopDomain, q, limit);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get orders for a specific customer</summary>
    [HttpGet("{customerId}/orders")]
    public async Task<IActionResult> GetCustomerOrdersAsync([FromQuery] string shopDomain, string customerId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.GetCustomerOrdersAsync(shopDomain, customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a new customer</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromQuery] string shopDomain, [FromBody] CreateCustomerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.CreateCustomerAsync(shopDomain, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update an existing customer</summary>
    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomerAsync([FromQuery] string shopDomain, string customerId, [FromBody] UpdateCustomerRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.UpdateCustomerAsync(shopDomain, customerId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a customer</summary>
    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomerAsync([FromQuery] string shopDomain, string customerId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.DeleteCustomerAsync(shopDomain, customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Generate an account activation URL for a customer</summary>
    [HttpPost("{customerId}/activation-url")]
    public async Task<IActionResult> GenerateAccountActivationUrlAsync([FromQuery] string shopDomain, string customerId)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.GenerateAccountActivationUrlAsync(shopDomain, customerId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Send an account invite to a customer</summary>
    [HttpPost("{customerId}/invite")]
    public async Task<IActionResult> SendInviteAsync([FromQuery] string shopDomain, string customerId, [FromBody] SendInviteRequest request)
    {
        if (string.IsNullOrWhiteSpace(shopDomain)) return BadRequest("shopDomain is required.");
        var result = await _customerService.SendInviteAsync(shopDomain, customerId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
