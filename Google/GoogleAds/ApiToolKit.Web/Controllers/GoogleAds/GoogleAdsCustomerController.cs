using ApiToolKit.Application.DTOs.GoogleAds.Customer;
using ApiToolKit.Application.Interfaces.Services.GoogleAds;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.GoogleAds;

[ApiController]
[Route("api/googleads/customers")]
[Produces("application/json")]
public class GoogleAdsCustomerController : ControllerBase
{
    private readonly IGoogleAdsCustomerService _customerService;

    public GoogleAdsCustomerController(IGoogleAdsCustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>List all customers accessible with the provided access token</summary>
    [HttpGet("accessible")]
    public async Task<IActionResult> ListAccessibleCustomers([FromQuery] string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken)) return BadRequest("accessToken is required.");
        var result = await _customerService.ListAccessibleCustomersAsync(accessToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get details of a specific customer</summary>
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomer(string customerId)
    {
        var request = new GetCustomerRequest { CustomerId = customerId };
        var result = await _customerService.GetCustomerAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
